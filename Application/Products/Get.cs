using Application.Core;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Products;

public class Get
{
    public class Query : IRequest<Result<Product>>
    {
        public int Id { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, Result<Product>>
    {
        private readonly StoreDbContext _context;

        public Handler(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Product>> Handle(Query request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (product == null) return null;

            return Result<Product>.Success(product);

        }
    }
}