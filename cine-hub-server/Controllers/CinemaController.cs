using cine_hub_server.DTOs.Cinema;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace cine_hub_server.Controllers
{
    [ApiController]
    [Route("api/cinema")]
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaService _cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPagination([FromQuery] int page = 1, [FromQuery] int itemsPerPage = 10)
        {
            if (page < 1 || itemsPerPage < 1)
                return BadRequest(new { message = "Page number and items per page must be greater than 0." });

            var cinemas = await _cinemaService.GetAllPagination(page, itemsPerPage);
            return Ok(cinemas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            var cinema = _cinemaService.GetById(id);
            if (cinema == null)
                return NotFound(new { message = "Cinema not found" });
            return Ok(cinema);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Create([FromBody] CreateCinemaDto cinemaDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _cinemaService.Create(cinemaDto);
            return Ok(new { message = "Cinema created" });
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Update([FromRoute] string id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _cinemaService.Update(id, cinemaDto);
            return Ok(new { message = "Cinema updated" });
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Delete([FromRoute] string id)
        {
            _cinemaService.Delete(id);
            return Ok(new { message = "Cinema deleted" });
        }

    }
}