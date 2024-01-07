using Application.Core;
using Domain.Entities.BusinessEntities;
using Domain.Repository;
using MediatR;

namespace Application.Basket;

public class Get
{
    public class Query : IRequest<Result<CustomerBasket>>
    {
        public string BasketId { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, Result<CustomerBasket>>
    {
        private readonly IBasketRepository _repository;

        public Handler(IBasketRepository repository) => _repository = repository;
        
        public async Task<Result<CustomerBasket>> Handle(Query request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetBasketAsync(request.BasketId);
            if (data == null) return Result<CustomerBasket>.Success(new CustomerBasket { Id = request.BasketId});
            return Result<CustomerBasket>.Success(data);   
        }
    }
}
