using System.Collections.Generic;

namespace cine_hub_server.DTOs.Genre
{
    public class GenreResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> FilmIds { get; set; }
    }

    public class CreateGenreDto
    {
        public string Name { get; set; }
    }

    public class UpdateGenreDto
    {
        public string Name { get; set; }
    }
}