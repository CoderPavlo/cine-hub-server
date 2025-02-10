using cine_hub_server.DTOs.Film;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;
using cine_hub_server.Exceptions;
using AutoMapper;
using System.Net;

namespace cine_hub_server.Services
{
    public class FilmService
    {
        private readonly IRepository<Film> filmRepo;
        private readonly IMapper mapper;

        public FilmService(IRepository<Film> filmRepo, IMapper mapper)
        {
            this.filmRepo = filmRepo;
            this.mapper = mapper;
        }

        public void Create(CreateFilmDto filmDto)
        {
            var film = mapper.Map<Film>(filmDto);

            // Створення зв'язків із жанрами
            film.FilmGenres = filmDto.GenreIds
                .Select(genreId => new FilmGenre { GenreId = genreId, FilmId = film.Id })
                .ToList();

            filmRepo.Insert(film);
            filmRepo.Save();
        }

        public void Delete(string id)
        {
            var film = filmRepo.GetByID(id);

            if (film == null)
                throw new HttpException("Film not found", HttpStatusCode.NotFound);

            filmRepo.Delete(film);
            filmRepo.Save();
        }

        public void Update(string id, UpdateFilmDto updateFilmDto)
        {
            var film = filmRepo.GetByID(id);

            if (film == null)
                throw new HttpException("Film not found", HttpStatusCode.NotFound);

            // Оновлюємо сутність через AutoMapper
            mapper.Map(updateFilmDto, film);

            filmRepo.Update(film);
            filmRepo.Save();
        }

        public FilmResponseDto Get(string id)
        {
            var film = filmRepo.Get(
                filter: f => f.Id == id,
                includeProperties: $"{nameof(Film.FilmGenres)}.{nameof(FilmGenre.Genre)}"
             ).FirstOrDefault();

            if (film == null)
                throw new HttpException("Film not found", HttpStatusCode.NotFound);

            return mapper.Map<FilmResponseDto>(film);
        }

        public IEnumerable<FilmResponseDto> GetAll()
        {

            var films = filmRepo.Get(includeProperties: $"{nameof(Film.FilmGenres)}.{nameof(FilmGenre.Genre)}");
            return mapper.Map<IEnumerable<FilmResponseDto>>(films);
        }
    }
}
