namespace cine_hub_server.Models
{
    public class Ticket
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string SessionId { get; set; }
        public Session Session { get; set; }
    }

}
