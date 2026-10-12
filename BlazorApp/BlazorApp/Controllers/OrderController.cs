using System.Security.Claims;
using Application.Requests.Commands.Order.Add;
using Application.Requests.Queries.Orders.GetOrderById;
using Application.Requests.Queries.Orders.GetOrdersByUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("my")]
    [Authorize]
    public async Task<ActionResult> GetMyOrders()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var result = await _mediator.Send(new GetOrdersByUserQuery { UserId = userId });
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery { Id = id });
        if (result == null)
            return NotFound();

        // Verify user owns this order (unless admin)
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!User.IsInRole("Admin") && userIdClaim != result.UserId.ToString())
            return Forbid();

        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<int>> Add([FromBody] AddOrderCommand command)
    {
        // Set the user ID from the JWT token
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        command.UserId = userId;
        return await _mediator.Send(command);
    }
}
