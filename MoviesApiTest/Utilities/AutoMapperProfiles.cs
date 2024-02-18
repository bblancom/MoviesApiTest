using AutoMapper;
using GrowthApi.Dtos;
using GrowthApi.Entities;

namespace MoviesApiTest.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<CreateGenreDto, Genre>();
            CreateMap<Genre, GenreDto>();
        }
    }
}
