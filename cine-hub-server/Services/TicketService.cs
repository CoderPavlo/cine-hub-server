using AutoMapper;
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
    }
}
