using API.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseController
{
    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
        return NotFound(new ApiResponse(404));
    }
    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
        throw new NullReferenceException();
        return Ok();
    }
    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }
    [HttpGet("badrequest/{id}")]
    public ActionResult GetServerError(int id)
    {
        return Ok();
    }

    [HttpGet("testauth")]
    [Authorize]
    public ActionResult<string> GetSecretText()
    {
        return "secret stuff";
    }
}