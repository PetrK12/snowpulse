using Application.Core;
using Application.DataTransferObject;
using AutoMapper;
using Domain.Entities.BusinessEntities;
using Domain.Repository;
using Infrastructure.DataAccess.Specification;
using Infrastructure.DataAccess.Specification.Filters;
using MediatR;

namespace Application.Products;

public class List
{
    public class Query : IRequest<Result<Pagination<ProductClientDto>>>
    {
        public ProductSpecificationParams ProductParams { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<Pagination<ProductClientDto>>>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public Handler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<Pagination<ProductClientDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(request.ProductParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(request.ProductParams);

            var totalItems = await _repository.CountAsync(countSpec);
            var products = await _repository.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductClientDto>>(products);
            
            if (data == null) return null;

            return Result<Pagination<ProductClientDto>>.Success(new Pagination<ProductClientDto>
            {
                PageIndex = request.ProductParams.PageIndex,
                PageSize = request.ProductParams.PageSize,
                Count = totalItems,
                Data = data
            });
        }
    }
}