

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using webapi.core.IFeaturModule;
using webapi.core.ioc;

namespace webapi.Features.Pizzas;
public class Login : IFeatureModule
{
    public record LoginRequest(string Name, string Password);
    public interface IService
    {
        string Handle(LoginRequest request);
    }

    [Injectable]
    public class Service : IService
    {
        public string Handle(LoginRequest request)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret_key123456789999999999999999999999999999"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: [new Claim(JwtRegisteredClaimNames.Sub, request.Name)],
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", (IService service, [FromBody] LoginRequest request) =>
        {
            if (request.Name == "tomas" && request.Password == "123")
            {
                var token = service.Handle(request);
                return Results.Ok(token);
            }
            else return Results.Unauthorized();
        }).AllowAnonymous();

    }
}   