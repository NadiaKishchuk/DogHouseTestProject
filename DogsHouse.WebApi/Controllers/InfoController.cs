using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DogsHouse.WebApi.Controllers;
[Route("[action]")]
public class InfoController : BaseController
{
    private readonly IConfiguration _configuration;
    public InfoController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpGet]
    public IActionResult Ping()
    {
        return HandleResult(Result.Ok(_configuration["Messages:Default"]));
    }

}