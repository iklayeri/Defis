

using Defis.Dto;
using Defis.Models;

namespace Defis.IRepos
{
    public interface IFilmRepos
    {
        Task<List<Film>> ListeFilm();
        Task<Film> SaveFilm(DtoAuteur dtoObjet);
    }
}