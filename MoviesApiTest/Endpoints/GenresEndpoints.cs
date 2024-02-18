using GrowthApi.Dtos;
using GrowthApi.Entities;
using GrowthApi.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace GrowthApi.Endpoints
{
    public static class GenresEndpoints
    {
        public static RouteGroupBuilder MapGenres(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetGenres).CacheOutput(cache => cache.Expire(TimeSpan.FromSeconds(15)));
            group.MapGet("/{id:int}", GetGenreById);
            group.MapPost("/", CreateGenre);
            group.MapPut("/{id:int}", UpdateGenre);
            group.MapDelete("/{id:int}", DeleteGenre);

            return group;
        }


        static async Task<Ok<List<GenreDto>>> GetGenres(IGenreRepository genreRepository)
        {
            var genres = await genreRepository.GetAll();
            var genresDto = genres.Select(x => new GenreDto { Id = x.Id, Name = x.Name }).ToList();

            return TypedResults.Ok(genresDto);
        }

        static async Task<Results<Ok<GenreDto>, NotFound>> GetGenreById(IGenreRepository repository, int id)
        {
            var genre = await repository.GetById(id);

            if (genre is null)
            {
                return TypedResults.NotFound();
            }

            var genreDto = new GenreDto
            {
                Id = id,
                Name = genre.Name,
            };

            return TypedResults.Ok(genreDto);
        }

        static async Task<Created<GenreDto>> CreateGenre(CreateGenreDto createGenreDto,
            IGenreRepository repository,
            IOutputCacheStore outputCacheStore)
        {
            var genre = new Genre
            {
                Name = createGenreDto.Name,
            };
            var id = await repository.Create(genre);

            // Refreshing the cache right after inserting a genre
            await outputCacheStore.EvictByTagAsync("genres-get", default);

            var genreDto = new GenreDto
            {
                Id = id,
                Name = genre.Name,
            };

            return TypedResults.Created($"/genres/{id}", genreDto);
        }

        static async Task<Results<NoContent, NotFound>> UpdateGenre(int id, Genre genre, IGenreRepository genreRepository,
            IOutputCacheStore outputCacheStore)
        {
            var exists = await genreRepository.Exists(id);

            if (!exists)
            {
                TypedResults.NotFound();
            }

            await genreRepository.Update(genre);

            // Refreshing the cache right after inserting a genre
            await outputCacheStore.EvictByTagAsync("genres-get", default);

            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> DeleteGenre(int id,
            IGenreRepository genreRepository,
            IOutputCacheStore outputCacheStore)
        {
            var exists = await genreRepository.Exists(id);

            if (!exists)
            {
                return TypedResults.NotFound();
            }

            await genreRepository.Delete(id);

            // Refreshing the cache right after inserting a genre
            await outputCacheStore.EvictByTagAsync("genres-get", default);

            return TypedResults.NoContent();
        }
    }
}
