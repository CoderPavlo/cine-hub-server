using cine_hub_server.Data_access;
using cine_hub_server.DTOs;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;
using Microsoft.EntityFrameworkCore;

namespace cine_hub_server.Repositories
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        public SessionRepository(CineDbContext context) : base(context)
        {
        }
        public async Task<PaginationResponseDto<Session>> GetSessionsPagination(int page, int itemsPerPage, string? cinemaId, string? hallId, int? filmId, DateTime? date)
        {
            DateTime today = DateTime.UtcNow.Date;
            IQueryable<Session> query = dbSet
                .Include(s => s.Cinema)
                .Include(s => s.Auditorium);

            if (!string.IsNullOrEmpty(cinemaId))
                query = query.Where(s => s.CinemaId == cinemaId);

            if (!string.IsNullOrEmpty(hallId))
                query = query.Where(s => s.AuditoriumId == hallId);

            if (filmId.HasValue)
                query = query.Where(s => s.FilmId == filmId.Value);

            if (date.HasValue)
                query = query.Where(s => s.StartTime.Date == date.Value.Date);
            else
                query = query.Where(s => s.EndTime.Date >= today);

            query = query.OrderBy(s => s.StartTime);

            int totalResults = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalResults / (double)itemsPerPage);
            var results = await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
            return new PaginationResponseDto<Session>(totalPages, totalResults, page, results);
        }

        public override IEnumerable<Session> GetAll()
        {
            return Get(includeProperties: "Cinema,Auditorium");
        }
        public override Session GetByID(object id)  
        {
            return Get(
                    filter: s => s.Id == id,    
                    includeProperties: "Cinema,Auditorium,Tickets"
                    ).FirstOrDefault();

        }
    }
}
