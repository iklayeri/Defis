using System;
using Defis.Dto;


namespace Defis.Middleware
{
	public class ValidSessionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private const string SessionKey = "SessionId";

        public ValidSessionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)

        {
            var resultat = new StatusReport();
           // var transaction = context.RequestServices.GetRequiredService<IAuthRepos>();

            if (context.Request.Path.Value.Contains("Param") || context.Request.Method == "GET")
            {

                context.Request.Headers.TryGetValue(SessionKey, out var sessionKeyVal);
                if (!string.IsNullOrEmpty(sessionKeyVal))
                {
                   
                    if (sessionKeyVal.ToString() != "Nene")
                    {
                        resultat.Valide = false;
                        resultat.Message = "Session non valide!";
                        resultat.Resultat = null;
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsJsonAsync(resultat);

                    }
                    else
                        await _requestDelegate(context);
                }
                else
                    await _requestDelegate(context);
            }

            else if (!(context.Request.Path.Value.Contains("swagger")
                     || context.Request.Method == "OPTIONS"

                     || context.Request.Path.Value.Contains("Auth")
                     || context.Request.Path.Value.Contains("Param")
                     || context.Request.Path.Value.Contains("Cash")
                     || context.Request.Path.Value.Contains("negotiate")
                     || context.Request.Path.Value.Contains("NotifHub")
                  )
              )
            {
                if (!context.Request.Headers.TryGetValue(SessionKey, out var sessionKeyVal))
                {
                    resultat.Valide = false;
                    resultat.Message = "Session Key not found!";
                    resultat.Resultat = null;
                    context.Response.StatusCode = 402;
                    await context.Response.WriteAsJsonAsync(resultat);
                    // return BadHttpRequestException()
                    //WriteAsync("Api Key not found!");
                }
                else
                {

                    if (sessionKeyVal.ToString() != "Nene")
                    {
                        resultat.Valide = false;
                        resultat.Message = "Session non valide!";
                        resultat.Resultat = null;
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsJsonAsync(resultat);

                    }
                    else
                        await _requestDelegate(context);
                }

                //   var transaction =  context.RequestServices.GetRequiredService<IAuthRepository>();


            }



            else
                await _requestDelegate(context);

        }

    }
}

