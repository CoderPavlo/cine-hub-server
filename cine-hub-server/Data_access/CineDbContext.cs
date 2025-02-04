using cine_hub_server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

namespace cine_hub_server.Data_access
{
    public class CineDbContext : IdentityDbContext<User>
    {
        
        public CineDbContext(DbContextOptions<CineDbContext> options)
            : base(options) 
        { 
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("identity");
            
            builder.Entity<Cinema>().ToTable("Cinemas", "app");
            builder.Entity<Film>().ToTable("Films", "app");
            builder.Entity<Ticket>().ToTable("Tickets", "app");
            builder.Entity<Genre>().ToTable("Genres", "app");
            builder.Entity<FilmGenre>().ToTable("FilmGenres", "app");
            builder.Entity<Session>().ToTable("Sessions", "app");
            builder.Entity<Auditorium>().ToTable("Auditoriums", "app");
            builder.Entity<User>().ToTable("Users", "app");


        }
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmGenre> FilmGenres { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }


    }
}
