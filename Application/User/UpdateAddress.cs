using System.Security.Claims;
using Application.Core;
using Application.DataTransferObject;
using Application.Extensions;
using AutoMapper;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User;

public class UpdateAddress
{
    public class Command : IRequest<Result<AddressDto>>
    {
        public AddressDto AddressDto { get; set; }
        public ClaimsPrincipal User { get; set; }
    }
    public class Handler : IRequestHandler<Command, Result<AddressDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public Handler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<AddressDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddress(request.User);
            if (user == null) return null;
            
            user.Address = _mapper.Map<AddressDto, Address>(request.AddressDto);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Result<AddressDto>.Success(_mapper.Map<Address, AddressDto>(user.Address));

            return Result<AddressDto>.Failure("Failed to update address");
        }
    }
}