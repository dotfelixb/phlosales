using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PhloSales.Core;
using PhloSales.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PhloSales.Auth.Features.UserFeatures.CreateToken;

public class CreateTokenCommandResult
{
    public string AccessToken { get; set; } = null!;
    public FailedType FailedType { get; set; }
}

public class CreateTokenCommand : IRequest<Result<CreateTokenCommandResult>>
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, Result<CreateTokenCommandResult>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateTokenCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<CreateTokenCommandResult>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user == null)
        {
            return Result.Fail("Bad credentials"); // user does not exist
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
        {
            return Result.Fail("Bad credentials");
        }

        var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Name, user.NormalizedUserName),
            new(JwtRegisteredClaimNames.Email, user.NormalizedEmail)
        };

        var bearerKey = Environment.GetEnvironmentVariable("BEARER_KEY")
             ?? throw new ArgumentNullException($"BEARER_KEY env not provided");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bearerKey)),
                SecurityAlgorithms.HmacSha512Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var result = new CreateTokenCommandResult
        {
            AccessToken = tokenHandler.WriteToken(token),
            FailedType = FailedType.None
        };

        return Result.Ok(result);
    }
}