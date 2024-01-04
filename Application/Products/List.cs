using Application.Core;
using Domain.Entities;
using Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

namespace Application.Products;

public class List
{
    public class Query : IRequest<Result<IEnumerable<Product>>>
    {
        
    }

    public class Handler : IRequestHandler<Query, Result<IEnumerable<Product>>>
    {
        private readonly IProductRepository _repository;

        public Handler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<Product>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetProductAsync();

            if (products == null) return null;

            return Result<IEnumerable<Product>>.Success(products);
        }
    }
}