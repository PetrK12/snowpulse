using Application.Core;
using Application.DataTransferObject;
using Application.Products;
using Infrastructure.DataAccess.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductClientDto>>> GetProducts([FromQuery] ProductSpecificationParams productParams)
            => HandleResult(await Mediator.Send(new List.Query { ProductParams = productParams}));
        
    // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductClientDto>> GetProduct(int id) => HandleResult(await 
            Mediator.Send(new Get.Query { Id = id }));
        
    }
}
