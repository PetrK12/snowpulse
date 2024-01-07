using System.Security.Claims;
using Application.Core;
using Application.DataTransferObject;
using Application.Extensions;
using AutoMapper;
using Domain;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User;

public class GetAddress
{
    public class Query : IRequest<Result<AddressDto>>
    {
        public ClaimsPrincipal User { get; set; }
    }
    public class Handler : IRequestHandler<Query, Result<AddressDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public Handler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<AddressDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddress(request.User);
            
            if ((user != null) && (user.Address == null)) return Result<AddressDto>.Success(null);

            return Result<AddressDto>.Success(_mapper.Map<AddressDto>(user.Address));
        }
    }
}