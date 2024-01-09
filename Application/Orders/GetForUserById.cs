using System.Security.Claims;
using Application.Core;
using Application.DataTransferObject;
using Application.Extensions;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Orders;

public class GetForUserById
{
    public class Query : IRequest<Result<OrderToReturnDto>>
    {
        public int Id { get; set; }
        public ClaimsPrincipal User { get; set; }
    }
    public class Handler : IRequestHandler<Query, Result<OrderToReturnDto>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public Handler(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }
        public async Task<Result<OrderToReturnDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var email = request.User.RetrieveEmailFromPrincipal();
            var order = await _orderService.GetOrderByIdAsync(request.Id, email);

            if (order == null) return Result<OrderToReturnDto>.Failure(null);

            return Result<OrderToReturnDto>.Success(_mapper.Map<OrderToReturnDto>(order));
        }
    }
}