using AutoMapper;
using cine_hub_server.DTOs;
using cine_hub_server.DTOs.Cinema;
using cine_hub_server.DTOs.Session;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;
using Microsoft.AspNetCore.Http;

namespace cine_hub_server.Services
{
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepo;
        private readonly IMapper _mapper;

        public CinemaService(ICinemaRepository cinemaRepo, IMapper mapper)
        {
            _cinemaRepo = cinemaRepo;
            _mapper = mapper;
        }

        public CinemaResponseDto GetById(string id)
        {
            var cinema = _cinemaRepo.GetByID(id);
            return cinema == null ? null : _mapper.Map<CinemaResponseDto>(cinema);
        }

        public void Create(CreateCinemaDto cinemaDto)
        {
            var cinema = _mapper.Map<Cinema>(cinemaDto);
            _cinemaRepo.Insert(cinema);
            _cinemaRepo.Save();
        }

        public void Update(string id, UpdateCinemaDto cinemaDto)
        {
            var cinema = _cinemaRepo.GetByID(id);
            if (cinema == null) return;
            _mapper.Map(cinemaDto, cinema);
            _cinemaRepo.Update(cinema);
            _cinemaRepo.Save();
        }

        public void Delete(string id)
        {
            var cinema = _cinemaRepo.GetByID(id);
            if (cinema == null) return;
            if (cinema.Auditoriums != null && cinema.Auditoriums.Any())
            {
                throw new InvalidOperationException("Cannot delete cinema because there are associated halls.");
            }
            _cinemaRepo.Delete(cinema);
            _cinemaRepo.Save();
        }

        public async Task<PaginationResponseDto<CinemaResponseDto>> GetAllPagination(int page, int itemsPerPage)
        {
            var cinema = await _cinemaRepo.GetAllPagination(page, itemsPerPage);
            return new PaginationResponseDto<CinemaResponseDto>(
               cinema.TotalPages, cinema.TotalResults, cinema.Page,
               _mapper.Map<IEnumerable<CinemaResponseDto>>(cinema.Results)
             );
        }

    }
}
