using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Requests.Commands.Order.LinkGuest;

public class LinkGuestOrdersHandler(IOrderRepository orderRepository)
    : IRequestHandler<LinkGuestOrdersCommand, int>
{
    public async Task<int> Handle(LinkGuestOrdersCommand request, CancellationToken cancellationToken)
    {
        await orderRepository.LinkGuestOrdersToUserAsync(request.Email, request.UserId, cancellationToken);
        return request.UserId;
    }
}
