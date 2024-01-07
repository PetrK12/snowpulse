using API.Errors;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??=
        HttpContext.RequestServices.GetService<IMediator>();

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result == null) return NotFound(new ApiResponse(404));

        if (result.Error == "Email used")
        {
            return BadRequest(new ApiValidationErrorResponse
            {
                Errors = new[]
                {
                    "Email address is already in use"
                }
            });
        }

        if (!result.IsSuccess && result.Error == "Unauthorized") return Unauthorized(new ApiResponse(401));

        if (!result.IsSuccess && result.Error == "BadRequest") return BadRequest(new ApiResponse(400));
        
        if (result.IsSuccess && result.Value != null) return Ok(result.Value);

        if (result.IsSuccess && result.Value == null) return NotFound(new ApiResponse(404));
        
        return  BadRequest(new ApiResponse(400));
    }


}