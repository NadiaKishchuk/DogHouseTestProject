using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace DogsHouse.WebApi.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator => _mediator ??=
        HttpContext.RequestServices.GetService<IMediator>()!;

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return (result.Value is null) ?
                NotFound("Found result matching null") : Ok(result.Value);
        }

        return BadRequest(result.Reasons);
    }
}