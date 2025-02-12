using cine_hub_server.Data_access;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;

namespace cine_hub_server.Repositories
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        public SessionRepository(CineDbContext context) : base(context)
        {
        }

        public IEnumerable<Session> GetSessionsByCinemaAndStartTime(string cinemaId, DateTime startTime)
        {
            return dbSet.Where(s => s.CinemaId == cinemaId && s.StartTime == startTime).ToList();
        }

        
        public IEnumerable<Session> GetSessionsByCinemaStartTimeAndFilmId(string cinemaId, DateTime startTime, int filmId)
        {
            return dbSet.Where(s => s.CinemaId == cinemaId && s.StartTime == startTime && s.FilmId == filmId).ToList();
        }
    }
}
