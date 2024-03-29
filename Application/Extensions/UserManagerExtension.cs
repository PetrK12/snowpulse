using System.Security.Claims;
using Domain.Entities.BusinessEntities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Extensions;

public static class UserManagerExtension
{
    public static async Task<AppUser> FindUserByClaimsPrincipleWithAddress(this UserManager<AppUser> userManager,
        ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email);
        return await userManager.Users.Include(x => x.Address)
            .SingleOrDefaultAsync(x => x.Email == email);
    }

    public static async Task<AppUser> FindByEmailFromClaimsPrincipal(this UserManager<AppUser> userManager,
        ClaimsPrincipal user)
    {
        return await userManager.Users.SingleOrDefaultAsync(x => x.Email == user
            .FindFirstValue(ClaimTypes.Email));
    }
}