using AutoMapper;
using MoviesApiTest.Dtos;
using MoviesApiTest.Entities;

namespace MoviesApiTest.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<CreateGenreDto, Genre>();
            CreateMap<Genre, GenreDto>();

			CreateMap<CreateActorDto, Actor>()
                .ForMember(x => x.Picture, options => options.Ignore());
			CreateMap<Actor, ActorDto>();
		}
    }
}
