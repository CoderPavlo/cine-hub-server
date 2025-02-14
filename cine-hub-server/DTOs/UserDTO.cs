using System;
using System.Collections.Generic;

namespace cine_hub_server.DTOs.User
{
    public class UserResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public ICollection<string> TicketIds { get; set; }
    }

    public class RegisterUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
    }
}
