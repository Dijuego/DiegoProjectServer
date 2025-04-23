using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace DiegoProjectServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(NbaPlayersContext context, IHostEnvironment environment,
        UserManager<NbaUser> userManager) : ControllerBase
    {
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
