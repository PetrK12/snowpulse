using Application.DataTransferObject;
using Application.User;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : BaseController
{
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) => HandleResult(await Mediator.Send(new
        Login.Command { LoginDto = loginDto }));

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) => HandleResult(await Mediator.Send(new 
        Register.Command{ RegisterDto = registerDto }));

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser() => HandleResult(await Mediator.Send(new 
        LoggedInUser.Query{ User = User }));

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email) => await Mediator.Send(
        new EmailUsed.Query { Email = email });

    [Authorize]
    [HttpGet("address")]
    public async Task<ActionResult<AddressDto>> GetUserAddress() => HandleResult(await Mediator.Send(
        new GetAddress.Query { User = User }));

    [Authorize]
    [HttpPut("address")]
    public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address) => HandleResult(await
        Mediator.Send(new UpdateAddress.Command { AddressDto = address, User = User}));
}