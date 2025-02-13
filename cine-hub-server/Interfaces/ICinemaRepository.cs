using cine_hub_server.DTOs.Cinema;
using cine_hub_server.DTOs;
using cine_hub_server.Models;

namespace cine_hub_server.Interfaces
{
    public interface ICinemaRepository : IRepository<Cinema>
    {
        public Task<PaginationResponseDto<Cinema>> GetAllPagination(int page, int itemsPerPage);
    }
}
