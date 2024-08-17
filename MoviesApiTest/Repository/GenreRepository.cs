using Microsoft.EntityFrameworkCore;
using MoviesApiTest.Entities;

namespace MoviesApiTest.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private ApplicationDbContext _applicationDbContext;

        public GenreRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> Create(Genre genre)
        {
            _applicationDbContext.Add(genre);

            await _applicationDbContext.SaveChangesAsync();

            return genre.Id;
        }

        public async Task Delete(int id)
        {
            await _applicationDbContext.Genres.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _applicationDbContext.Genres.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Genre>> GetAll()
        {
            return await _applicationDbContext.Genres.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Genre?> GetById(int id)
        {
            return await _applicationDbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Genre genre)
        {
            _applicationDbContext.Update(genre);

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
