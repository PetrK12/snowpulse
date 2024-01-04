using System.Collections;
using Application.Core;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Products;

public class List
{
    public class Query : IRequest<Result<IEnumerable<Product>>>
    {
        
    }

    public class Handler : IRequestHandler<Query, Result<IEnumerable<Product>>>
    {
        private readonly StoreDbContext _context;

        public Handler(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Product>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var products = await _context.Products.ToListAsync();

            if (products == null) return null;

            return Result<IEnumerable<Product>>.Success(products);
        }
    }
}