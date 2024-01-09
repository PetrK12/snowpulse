using Application.Core;
using Domain.Entities.OrderAggregate;
using Domain.Interfaces;
using MediatR;

namespace Application.Orders;

public class GetDeliveryMethods
{
    public class Query : IRequest<Result<IReadOnlyList<DeliveryMethod>>>{}
    public class Command : IRequestHandler<Query, Result<IReadOnlyList<DeliveryMethod>>>
    {
        private readonly IOrderService _orderService;

        public Command(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Result<IReadOnlyList<DeliveryMethod>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            return Result<IReadOnlyList<DeliveryMethod>>.Success(deliveryMethods);
        }
    }
}