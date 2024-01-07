using System.Security.Claims;
using Application.Core;
using Application.DataTransferObject;
using Application.Extensions;
using Domain;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User;

public class LoggedInUser
{
    public class Query : IRequest<Result<UserDto>>
    {
        public ClaimsPrincipal User { get; set; }
    }
    public class Handler : IRequestHandler<Query, Result<UserDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public Handler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(request.User);
            if (user == null) return null;
            
            return Result<UserDto>.Success(new UserDto
            {
                Email = user.Email,
                Displayname = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            });
        }
    }
}