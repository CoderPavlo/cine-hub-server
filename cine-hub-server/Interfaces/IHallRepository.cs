using cine_hub_server.DTOs;
using cine_hub_server.Models;

namespace cine_hub_server.Interfaces
{
    public interface IHallRepository : IRepository<Auditorium>
    {
        public Task<PaginationResponseDto<Auditorium>> GetAllPagination(int page, int itemsPerPage, string? cinemaId);
    }
}
