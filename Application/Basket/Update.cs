using Application.Core;
using Domain.Entities;
using Domain.Repository;
using MediatR;

namespace Application.Basket;

public class Update
{
    public class Command : IRequest<Result<CustomerBasket>>
    {
        public CustomerBasket CustomerBasket { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<CustomerBasket>>
    {
        private readonly IBasketRepository _repository;

        public Handler(IBasketRepository repository) => _repository = repository;
        
        public async Task<Result<CustomerBasket>> Handle(Command request, CancellationToken cancellationToken)
        {
            var data = await _repository.UpdateBasketAsync(request.CustomerBasket);
            if (data == null) return Result<CustomerBasket>.Failure("Failed to update basket");
            return Result<CustomerBasket>.Success(data);
        }
    }
}