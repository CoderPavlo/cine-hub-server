using Microsoft.AspNetCore.Mvc;
using cine_hub_server.DTOs.Session;
using cine_hub_server.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using cine_hub_server.Services;
using cine_hub_server.DTOs.Ticket;

namespace cine_hub_server.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
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
        public IActionResult Create([FromBody] CreateTicketDto ticketDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _ticketService.Create(ticketDto);
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
