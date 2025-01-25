namespace cine_hub_server.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; } // ADMIN or USER
        public ICollection<Ticket> Tickets { get; set; }
    }
}
