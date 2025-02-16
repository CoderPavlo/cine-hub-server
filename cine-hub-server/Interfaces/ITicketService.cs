﻿using cine_hub_server.DTOs.Session;
using cine_hub_server.DTOs;
using cine_hub_server.DTOs.Ticket;

namespace cine_hub_server.Interfaces
{
      public interface ITicketService
      {
        TicketResponseDto GetById(string id);
        void Create(CreateTicketDto ticketDto);
            
        void Delete(string id);
         
      }
    
}
