using API.Errors;
using Application.Basket;
using Application.DataTransferObject;
using Domain.Entities.BusinessEntities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BasketController : BaseController
{
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task DeleteBasket(string id)
    => HandleResult(await Mediator.Send(new Delete.Command { BasketId = id}));
    
    [HttpPost]
    [ProducesResponseType(typeof(CustomerBasket),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
    => 
        HandleResult(await Mediator.Send(new Update.Command { CustomerBasketDto = basket}));
    
    [HttpGet]
    [ProducesResponseType(typeof(CustomerBasket),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        => HandleResult(await Mediator.Send(new Get.Query { BasketId = id}));
}