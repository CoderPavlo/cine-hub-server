using AutoMapper;
using cine_hub_server.DTOs.Session;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;
using cine_hub_server.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace cine_hub_server.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepo;
        private readonly IMapper _mapper;

        public SessionService(ISessionRepository sessionRepo, IMapper mapper)
        {
            _sessionRepo = sessionRepo;
            _mapper = mapper;
        }

        public IEnumerable<SessionResponseDto> GetAll()
        {
            var sessions = _sessionRepo.GetAll();
            return _mapper.Map<IEnumerable<SessionResponseDto>>(sessions);
        }

        public SessionResponseDto GetById(string id)
        {
            var session = _sessionRepo.GetByID(id);
            return session == null ? null : _mapper.Map<SessionResponseDto>(session);
        }

        public void Create(CreateSessionDto sessionDto)
        {
            var session = _mapper.Map<Session>(sessionDto);
            _sessionRepo.Insert(session);
            _sessionRepo.Save();
        }

        public void Update(string id, UpdateSessionDto sessionDto)
        {
            var session = _sessionRepo.GetByID(id);
            if (session == null) return;
            _mapper.Map(sessionDto, session);
            _sessionRepo.Update(session);
            _sessionRepo.Save();
        }

        public void Delete(string id)
        {
            var session = _sessionRepo.GetByID(id);
            if (session == null) return;
            _sessionRepo.Delete(session);
            _sessionRepo.Save();
        }
        public IEnumerable<SessionResponseDto> GetSessionsByCinemaAndStartTime(string cinemaId, DateTime startTime)
        {
            var sessions = _sessionRepo.GetSessionsByCinemaAndStartTime(cinemaId, startTime);
            return _mapper.Map<IEnumerable<SessionResponseDto>>(sessions);
        }

      
        public IEnumerable<SessionResponseDto> GetSessionsByCinemaStartTimeAndFilmId(string cinemaId, DateTime startTime, int filmId)
        {
            var sessions = _sessionRepo.GetSessionsByCinemaStartTimeAndFilmId(cinemaId, startTime, filmId);
            return _mapper.Map<IEnumerable<SessionResponseDto>>(sessions);
        }
    }
}