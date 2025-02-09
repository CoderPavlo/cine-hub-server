namespace cine_hub_server.Services
{
    public class JwtOptions
    {
        public string Key { get; set; }
        public int AccessLifetime { get; set; }
        public int RefreshLifetime { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
