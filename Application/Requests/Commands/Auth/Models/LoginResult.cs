namespace Application.Requests.Commands.Auth.Models;

public class LoginResult
{
    public required string Token { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
}