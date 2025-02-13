using cine_hub_server.DTOs;
using cine_hub_server.DTOs.Cinema;
using cine_hub_server.DTOs.Session;
using cine_hub_server.Models;

namespace cine_hub_server.Interfaces
{
    public interface ICinemaService
    {
        Task<PaginationResponseDto<CinemaResponseDto>> GetAllPagination(int page, int itemsPerPage);
        CinemaResponseDto GetById(string id);
        void Create(CreateCinemaDto cinemaDto);
        void Update(string id, UpdateCinemaDto cinemaDto);
        void Delete(string id);
    }
}
