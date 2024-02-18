using GrowthApi.Entities;

namespace GrowthApi.Repository
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
