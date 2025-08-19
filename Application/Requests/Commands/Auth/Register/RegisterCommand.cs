using MediatR;

namespace Application.Requests.Commands.Auth.Register;

public class RegisterCommand : IRequest<bool>
{
    public required string Username { get; set; }
    public required string Firstname { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
    public string? PhoneNumber { get; set; }
}