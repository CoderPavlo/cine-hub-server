using cine_hub_server.DTOs.Session;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;
using cine_hub_server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IActionResult> GetAllPagination([FromQuery] int page = 1, [FromQuery] int itemsPerPage = 10, [FromQuery] string? cinemaId = null, [FromQuery] string? hallId = null, [FromQuery] int? filmId = null, [FromQuery] DateTime? date = null)
        {
            if (page < 1 || itemsPerPage < 1)
                return BadRequest(new { message = "Page number and items per page must be greater than 0." });

            var sessions = await _sessionService.GetSessionsPagination(page, itemsPerPage, cinemaId, hallId, filmId, date);
            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            var session = _sessionService.GetById(id);
            if (session == null)
                return NotFound(new { message = "Session not found" });

            return Ok(session);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Create([FromBody] CreateSessionsDto sessionsDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var endDate = sessionsDto.EndDate;
            if (!endDate.HasValue)
                endDate = sessionsDto.StartDateTime;
            Console.WriteLine(endDate);
            var i = 0;
            for (DateTime day = sessionsDto.StartDateTime; day <= endDate; day = day.AddDays(1))
            {
                _sessionService.Create(new CreateSessionDto
                {
                    StartTime = day,
                    EndTime = day.AddMinutes(sessionsDto.Runtime),
                    FormatType = sessionsDto.FormatType,
                    Price = sessionsDto.Price,
                    FilmId = sessionsDto.FilmId,
                    FilmName = sessionsDto.FilmName,
                    CinemaId = sessionsDto.CinemaId,
                    AuditoriumId = sessionsDto.AuditoriumId,
                });
                i++;
            }
            return Ok(new { message = i.ToString() + " sessions created" });
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Update([FromRoute] string id, [FromBody] UpdateSessionRequestDto sessionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _sessionService.Update(id, new UpdateSessionDto
            {
                StartTime = sessionDto.StartTime,
                EndTime = sessionDto.StartTime.AddMinutes(sessionDto.Runtime),
                FormatType = sessionDto.FormatType,
                Price = sessionDto.Price,
                FilmId = sessionDto.FilmId,
                FilmName = sessionDto.FilmName,
            });
            return Ok(new { message = "Session updated" });
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Delete([FromRoute] string id)
        {
            _sessionService.Delete(id);
            return Ok(new { message = "Session deleted" });
        }
        
    }
}