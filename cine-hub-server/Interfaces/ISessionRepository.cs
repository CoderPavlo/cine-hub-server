using cine_hub_server.DTOs;
using cine_hub_server.Models;

namespace cine_hub_server.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        public Task<PaginationResponseDto<Session>> GetSessionsPagination(int page, int itemsPerPage, string? cinemaId, string? hallId, int? filmId, DateTime? date);
    }
}
