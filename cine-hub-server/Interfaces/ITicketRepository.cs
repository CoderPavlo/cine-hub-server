using cine_hub_server.DTOs;
using cine_hub_server.Models;

namespace cine_hub_server.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        public Task<PaginationResponseDto<Ticket>> GetTicketsPagination(int page, int itemsPerPage, string userId);
        IEnumerable<Ticket> GetTicketsBySession(string sessionId);

    }
}
