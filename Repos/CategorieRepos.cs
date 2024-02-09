using System.Data.Common;

using AutoMapper;
using Defis.Dto;
using Defis.IRepos;
using Defis.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Defis.Repos
{
    public class CategorieRepos: ICategorie
    {
             private readonly DataContext _context;
        // private readonly ICacheRepos _rCache;
      
        //private readonly IMongodb _mongo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
       
      
        private readonly DbConnection connection;
       

         
         private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromDays(7);

         public CategorieRepos(
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

        public async Task<List<Category>> ListeCategorie(){
              try {

              
            return await _context.Categories.ToListAsync();
         

            } catch(Exception ex){
                throw ex;
            }

        }

        public async Task<Category> SaveCategorie(DtoCategorie dtoObjet)
        {

           
            Category dalObjet = new Category();
            DateTime dateoperation = DateTime.Now;

            try {

              dalObjet = _mapper.Map<Category>(dtoObjet) ;
            dalObjet.Code =new Guid().ToString();
                 dalObjet.DateCreation = dateoperation;
                 dalObjet.ModifierPar = dtoObjet.Userid;

            

             await _context.Categories.AddAsync(dalObjet);
            await _context.SaveChangesAsync();

           

            return dalObjet;
         

            } catch(Exception ex){
                throw ex;
            }
           
         

          
        }




    }

   
}