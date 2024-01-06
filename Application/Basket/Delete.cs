using Application.Core;
using Domain.Repository;
using MediatR;

namespace Application.Basket;

public class Delete
{
    public class Command : IRequest<Result<Unit>>
    {
        public string BasketId { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IBasketRepository _repository;

        public Handler(IBasketRepository repository) => _repository = repository;
        
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteBasketAsync(request.BasketId);
            if (deleted == false) return Result<Unit>.Failure("Failed to delete");
            return Result<Unit>.Success(Unit.Value);
        }
    }
}