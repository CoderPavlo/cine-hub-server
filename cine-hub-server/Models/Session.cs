using System.Net.Sockets;

namespace cine_hub_server.Models
{
    public class Session
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string FormatType { get; set; }
        public decimal Price { get; set; }

        public string FilmId { get; set; }
        public Film Film { get; set; }

        public string CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        public string AuditoriumId { get; set; }
        public Auditorium Auditorium { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }

}
