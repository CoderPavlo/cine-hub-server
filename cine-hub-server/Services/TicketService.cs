using AutoMapper;
using cine_hub_server.DTOs;
using cine_hub_server.DTOs.Session;
using cine_hub_server.DTOs.Ticket;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;

namespace cine_hub_server.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepo;
        private readonly IMapper _mapper;
        public TicketService(ITicketRepository ticketRepo, IMapper mapper)
        {
            _ticketRepo = ticketRepo;
            _mapper = mapper;
        }
        public TicketResponseDto GetById(string id)
        {
            var ticket = _ticketRepo.GetByID(id);
            return ticket == null ? null : _mapper.Map<TicketResponseDto>(ticket);
        }

        public IEnumerable<TicketSeatDto> GetReservedSeats(string sessionId)
        {
            var tickets = _ticketRepo.GetTicketsBySession(sessionId);
            return _mapper.Map<IEnumerable<TicketSeatDto>>(tickets);
        }

        public void Create(CreateTicketDto ticketDto)
        {
            var ticket = _mapper.Map<Ticket>(ticketDto);
            ticket.Id = Guid.NewGuid().ToString();
            _ticketRepo.Insert(ticket);
            _ticketRepo.Save();
        }

        public void Delete(string id)
        {
            var ticket = _ticketRepo.GetByID(id);
            if (ticket == null) return;
            _ticketRepo.Delete(ticket);
            _ticketRepo.Save();
        }

        public async Task<PaginationResponseDto<TicketResponseDto>> GetTicketsPagination(int page, int itemsPerPage, string userId)
        {
            var tickets = await _ticketRepo.GetTicketsPagination(page, itemsPerPage, userId);
            return new PaginationResponseDto<TicketResponseDto>(
                tickets.Total_pages, tickets.Total_results, tickets.Page,
                _mapper.Map<IEnumerable<TicketResponseDto>>(tickets.Results)
            );
        }
    }
}
