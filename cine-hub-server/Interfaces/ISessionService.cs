using cine_hub_server.DTOs;
using cine_hub_server.DTOs.Session;
using cine_hub_server.Models;

namespace cine_hub_server.Interfaces
{
    public interface ISessionService
    {
        SessionResponseDto GetById(string id);
        void Create(CreateSessionDto sessionDto);
        void Update(string id, UpdateSessionDto sessionDto);
        void Delete(string id);
        Task<PaginationResponseDto<SessionResponseDto>> GetSessionsPagination(int page, int itemsPerPage, string? cinemaId, string? hallId, int? filmId, DateTime? date);
    }
}