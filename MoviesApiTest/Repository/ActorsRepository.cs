using GrowthApi;
using Microsoft.EntityFrameworkCore;
using MoviesApiTest.Entities;

namespace MoviesApiTest.Repository
{
	public class ActorsRepository : IActorsRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;

		public ActorsRepository(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public async Task<List<Actor>> GetAll()
		{
			return await _applicationDbContext.Actors.OrderBy(actor => actor.Name).ToListAsync();
		}

		public async Task<Actor?> GetById(int id)
		{
			return await _applicationDbContext.Actors.AsNoTracking().FirstOrDefaultAsync(actor => actor.Id == id);
		}

		public async Task<int> Create(Actor actor)
		{
			_applicationDbContext.Add(actor);
			await _applicationDbContext.SaveChangesAsync();
			return actor.Id;
		}

		public async Task<bool> Exist(int id)
		{
			return await _applicationDbContext.Actors.AnyAsync(actor => actor.Id == id);
		}


		public async Task Update(Actor actor)
		{
			_applicationDbContext.Update(actor);
			await _applicationDbContext.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			await _applicationDbContext.Actors.Where(actor => actor.Id == id).ExecuteDeleteAsync();
		}
	}
}
