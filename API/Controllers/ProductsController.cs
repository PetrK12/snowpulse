using API.Errors;
using Application.Core;
using Application.DataTransferObject;
using Application.Products;
using Domain.Entities;
using Infrastructure.DataAccess.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductClientDto>>> GetProducts([FromQuery] ProductSpecificationParams productParams)
            => HandleResult(await Mediator.Send(new List.Query { ProductParams = productParams}));
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductClientDto>> GetProduct(int id) => HandleResult(await 
            Mediator.Send(new Get.Query { Id = id }));
      
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() =>
            HandleResult(await Mediator.Send(new ListBrands.Query()));
        
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() => 
            HandleResult(await Mediator.Send(new ListTypes.Query()));
    }
}
