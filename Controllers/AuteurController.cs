using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Defis.Dto;
using AutoMapper;
using NLog;
using Defis.IRepos;

namespace Defis.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuteurController : ControllerBase
{
     private readonly IAuteurRepos _repos;
     private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
       
          public AuteurController(
           
            IConfiguration config,
            IMapper mapper,
          IAuteurRepos repos
           
        )
        {
         
            _config = config;
            _mapper = mapper;
            _repos = repos;

           

        }

     /// <summary>
        ///Liste des auteurs
        /// </summary>
        /// <remarks>
        /// </remarks>   
        [HttpPost("ListeAuteur")]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> ListeAuteur()
        {
            try
            {
                var allItems = await _repos.ListeAuteur();
                var resultat = new StatusReport
                {
                    Valide = true,
                    Resultat = allItems
                    //_mapper.Map<List<TypetarifDto>>(allItems)
                };
              

                _logger.Info($" Recuperation des auteurs");

                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.Error($"Erreur Recuperation desauteurs");
                _logger.Debug(ex.Message);
                 return StatusCode(500, "Erreur : "+ ex.Message);
            }
        }


  /// <summary>
        /// Auteur -  Save Auteur
        /// </summary>
        /// <remarks>
        /// Enregistrement Auteur 
        /// </remarks>   
        [HttpPost("CreationAuteur")]

        public async Task<IActionResult> CreationAuteur([FromBody] DtoAuteur data)
        {
            var resultat = new StatusReport();
            try
            {
                var date = DateTime.UtcNow;


                if (!ModelState.IsValid)
                {
                    _logger.Info($" Paramettre Invalide");
                    resultat.Message = " Paramettre Invalide";
                    resultat.Valide = false;
                    resultat.Resultat = null;
                    return BadRequest(ModelState);
                }



                var save = await _repos.SaveAuteur(data);

                if (save == null)
                {


                    resultat.Message = "Enregistrement Auteur ";
                    resultat.Valide = false;
                    resultat.Resultat = null;
                    _logger.Info($" Echec Enregistrement Auteur");
                    return BadRequest(resultat);
                }

                var savedto = _mapper.Map<DtoAuteur>(save);

                _logger.Info($" Enregistrement Auteur");
                resultat.Message = "Enregistrement Auteur";
                resultat.Valide = true;
                resultat.Resultat = savedto;


                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.Error($"Erreur Auteur");
                _logger.Debug(ex.Message);
                //  return StatusCode(500, "Erreur : "+ ex.Message);

                resultat.Message = ex.Message;
                resultat.Valide = false;
                resultat.Resultat = null;
                _logger.Info($" Echec Enregistrement Auteur");
                return BadRequest(resultat);
            }
            finally
            {

                
            }
        }
        
      

}