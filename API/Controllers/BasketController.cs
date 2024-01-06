using API.Errors;
using Application.Basket;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BasketController : BaseController
{
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task DeleteBasket([FromQuery]string id)
    => HandleResult(await Mediator.Send(new Delete.Command { BasketId = id}));
    
    [HttpPost]
    [ProducesResponseType(typeof(CustomerBasket),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
    => 
        HandleResult(await Mediator.Send(new Update.Command { CustomerBasket = basket}));
    
    [HttpGet]
    [ProducesResponseType(typeof(CustomerBasket),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
        => HandleResult(await Mediator.Send(new Get.Query { BasketId = basketId}));
}