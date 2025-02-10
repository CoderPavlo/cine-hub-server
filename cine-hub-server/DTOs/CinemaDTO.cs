using System.Collections.Generic;

namespace cine_hub_server.DTOs.Cinema
{
    public class CinemaResponseDto
    {
        public string Id { get; set; }
        public string Location { get; set; }
        public ICollection<string> AuditoriumNames { get; set; }
    }

    public class CreateCinemaDto
    {
        public string Location { get; set; }
    }

    public class UpdateCinemaDto
    {
        public string Location { get; set; }
    }
}