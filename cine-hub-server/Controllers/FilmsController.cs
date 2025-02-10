using cine_hub_server.DTOs.Film;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace cine_hub_server.Controllers
{
    public class FilmsController : ControllerBase
    {
        private readonly IFilmService filmService;

        public FilmsController(IFilmService filmService)
        {
            this.filmService = filmService;
        }

        // GET: ~/api/films/collection
        [HttpGet("collection")]
        public IActionResult GetAll()
        {
            return Ok(filmService.GetAll());
        }

        // GET: ~/api/films/{id}
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] string id)
        {
            return Ok(filmService.Get(id));
        }

        // POST: ~/api/films
        [HttpPost]
        public IActionResult Create([FromBody] CreateFilmDto film)
        {
            if (!ModelState.IsValid) return BadRequest();

            filmService.Create(film);

            return Ok();
        }

        // PUT: ~/api/films
        [HttpPut]
        public IActionResult Edit([FromRoute] string id, [FromBody] UpdateFilmDto film)
        {
            if (!ModelState.IsValid) return BadRequest();

            filmService.Update(id, film);

            return Ok();
        }

        // DELETE: ~/api/films/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            filmService.Delete(id);
            return Ok();
        }
    }
}
