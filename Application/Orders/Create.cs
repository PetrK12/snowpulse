using System.Security.Claims;
using Application.Core;
using Application.DataTransferObject;
using Application.Extensions;
using AutoMapper;
using Domain.Entities.OrderAggregate;
using Domain.Interfaces;
using MediatR;

namespace Application.Orders;

public class Create
{
    public class Command : IRequest<Result<Order>>
    {
        public OrderDto OrderDto { get; set; }
        public ClaimsPrincipal User { get; set; }
    }
    public class Handler : IRequestHandler<Command, Result<Order>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public Handler(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        public async Task<Result<Order>> Handle(Command request, CancellationToken cancellationToken)
        {
            var email = request.User.RetrieveEmailFromPrincipal();
            var address = _mapper.Map<AddressDto, Address>(request.OrderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, request.OrderDto.DeliveryMethodId,
                request.OrderDto.BasketId, address);
            
            if (order == null) return Result<Order>.Failure("BadRequest");
            return Result<Order>.Success(order);
        }
    }
}