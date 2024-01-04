using Application.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products : BaseController
    {
        private readonly StoreDbContext _context;

        public Products(StoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() => HandleResult(await
            Mediator.Send(new List.Query { }));
        
    // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) => HandleResult(await 
            Mediator.Send(new Get.Query { Id = id }));
        
    }
}
