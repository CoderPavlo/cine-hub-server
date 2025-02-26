using Microsoft.AspNetCore.Mvc;
using cine_hub_server.DTOs.Session;
using cine_hub_server.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using cine_hub_server.Services;
using cine_hub_server.DTOs.Ticket;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace cine_hub_server.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ISessionService _sessionService;


        public TicketController(ITicketService ticketService, ISessionService sessionService)
        {
            _ticketService = ticketService;
            _sessionService = sessionService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<IActionResult> GetAllPagination([FromQuery] int page = 1, [FromQuery] int itemsPerPage = 10)
        {
            if (page < 1 || itemsPerPage < 1)
                return BadRequest(new { message = "Page number and items per page must be greater than 0." });
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tickets = await _ticketService.GetTicketsPagination(page, itemsPerPage, userId);
            return Ok(tickets);
        }

        [HttpGet("reserved")]
        public async Task<IActionResult> GetReservedSeats([FromQuery] string sessionId)
        {
            var tickets = _ticketService.GetReservedSeats(sessionId);
            var session = _sessionService.GetById(sessionId);
            var rows = _sessionService.GetRowCount(sessionId);
            var seats = _sessionService.GetSeatsCount(sessionId);
            return Ok(new { Session = session, ReservedSeats = tickets, Rows = rows, Seats = seats });
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            var ticket = _ticketService.GetById(id);
            if (ticket == null)
                return NotFound(new { message = "Ticket not found" });

            return Ok(ticket);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public IActionResult Create([FromBody] CreateTicketBodyDto ticketDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _ticketService.Create(new CreateTicketDto
            {
                UserId = userId,
                Price = ticketDto.Price,
                RowNumber = ticketDto.RowNumber,
                SeatNumber = ticketDto.SeatNumber,
                SessionId = ticketDto.SessionId,
            });
            return Ok(new { message = "Ticket created successfully" });
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Delete([FromRoute] string id)
        {
            _ticketService.Delete(id);
            return Ok(new { message = "Ticket deleted" });
        }
    }
}
