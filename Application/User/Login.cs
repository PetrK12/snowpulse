using Application.Core;
using Application.DataTransferObject;
using Domain.Interfaces;
using Domain.Entities.BusinessEntities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User;

public class Login
{
    public class Command : IRequest<Result<UserDto>>
    {
        public LoginDto LoginDto { get; set; }
    }
    public class Handler : IRequestHandler<Command, Result<UserDto>>
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public Handler(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
            ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<Result<UserDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.LoginDto.Email);
            if (user == null) return Result<UserDto>.Failure("Unauthorized");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.LoginDto.Password, false);
            if (result.Succeeded)
                return Result<UserDto>.Success(new UserDto
                    { Email = user.Email, Displayname = user.DisplayName, Token = _tokenService.CreateToken(user) });
        
            return Result<UserDto>.Failure("Unauthorized");
        }
    }
}