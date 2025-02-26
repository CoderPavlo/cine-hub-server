namespace cine_hub_server.Models
{
    public class Auditorium
    {
        public string Id { get; set; }
        public string Name { get; set; }        
        public int RowCount { get; set; }    
        public int SeatsPerRow { get; set; } 

        public string CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}
