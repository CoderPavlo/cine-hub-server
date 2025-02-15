using cine_hub_server.Data_access;
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

        public override IEnumerable<Session> GetAll()
        {
            return Get(includeProperties: "Cinema,Auditorium");
        }
        public override Session GetByID(object id)  
        {
            return Get(
                filter: s => s.Id == id,
                includeProperties: "Cinema,Auditorium"
            ).FirstOrDefault();  

        }

        public IEnumerable<Session> GetSessionsByCinemaAndStartTime(string cinemaId, DateTime startTime)
        {
            return Get(
                filter: s => s.CinemaId == cinemaId && s.StartTime == startTime,
                includeProperties: "Cinema,Auditorium"
            );
        }

        public IEnumerable<Session> GetSessionsByCinemaStartTimeAndFilmId(string cinemaId, DateTime startTime, int filmId)
        {
            return Get(
                filter: s => s.CinemaId == cinemaId && s.StartTime == startTime && s.FilmId == filmId,
                includeProperties: "Cinema,Auditorium"
            );
        }
    }
}
