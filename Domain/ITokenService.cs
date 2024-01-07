using Domain.Entities.Identity;

namespace Domain;

public interface ITokenService
{
    string CreateToken(AppUser user);
}