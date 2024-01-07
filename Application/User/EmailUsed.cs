using Application.Core;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User;

public class EmailUsed
{

    public class Query : IRequest<bool>
    {
        public string Email { get; set; }
    }
    public class Handler : IRequestHandler<Query, bool>
    {
        private readonly UserManager<AppUser> _userManager;
        public Handler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _userManager.FindByEmailAsync(request.Email) != null;
        }
    }
}
