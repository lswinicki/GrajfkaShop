using Application.Requests.Commands.Auth.Models;
using MediatR;

namespace Application.Requests.Commands.Auth.Login;

public class LoginCommand : IRequest<LoginResult?>
{
    public string Email { get; set; }
    public string Password { get; set; }
}