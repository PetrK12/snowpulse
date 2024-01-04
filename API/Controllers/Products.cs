using Application.DataTransferObject;
using Application.Products;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products : BaseController
    {
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductClientDto>>> GetProducts() => HandleResult(await
            Mediator.Send(new List.Query { }));
        
    // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductClientDto>> GetProduct(int id) => HandleResult(await 
            Mediator.Send(new Get.Query { Id = id }));
        
    }
}
