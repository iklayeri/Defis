

using Defis.Dto;
using Defis.Models;

namespace Defis.IRepos
{
    public interface IAuteurRepos
    {
        Task<List<Auteur>> ListeAuteur();
        Task<Auteur> SaveAuteur(DtoAuteur dtoObjet);
    }
}