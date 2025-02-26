using Microsoft.AspNetCore.Identity;

namespace cine_hub_server.Models
{
    public class User : IdentityUser
    {
      
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
