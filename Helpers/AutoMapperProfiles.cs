
using AutoMapper;
using Defis.Dto;
using Defis.Models;


namespace Defis.Helpers
{
    public class AutoMapper: Profile
    {
         public AutoMapper()
        {
            //  CreateMap<Personne, PersonneDto>()
            // .ForMember(dest => dest.Image, opt =>
            // {
            //     // opt.MapFrom(d => d.Photo.ByteToString());
            //     opt.MapFrom(d => d.Image);
            // });
             CreateMap<DtoAuteur, Auteur>();
               CreateMap<Auteur, DtoAuteur>();
            //  CreateMap<Connexion, ConnexionDto>();
            //   CreateMap<Personnephysique, PersonnephysiqueDto>();
            //    CreateMap<Client, ClientDto>();
            //    CreateMap<Profession, ProfessionDto>();
            //     CreateMap<Modecontact, ModeContactDto>();
            //      CreateMap<CreditSimutaionDto, CreditRegister>();
        }
    }
}