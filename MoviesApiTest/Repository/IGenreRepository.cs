using MoviesApiTest.Entities;

namespace MoviesApiTest.Repository
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAll();
        Task<Genre?> GetById(int id);
        Task<int> Create(Genre genre);
        Task<bool> Exists(int id);
        Task Update(Genre genre);
        Task Delete(int id);
    }
}
