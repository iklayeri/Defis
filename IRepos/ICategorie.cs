

using Defis.Dto;
using Defis.Models;

namespace Defis.IRepos
{
    public interface ICategorie
    {
        Task<List<Category>> ListeCategorie();
        Task<Category> SaveCategorie(DtoCategorie dtoObjet);
    }
}