using Application.Core;
using Domain.Entities;
using Domain.Repository;
using Infrastructure.DataAccess.Specification.Filters;
using MediatR;

namespace Application.Products;

public class List
{
    public class Query : IRequest<Result<IEnumerable<Product>>>
    {
        
    }

    public class Handler : IRequestHandler<Query, Result<IEnumerable<Product>>>
    {
        private readonly IRepository<Product> _repository;

        public Handler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<Product>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var products = await _repository.ListAsync(new ProductsWithTypesAndBrandsSpecification());

            if (products == null) return null;

            return Result<IEnumerable<Product>>.Success(products);
        }
    }
}