using Application.Core;
using Application.DataTransferObject;
using Domain;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User;

public class Register
{
    public class Command : IRequest<Result<UserDto>>
    {
        public RegisterDto RegisterDto { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<UserDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public Handler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<Result<UserDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                DisplayName = request.RegisterDto.Displayname,
                Email = request.RegisterDto.Email,
                UserName = request.RegisterDto.Email
            };

            var result = await _userManager.CreateAsync(user, request.RegisterDto.Password);

            if (!result.Succeeded) return Result<UserDto>.Failure("BadRequest");

            return Result<UserDto>.Success(new UserDto
            {
                Displayname = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            });
        }
    }
}