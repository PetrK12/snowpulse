using Domain.Entities.BusinessEntities.Identity;

namespace Domain;

public interface ITokenService
{
    string CreateToken(AppUser user);
}