using System.Security.Claims;
using Application.Core;
using Application.DataTransferObject;
using Application.Extensions;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Orders;

public class GetForUser
{
    public class Query : IRequest<Result<IReadOnlyList<OrderToReturnDto>>>
    {
        public ClaimsPrincipal User { get; set; }
    }
    public class Handler : IRequestHandler<Query, Result<IReadOnlyList<OrderToReturnDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public Handler(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }
        public async Task<Result<IReadOnlyList<OrderToReturnDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var email = request.User.RetrieveEmailFromPrincipal();
            var orders = await _orderService.GetOrdersForUserAsync(email);
            return Result<IReadOnlyList<OrderToReturnDto>>.Success(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }
    }
}