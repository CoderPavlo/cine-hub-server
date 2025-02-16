using AutoMapper;
using cine_hub_server.DTOs.Auditorium;
using cine_hub_server.DTOs;
using cine_hub_server.DTOs.Session;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;
using cine_hub_server.Repositories;
using System.Collections.Generic;
using System.Linq;
using System;

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

        public SessionResponseDto GetById(string id)
        {
            var session = _sessionRepo.GetByID(id);
            return session == null ? null : _mapper.Map<SessionResponseDto>(session);
        }

        public void Create(CreateSessionDto sessionDto)
        {
            var session = _mapper.Map<Session>(sessionDto);
            session.Id = Guid.NewGuid().ToString();
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
        public async Task<PaginationResponseDto<SessionResponseDto>> GetSessionsPagination(int page, int itemsPerPage, string? cinemaId, string? hallId, int? filmId, DateTime? date)
        {
            var sessions = await _sessionRepo.GetSessionsPagination(page, itemsPerPage, cinemaId, hallId, filmId, date);
            return new PaginationResponseDto<SessionResponseDto>(
                sessions.Total_pages, sessions.Total_results, sessions.Page,
                _mapper.Map<IEnumerable<SessionResponseDto>>(sessions.Results)
            );
        }
    }
}