using cine_hub_server.DTOs.Session;

namespace cine_hub_server.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionResponseDto> GetAll();
        SessionResponseDto GetById(string id);
        void Create(CreateSessionDto sessionDto);
        void Update(string id, UpdateSessionDto sessionDto);
        void Delete(string id);

        IEnumerable<SessionResponseDto> GetSessionsByCinemaAndStartTime(string cinemaId, DateTime startTime);
        IEnumerable<SessionResponseDto> GetSessionsByCinemaStartTimeAndFilmId(string cinemaId, DateTime startTime, int filmId);
    }
}