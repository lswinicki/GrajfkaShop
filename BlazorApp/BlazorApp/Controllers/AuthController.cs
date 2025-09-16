using Application.Requests.Commands.Auth.Login;
using Application.Requests.Commands.Auth.Models;
using Application.Requests.Commands.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult<bool>> Register([FromBody] RegisterCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<LoginResult?>> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
}