namespace cine_hub_server.Models
{
    public class Cinema
    {
        public string Id { get; set; }
        public string Location { get; set; }

        public ICollection<Auditorium> Auditoriums { get; set; }
    }
}
