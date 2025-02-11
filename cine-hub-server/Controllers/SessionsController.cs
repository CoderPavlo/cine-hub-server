using cine_hub_server.DTOs.Session;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace cine_hub_server.Controllers
{
    [ApiController]
    [Route("api/sessions")]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_sessionService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            var session = _sessionService.GetById(id);
            if (session == null)
                return NotFound("Session not found");

            return Ok(session);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] CreateSessionDto sessionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _sessionService.Create(sessionDto);
            return Ok("Session created");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] string id, [FromBody] UpdateSessionDto sessionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _sessionService.Update(id, sessionDto);
            return Ok("Session updated");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] string id)
        {
            _sessionService.Delete(id);
            return Ok("Session deleted");
        }
    }
}