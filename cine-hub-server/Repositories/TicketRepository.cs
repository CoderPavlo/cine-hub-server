using cine_hub_server.Data_access;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;

namespace cine_hub_server.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(CineDbContext context) : base(context)
        {
        }
        public override IEnumerable<Ticket> GetAll()
        {
            return Get(includeProperties: "User,Session");
        }
        public override Ticket GetByID(object id)
        {
            return Get(
                filter: s => s.Id == id,
                includeProperties: "User,Session"
            ).FirstOrDefault();

        }
    }
}
