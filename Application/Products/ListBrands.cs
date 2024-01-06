using Application.Core;
using Domain.Entities;
using Domain.Repository;
using Infrastructure.DataAccess.Repositories;
using MediatR;

namespace Application.Products;

public class ListBrands
{
    public class Query : IRequest<Result<IReadOnlyList<ProductBrand>>> { }
    public class Handler : IRequestHandler<Query, Result<IReadOnlyList<ProductBrand>>>
    {
        private readonly IRepository<ProductBrand> _repository;

        public Handler(IRepository<ProductBrand> repository) => _repository = repository;

        public async Task<Result<IReadOnlyList<ProductBrand>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var brands =  await _repository.ListAllAsync();
            return Result<IReadOnlyList<ProductBrand>>.Success(brands);
        }
    }
}