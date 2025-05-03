using System.Diagnostics.Metrics;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using DiegoProjectServer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DiegoProjectServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(NbaPlayersContext context, IHostEnvironment environment,
        UserManager<NbaUser> userManager) : ControllerBase
    {
        string _pathNamePlayers = Path.Combine(environment.ContentRootPath, "Data/nbaPlayers.csv");
        string _pathNameTeams = Path.Combine(environment.ContentRootPath, "Data/nbaTeams.csv");

        [HttpPost("Teams")]
        public async Task<ActionResult> importTeamsAsync()
        {
            // create a lookup dictionary containing all the countries already existing 
            // into the Database (it will be empty on first run).
            Dictionary<string, Team> teamsByName = context.Teams
                .AsNoTracking().ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathNameTeams);
            using CsvHelper.CsvReader csv = new(reader, config);

            List<teamDto> records = csv.GetRecords<teamDto>().ToList();
            foreach (teamDto record in records)
            {
                Team team = new()
                {
                    Name = record.Name,
                    Conference = record.Conference,
                    Win = record.Win,
                    Loss = record.Loss,
                };
                await context.Teams.AddAsync(team);
                teamsByName.Add(record.Name, team);
            }

            await context.SaveChangesAsync();

            return new JsonResult(teamsByName.Count);
        }

        [HttpPost("Players")]
        public async Task<ActionResult> importPlayersAsync()
        {
            Dictionary<string, Team> teams = await context.Teams//.AsNoTracking()
            .ToDictionaryAsync(t => t.Name);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };
            int playerCount = 0;
            using (StreamReader reader = new(_pathNamePlayers))
            using (CsvHelper.CsvReader csv = new(reader, config))
            {
                IEnumerable<playerDto>? records = csv.GetRecords<playerDto>();
                foreach (playerDto record in records)
                {
                    if (!teams.TryGetValue(record.Team, out Team? value))
                    {
                        Console.WriteLine($"Not found country for {record.Name}");
                        return NotFound(record);
                    }

                    Player player = new()
                    {
                        Name = record.Name,
                        Points = (double)record.Points,
                        Rebunds = (double)record.Rebunds,
                        Assists = (double)record.Assists,
                        Minutes = (double)record.Minutes,
                        TeamId = value.Id
                    };
                    context.Players.Add(player);
                    playerCount++;
                }
                await context.SaveChangesAsync();
            }
            return new JsonResult(playerCount);
        }

        [HttpPost("Users")]
        public async Task importUsersAsync()
        {
            //user and admin are going to be seeded
            // user@email.com
            // admin@email.com
            // Passw0rd!

            NbaUser user = new()
            {
                UserName = "user",
                Email = "user@email.com",
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            IdentityResult x = await userManager.CreateAsync(user, "Passw0rd!");

            int y = await context.SaveChangesAsync();

        }
    }
}
