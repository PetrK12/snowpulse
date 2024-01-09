using API.Errors;
using Application.DataTransferObject;
using Application.Extensions;
using Application.Orders;
using AutoMapper;
using Domain.Interfaces;
using Domain.Entities.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class OrdersController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto) => HandleResult(await 
        Mediator.Send(new Create.Command { OrderDto = orderDto, User = User }));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser() => HandleResult(await
        Mediator.Send(new GetForUser.Query { User = User}));

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id) => HandleResult(await
        Mediator.Send(new GetForUserById.Query { Id = id, User = User }));

    [HttpGet("deliveryMethods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods() => HandleResult(await
        Mediator.Send(new GetDeliveryMethods.Query()));

}