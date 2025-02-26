using cine_hub_server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
namespace cine_hub_server.Data_access
{
    public class SeedData
    {

        public class SessionSeed
        {
            public DateTime StartTime { get; set; }
            public int Runtime { get; set; }
            public string FormatType { get; set; }
            public decimal Price { get; set; }
            public int FilmId { get; set; }
            public string FilmName { get; set; }
            public string CinemaId { get; set; }
            public string AuditoriumId { get; set; }
        }

        static async Task<T> readSeedData<T>(string path)
        {
            var json = await File.ReadAllTextAsync(path);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, 
                Converters = { new JsonStringEnumConverter() } 
            };
            var data = JsonSerializer.Deserialize<T>(json, options);
            if (data == null)
            {
                throw new Exception("Data is invalid or missing.");
            }
            return data;
        }

        static async Task addRoles(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
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
        }

        static async Task createAdmin(UserManager<User> userManager, IConfiguration configuration)
        {
            var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
            if (!adminUsers.Any())
            {
                var admin = await readSeedData<User>("Data_access/admin.json");
                var adminPassword = configuration["AdminCredentials:Password"];
                if (string.IsNullOrEmpty(adminPassword))
                {
                    throw new Exception("Admin password is missing in appsettings.json");
                }
                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        static async Task addCinemas(CineDbContext context)
        {
            if (!await context.Cinemas.AnyAsync())
            {
                var cinemas = await readSeedData<List<Cinema>>("Data_access/cinemas.json");

                await context.Cinemas.AddRangeAsync(cinemas);
                await context.SaveChangesAsync();
            }
        }

        static async Task addHalls(CineDbContext context)
        {
            if (!await context.Auditoriums.AnyAsync())
            {
                var halls = await readSeedData<List<Auditorium>>("Data_access/halls.json");

                await context.Auditoriums.AddRangeAsync(halls);
                await context.SaveChangesAsync();
            }
        }

        static async Task addSessions(CineDbContext context)
        {
            if (!await context.Sessions.AnyAsync())
            {
                List<Session> sessionsList = new List<Session>();
                var sessions = await readSeedData<List<SessionSeed>>("Data_access/sessions.json");
                for (int i = 0; i < 30; i++)
                {
                    DateTime day = DateTime.UtcNow.AddDays(i);
                    foreach (var session in sessions)
                    {
                        DateTime date = new DateTime(day.Year, day.Month, day.Day, session.StartTime.Hour, session.StartTime.Minute, 0, DateTimeKind.Utc);
                        sessionsList.Add(new Session
                        {
                            Id = Guid.NewGuid().ToString(),
                            StartTime = date,
                            EndTime = date.AddMinutes(session.Runtime),
                            FormatType = session.FormatType,
                            Price = session.Price,
                            FilmId = session.FilmId,
                            FilmName = session.FilmName,
                            CinemaId = session.CinemaId,
                            AuditoriumId = session.AuditoriumId,
                        });
                    }
                }
                await context.Sessions.AddRangeAsync(sessionsList);
                await context.SaveChangesAsync();
            }
        }
        public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var context = serviceProvider.GetRequiredService<CineDbContext>();

            await context.Database.MigrateAsync();

            await addRoles(userManager, roleManager);

            await createAdmin(userManager, configuration);

            await addCinemas(context);
            await addHalls(context);
            await addSessions(context);

    }
    }
}
