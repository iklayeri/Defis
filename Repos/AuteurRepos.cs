using System.Data.Common;

using AutoMapper;
using Defis.Dto;
using Defis.IRepos;
using Defis.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Defis.Repos
{
    public class AuteurRepos: IAuteurRepos
    {
             private readonly DataContext _context;
        // private readonly ICacheRepos _rCache;
      
        //private readonly IMongodb _mongo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
       
      
        private readonly DbConnection connection;
       

         
         private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromDays(7);

         public AuteurRepos(
            DataContext context,
           
            IMapper rmapper,
            IConfiguration config
            
        )
        {
            _context = context;
          
            // _rCache = rCache;
             
            _mapper = rmapper;
            _config = config;
           
            connection = _context.Database.GetDbConnection();
            //_mongoCarteAssure = mongoCarteAssure;
            //_mongoSignalRNotif = mongoSignalRNotif;
        }

        public async Task<List<Auteur>> ListeAuteur(){
              try {

              
            return await _context.Auteurs.ToListAsync();
         

            } catch(Exception ex){
                throw ex;
            }

        }

        public async Task<Auteur> SaveAuteur(DtoAuteur dtoObjet)
        {

           
            Auteur dalObjet = new Auteur();
            DateTime dateoperation = DateTime.Now;

            try {

                if(dtoObjet.Id ==0) {
                       dalObjet = _mapper.Map<Auteur>(dtoObjet) ;
            dalObjet.Code =new Guid().ToString();
                 dalObjet.DateCreation = dateoperation;
                 dalObjet.ModifierPar = dtoObjet.Userid;

            

             await _context.Auteurs.AddAsync(dalObjet);
                } else {
                   dalObjet = await  _context.Auteurs.FirstOrDefaultAsync(x=> x.Id == dtoObjet.Id );
                   dalObjet.Libelle = dtoObjet.Libelle;
                   dalObjet.Description = dtoObjet.Description;
                    dalObjet.DateModification = dateoperation;
                     dalObjet.ModifierPar = dtoObjet.Userid;
                     _context.Auteurs.Update(dalObjet);
                }

           
            await _context.SaveChangesAsync();

           

            return dalObjet;
         

            } catch(Exception ex){
                throw ex;
            }
           
         

          
        }




    }

   
}