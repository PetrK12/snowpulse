using Application.Core;
using Application.DataTransferObject;
using AutoMapper;
using Domain.Entities.BusinessEntities;
using Domain.Repository;
using MediatR;

namespace Application.Basket;

public class Update
{
    public class Command : IRequest<Result<CustomerBasket>>
    {
        public CustomerBasketDto CustomerBasketDto { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<CustomerBasket>>
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;

        public Handler(IBasketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<CustomerBasket>> Handle(Command request, CancellationToken cancellationToken)
        {
            var basket = _mapper.Map<CustomerBasket>(request.CustomerBasketDto);
            var data = await _repository.UpdateBasketAsync(basket);
            if (data == null) return Result<CustomerBasket>.Failure("Failed to update basket");
            return Result<CustomerBasket>.Success(data);
        }
    }
}