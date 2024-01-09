using Domain.Entities.BusinessEntities.Identity;

namespace Domain.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}