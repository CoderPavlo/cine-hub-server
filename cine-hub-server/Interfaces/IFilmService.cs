using cine_hub_server.DTOs.Film;

namespace cine_hub_server.Interfaces
{
    public interface IFilmService
    {
        IEnumerable<FilmResponseDto> GetAll();
        FilmResponseDto Get(string id);
        void Create(CreateFilmDto createFilmDto);
        void Update(string id, UpdateFilmDto updateFilmDto);
        void Delete(string id);
    }
}
