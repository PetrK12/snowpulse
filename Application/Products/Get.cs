using Application.Core;
using Application.DataTransferObject;
using AutoMapper;
using Domain.Entities.BusinessEntities;
using Domain.Repository;
using Infrastructure.DataAccess.Specification.Filters;
using MediatR;

namespace Application.Products;

public class Get
{
    public class Query : IRequest<Result<ProductClientDto>>
    {
        public int Id { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, Result<ProductClientDto>>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public Handler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ProductClientDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetEntityWithSpec(new ProductsWithTypesAndBrandsSpecification(request.Id));

            if (product == null) return null;

            return Result<ProductClientDto>.Success(_mapper.Map<Product, ProductClientDto>(product));

        }
    }
}