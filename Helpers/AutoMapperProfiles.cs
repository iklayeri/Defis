
using AutoMapper;
using Defis.Dto;
using Defis.Models;


namespace Defis.Helpers
{
    public class AutoMapper: Profile
    {
         public AutoMapper()
        {
            
             CreateMap<DtoAuteur, Auteur>();
               CreateMap<Auteur, DtoAuteur>();
                CreateMap<DtoCategorie, Category>();
               CreateMap<Category, DtoCategorie>();
            
        }
    }
}