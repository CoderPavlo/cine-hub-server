using cine_hub_server.Models;

namespace cine_hub_server.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        IEnumerable<Session> GetSessionsByCinemaAndStartTime(string cinemaId, DateTime startTime);
        IEnumerable<Session> GetSessionsByCinemaStartTimeAndFilmId(string cinemaId, DateTime startTime, int filmId);
    }
}
