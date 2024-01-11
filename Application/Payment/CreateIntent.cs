using Application.Core;
using Domain.Entities.BusinessEntities;
using Domain.Interfaces;
using MediatR;

namespace Application.Payment;

public class CreateIntent
{
    public class Command : IRequest<Result<CustomerBasket>>
    {
        public string BasketId { get; set; }
    }
    public class Handler : IRequestHandler<Command, Result<CustomerBasket>>
    {
        private readonly IPaymentService _paymentService;

        public Handler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        public async Task<Result<CustomerBasket>> Handle(Command request, CancellationToken cancellationToken)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(request.BasketId);
            if (basket == null) return Result<CustomerBasket>.Failure("Problem with your basket");
            return Result<CustomerBasket>.Success(basket);
        }
    }
}