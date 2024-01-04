using Application.Core;
using Domain.Entities;
using Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

namespace Application.Products;

public class Get
{
    public class Query : IRequest<Result<Product>>
    {
        public int Id { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, Result<Product>>
    {
        private readonly IProductRepository _repository;

        public Handler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Product>> Handle(Query request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductByIdAsync(request.Id);

            if (product == null) return null;

            return Result<Product>.Success(product);

        }
    }
}