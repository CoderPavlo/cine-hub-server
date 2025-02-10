using AutoMapper;
using cine_hub_server.DTOs.Film;
using cine_hub_server.Models;

namespace cine_hub_server.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Film, FilmResponseDto>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                    src.FilmGenres.Select(fg => fg.Genre.Name)));
            CreateMap<CreateFilmDto, Film>()
                .ForMember(dest => dest.FilmGenres, opt => opt.Ignore());
            CreateMap<UpdateFilmDto, Film>()
                .ForMember(dest => dest.FilmGenres, opt => opt.Ignore());
        }
    }
}
