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
public class CategorieController : ControllerBase
{
     private readonly ICategorie _repos;
     private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
       
          public CategorieController(
           
            IConfiguration config,
            IMapper mapper,
          ICategorie repos
           
        )
        {
         
            _config = config;
            _mapper = mapper;
            _repos = repos;

           

        }

     /// <summary>
        ///Liste des categorie
        /// </summary>
        /// <remarks>
        /// </remarks>   
        [HttpPost("ListeCategorie")]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> ListeCategorie()
        {
            try
            {
                var allItems = await _repos.ListeCategorie();
                var resultat = new StatusReport
                {
                    Valide = true,
                    Resultat = allItems
                   
                };
              

                _logger.Info($" Recuperation des categorie");

                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.Error($"Erreur Recuperation descategorie");
                _logger.Debug(ex.Message);
                 return StatusCode(500, "Erreur : "+ ex.Message);
            }
        }


  /// <summary>
        /// Categorie -  Save Categorie
        /// </summary>
        /// <remarks>
        /// Enregistrement Categorie 
        /// </remarks>   
        [HttpPost("CreationCategorie")]

        public async Task<IActionResult> CreationCategorie([FromBody] DtoCategorie data)
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



                var save = await _repos.SaveCategorie(data);

                if (save == null)
                {


                    resultat.Message = "Enregistrement Categorie ";
                    resultat.Valide = false;
                    resultat.Resultat = null;
                    _logger.Info($" Echec Enregistrement Categorie");
                    return BadRequest(resultat);
                }

                var savedto = _mapper.Map<DtoCategorie>(save);

                _logger.Info($" Enregistrement Categorie");
                resultat.Message = "Enregistrement Categorie";
                resultat.Valide = true;
                resultat.Resultat = savedto;


                return Ok(resultat);
            }
            catch (Exception ex)
            {
                _logger.Error($"Erreur Categorie");
                _logger.Debug(ex.Message);
                //  return StatusCode(500, "Erreur : "+ ex.Message);

                resultat.Message = ex.Message;
                resultat.Valide = false;
                resultat.Resultat = null;
                _logger.Info($" Echec Enregistrement Categorie");
                return BadRequest(resultat);
            }
            finally
            {

                
            }
        }
        
      

}