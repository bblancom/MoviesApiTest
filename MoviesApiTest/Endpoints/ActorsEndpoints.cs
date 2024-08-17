using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MoviesApiTest.Dtos;
using MoviesApiTest.Entities;
using MoviesApiTest.Repository;
using MoviesApiTest.Services;

namespace MoviesApiTest.Endpoints
{
	public static class ActorsEndpoints
	{
		private static readonly string _container = "actors";
		public static RouteGroupBuilder MapActors(this RouteGroupBuilder routeGroupBuilder)
		{
			// We are disabling anti forgery, but maybe should check that one later
			routeGroupBuilder.MapPost("/", Create).DisableAntiforgery();

			return routeGroupBuilder;
		}

		static async Task<Created<ActorDto>> Create([FromForm] CreateActorDto createActorDto,
			IActorsRepository actorsRepository, IOutputCacheStore outputCacheStore, 
			IMapper mapper, IFileStorage fileStorage)
		{
			var actor = mapper.Map<Actor> (createActorDto);

			// If picture is not null, then we save it
			if(createActorDto.Picture is not null)
			{
				var url = await fileStorage.Store(_container, createActorDto.Picture);
				actor.Picture = url;
			}

			var id = await actorsRepository.Create(actor);

			await outputCacheStore.EvictByTagAsync("actors-get", default);

			var actorDto = mapper.Map<ActorDto>(actor);

			return TypedResults.Created($"/actors/{id}", actorDto);
		}
	}
}
