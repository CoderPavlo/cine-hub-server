using cine_hub_server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace cine_hub_server.Data_access
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var context = serviceProvider.GetRequiredService<CineDbContext>();

            // Переконуємося, що міграції застосовані
            await context.Database.MigrateAsync();
            // Перевірка ролей та їх додавання, якщо необхідно
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var role = new IdentityRole(roleName);
                    await roleManager.CreateAsync(role);
                }
            }
            // Перевірка, чи є в базі хоч один користувач з роллю "Admin"
            var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
            bool adminExists = adminUsers.Any();
            if (!adminExists)
            {
                var adminUser = new User
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    Name = "Admin",
                    Surname = "User",
                    Birthday = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123"); // Надійний пароль

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
            // Додавання кінотеатрів, якщо їх ще немає
            if (!await context.Cinemas.AnyAsync())
            {
                var cinemas = new List<Cinema>
                {
                    new Cinema { Id = Guid.NewGuid().ToString(), Location = "Kyiv, Ukraine" },
                    new Cinema { Id = Guid.NewGuid().ToString(), Location = "Lviv, Ukraine" }
                };

                await context.Cinemas.AddRangeAsync(cinemas);
                await context.SaveChangesAsync();
            }

            var cinemasFromDb = await context.Cinemas.ToListAsync();

            // Додавання аудиторій, якщо їх ще немає
            if (!await context.Auditoriums.AnyAsync())
            {
                var auditoriums = new List<Auditorium>
                {
                    new Auditorium { Id = Guid.NewGuid().ToString(), Name = "IMAX Hall", RowCount = 10, SeatsPerRow = 20, CinemaId = cinemasFromDb[0].Id },
                    new Auditorium { Id = Guid.NewGuid().ToString(), Name = "4DX Hall", RowCount = 8, SeatsPerRow = 15, CinemaId = cinemasFromDb[0].Id },
                    new Auditorium { Id = Guid.NewGuid().ToString(), Name = "VIP Hall", RowCount = 5, SeatsPerRow = 10, CinemaId = cinemasFromDb[1].Id }
                };

                await context.Auditoriums.AddRangeAsync(auditoriums);
                await context.SaveChangesAsync();
            }

            var auditoriumsFromDb = await context.Auditoriums.ToListAsync();

            // Додавання сеансів, якщо їх ще немає
            if (!await context.Sessions.AnyAsync())
            {
                var sessions = new List<Session>
                {
                    new Session
                    {
                        Id = Guid.NewGuid().ToString(),
                        StartTime = DateTime.UtcNow.AddHours(2),
                        EndTime = DateTime.UtcNow.AddHours(4),
                        FormatType = "IMAX",
                        Price = 250,
                        FilmId = 1,
                        FilmName = "Inception",
                        CinemaId = auditoriumsFromDb[0].CinemaId,
                        AuditoriumId = auditoriumsFromDb[0].Id
                    },
                    new Session
                    {
                        Id = Guid.NewGuid().ToString(),
                        StartTime = DateTime.UtcNow.AddHours(5),
                        EndTime = DateTime.UtcNow.AddHours(7),
                        FormatType = "4DX",
                        Price = 300,
                        FilmId = 0,
                        FilmName = "The Dark Knight",
                        CinemaId = auditoriumsFromDb[1].CinemaId,
                        AuditoriumId = auditoriumsFromDb[1].Id
                    },
                    new Session
                    {
                        Id = Guid.NewGuid().ToString(),
                        StartTime = DateTime.UtcNow.AddHours(8),
                        EndTime = DateTime.UtcNow.AddHours(10),
                        FormatType = "VIP",
                        Price = 500,
                        FilmId = 2,
                        FilmName = "Interstellar",
                        CinemaId = auditoriumsFromDb[2].CinemaId,
                        AuditoriumId = auditoriumsFromDb[2].Id
                    }
                };

                await context.Sessions.AddRangeAsync(sessions);
                await context.SaveChangesAsync();
            }
        
    }
    }
}
