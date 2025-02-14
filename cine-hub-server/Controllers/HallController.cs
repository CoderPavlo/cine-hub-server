using cine_hub_server.DTOs.Auditorium;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace cine_hub_server.Controllers
{
    [ApiController]
    [Route("api/hall")]
    public class HallController : ControllerBase
    {
        private readonly IHallService _hallService;

        public HallController(IHallService hallService)
        {
            _hallService = hallService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPagination([FromQuery] int page = 1, [FromQuery] int itemsPerPage = 10, [FromQuery] string? cinemaId = null)
        {
            if (page < 1 || itemsPerPage < 1)
                return BadRequest(new { message = "Page number and items per page must be greater than 0." });

            var halls = await _hallService.GetAllPagination(page, itemsPerPage, cinemaId);
            return Ok(halls);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            var hall = _hallService.GetById(id);
            if (hall == null)
                return NotFound(new { message = "Hall not found" });

            return Ok(hall);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Create([FromBody] CreateAuditoriumDto hallDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _hallService.Create(hallDto);
            return Ok(new { message = "Hall created" });
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Update([FromRoute] string id, [FromBody] UpdateAuditoriumDto hallDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _hallService.Update(id, hallDto);
            return Ok(new { message = "Hall updated" });
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Delete([FromRoute] string id)
        {
            _hallService.Delete(id);
            return Ok(new { message = "Hall deleted" });
        }

    }
}