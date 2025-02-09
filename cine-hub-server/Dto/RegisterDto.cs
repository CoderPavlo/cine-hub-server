namespace cine_hub_server.Dto
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        //public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
