using Movies.Domain.Entities;
using Movies.Application.DTOs.Movie;
using Movies.Application.DTOs.MovieAdmin;
using Movies.Application.Extensions;

namespace Movies.Application.Mappings
{
    public class MovieProfile : AutoMapper.Profile
    {
        public MovieProfile() { 
            
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.ThumbnailUrl, 
                    opt => opt.MapFrom(src => MovieExtensions.BuildImageUrl(src.ThumbnailUrl, 500)))
                .ForMember(dest => dest.BackdropUrl,
                    opt => opt.MapFrom(src => MovieExtensions.BuildImageUrl(src.BackdropUrl, 1280)))
                .ForMember(dest => dest.Genres,
                    opt => opt.MapFrom(src => src.MovieGenres.Select(mg => mg.Genre)));

            CreateMap<Movie, MovieAdminDto>()
                .ForMember(dest => dest.ThumbnailUrl,
                    opt => opt.MapFrom(src => MovieExtensions.BuildImageUrl(src.ThumbnailUrl, 500)))
                .ForMember(dest => dest.BackdropUrl,
                    opt => opt.MapFrom(src => MovieExtensions.BuildImageUrl(src.BackdropUrl, 1280)))
                .ForMember(dest => dest.Genres,
                    opt => opt.MapFrom(src => src.MovieGenres.Select(mg => mg.Genre)));

            CreateMap<CreateMovieAdminDto, Movie>()
                .ForMember(dest => dest.ThumbnailUrl,
                    opt => opt.MapFrom(src => $"/{src.ThumbnailUrl.Split('/').Last()}"))
                .ForMember(dest => dest.BackdropUrl,
                    opt => opt.MapFrom(src => $"/{src.BackdropUrl.Split('/').Last()}"));

            CreateMap<UpdateMovieAdminDto, Movie>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember, context) =>
                {
                    return srcMember != null;
                }));
        }
    }
}
