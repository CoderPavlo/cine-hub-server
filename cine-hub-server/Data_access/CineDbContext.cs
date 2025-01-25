using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace cine_hub_server.Data_access
{
    public class CineDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public CineDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }
        
    }
}
