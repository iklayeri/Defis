using System.Reflection;
using Defis.IRepos;
using Defis.Middleware;
using Defis.Models;
using Defis.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace  Defis.Helpers
{
	public static class ServiceExtension
	{
        public static void ConfigureDb(this WebApplicationBuilder builder)
        {
            var conf = builder.Configuration;
            var conctionstring = conf.GetConnectionString("DefaultConnection");
            
           

            builder.Services.AddDbContext<DataContext>(options =>
             options.UseNpgsql(conctionstring));

          

           

            builder.Services.AddHttpContextAccessor();

            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedForHeaderName = "X-Coming-From";
            });

        }

     
        public static void ConfigureCache(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();
            builder.Services.AddResponseCaching();
            // service.AddNCacheDistributedCache(conf.GetSection("NCacheSettings"));
        }

        public static void ConfigureSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Defis Rest API",
                    Version = "v1",
                    Description = "WebService Rest  Defis",
                    License = new OpenApiLicense
                    {
                        Name = "Defis Africa",
                        Url = new Uri("http://google.com/")
                    },
                    Contact = new OpenApiContact
                    {
                        Name = "Defis Developer",
                        Email = "iklayeri@outlook.com"
                    },
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
              
                c.AddServer(new OpenApiServer()
                {
                    Url = "http://localhost:1800"
                });


                c.AddSecurityDefinition("SessionId", new OpenApiSecurityScheme

                {

                    Name = "SessionId",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "ApiKeyScheme",
                    In = ParameterLocation.Header,
                    Description = "sessionId must appear in header"

                });




                c.AddSecurityRequirement(new OpenApiSecurityRequirement

                        {
                            {
                                    new OpenApiSecurityScheme
                                    {
                                    Reference = new OpenApiReference
                                        {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "SessionId"
                                        },
                                    In = ParameterLocation.Header
                                    },
                                        new string[]{}
                                }
                                });
            });
        }
        public static void ConfigureCors(this WebApplicationBuilder builder)
        {
        
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    b =>
                    {
                       
                        b
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(origin => true)
                        .AllowCredentials();
                    }
                );
            });
           



        }

         public static IApplicationBuilder UseOptionsRequest(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OptionsMiddleware>();
        }
        public static IApplicationBuilder UseValidRequest(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidSessionMiddleware>();
        }
          public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)

        {

            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();

        }




        public static void ConfigureInterface(this WebApplicationBuilder builder)
        {
           builder.Services.AddScoped<IAuteurRepos, AuteurRepos>();
             builder.Services.AddScoped<ICategorie, CategorieRepos>();
               builder.Services.AddScoped<IFilmRepos, FilmRepos>();
       

        }



    }
}
