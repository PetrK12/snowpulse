using Application.Payment;
using Domain.Entities.BusinessEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PaymentController : BaseController
{

    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId) => HandleResult(
        await Mediator.Send(new CreateIntent.Command { BasketId = basketId }));
}