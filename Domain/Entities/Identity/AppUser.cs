using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.BusinessEntities.Identity;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }
    public Address Address { get; set; }
}