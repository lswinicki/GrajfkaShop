using Application.Requests.Commands.Auth.Login;
using Application.Requests.Commands.Auth.Models;
using Application.Requests.Commands.Auth.Register;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly SignInManager<UserDao> _signInManager;

    public AuthController(IMediator mediator, SignInManager<UserDao> signInManager)
    {
        _mediator = mediator;
        _signInManager = signInManager;
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

    [HttpGet]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        return Redirect("/");
    }
}