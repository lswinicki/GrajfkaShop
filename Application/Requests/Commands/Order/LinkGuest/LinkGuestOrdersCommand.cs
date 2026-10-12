using MediatR;

namespace Application.Requests.Commands.Order.LinkGuest;

/// <summary>
/// Links guest orders (by email) to a registered user after login/registration.
/// </summary>
public class LinkGuestOrdersCommand : IRequest<int>
{
    public required int UserId { get; set; }
    public required string Email { get; set; }
}
