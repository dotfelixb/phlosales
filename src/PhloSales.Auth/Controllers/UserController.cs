using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhloSales.Auth.Features.UserFeatures.CreateToken;

namespace PhloSales.Auth.Controllers;

public class UserController : MethodsController
{
    private readonly ILogger<UserController> _logger;
    private readonly IMediator _mediatr;

    public UserController(ILogger<UserController> logger, IMediator mediatr)
    {
        _logger = logger;
        _mediatr = mediatr;
    }

    [AllowAnonymous]
    [HttpPost("user.token", Name = nameof(CreateToken))]
    public async Task<IActionResult> CreateToken(CreateTokenCommand command)
    {
        var rst = await _mediatr.Send(command);
        if (rst.IsFailed) { return BadRequest(rst.Reasons.FirstOrDefault()); }
        return Ok(rst.Value);
    }

    [AllowAnonymous]
    [HttpPost("user.refreshtoken", Name = nameof(RefreshToken))]
    public async Task<IActionResult> RefreshToken()
    {
        await Task.CompletedTask;
        return Ok(new { ok = true });
    }
}