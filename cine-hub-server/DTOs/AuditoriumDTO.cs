using System.Collections.Generic;

namespace cine_hub_server.DTOs.Auditorium
{
    public class AuditoriumResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int RowCount { get; set; }
        public int SeatsPerRow { get; set; }
        public string CinemaName { get; set; }
    }

    public class CreateAuditoriumDto
    {
        public string Name { get; set; }
        public int RowCount { get; set; }
        public int SeatsPerRow { get; set; }
        public string CinemaId { get; set; }
    }

    public class UpdateAuditoriumDto
    {
        public string Name { get; set; }
        public int RowCount { get; set; }
        public int SeatsPerRow { get; set; }
    }
}