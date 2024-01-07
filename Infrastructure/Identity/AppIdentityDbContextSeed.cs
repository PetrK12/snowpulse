using Domain.Entities.BusinessEntities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                DisplayName = "Petr",
                Email = "petr@test.com",
                UserName = "petr@test.com",
                Address = new Address
                {
                    FirstName = "Petr",
                    LastName = "Kutil",
                    Street = "Rostislavova",
                    City = "Prague",
                    ZipCode = "14000"
                }
            };
            await userManager.CreateAsync(user, "password");
        }
    }   
}