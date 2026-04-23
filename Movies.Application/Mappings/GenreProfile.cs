

using Movies.Application.DTOs.Genre;
using Movies.Domain.Entities;

namespace Movies.Application.Mappings
{
    public class GenreProfile : AutoMapper.Profile
    {
        public GenreProfile()
        {

            CreateMap<Genre, GenreDto>();
            CreateMap<CreateGenreDto, Genre>();
            CreateMap<UpdateGenreDto, Genre>();
        }
    }
}
