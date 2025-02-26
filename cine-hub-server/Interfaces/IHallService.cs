using cine_hub_server.DTOs;
using cine_hub_server.DTOs.Auditorium;
using cine_hub_server.DTOs.Cinema;
using cine_hub_server.Models;

namespace cine_hub_server.Interfaces
{
    public interface IHallService
    {
        Task<PaginationResponseDto<AuditoriumResponseDto>> GetAllPagination(int page, int itemsPerPage, string? cinemaId);
        AuditoriumResponseDto GetById(string id);
        void Create(CreateAuditoriumDto auditoriumDto);
        void Update(string id, UpdateAuditoriumDto auditoriumDto);
        void Delete(string id);
    }
}
