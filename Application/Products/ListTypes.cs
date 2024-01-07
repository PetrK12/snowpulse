using Application.Core;
using Domain.Entities.BusinessEntities;
using Domain.Repository;
using Infrastructure.DataAccess.Repositories;
using MediatR;

namespace Application.Products;

public class ListTypes
{
    public class Query : IRequest<Result<IReadOnlyList<ProductType>>> { }

    public class Handler : IRequestHandler<Query, Result<IReadOnlyList<ProductType>>>
    {
        private readonly IRepository<ProductType> _repository;

        public Handler(IRepository<ProductType> repository) => _repository = repository;
        

        public async Task<Result<IReadOnlyList<ProductType>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var types =  await _repository.ListAllAsync();
            return Result<IReadOnlyList<ProductType>>.Success(types);
        }
        
    }
}