using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces.Repositories;
using Application.Requests.Commands.Auth.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Application.Requests.Commands.Auth.Login;

public class LoginHandler(IUserRepository userRepository, UserManager<UserDao> userManager, IConfiguration configuration) : IRequestHandler<LoginCommand, LoginResult?>
{
    public async Task<LoginResult?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user != null && await userManager.CheckPasswordAsync(user, request.Password))
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user?.UserName ?? ""),
                new Claim(ClaimTypes.Email, user?.Email ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user?.Id.ToString() ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var loginResult = new LoginResult()
            {
                Email = request.Email,
                UserName = user!.UserName!,
                Token = tokenString
            };
            
            return loginResult;
        }

        return null;
    }
    
    private JwtSecurityToken GetToken(List<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? ""));

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            expires: DateTime.Now.AddHours(1),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}