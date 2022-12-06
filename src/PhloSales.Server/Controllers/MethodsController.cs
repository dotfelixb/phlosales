using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhloSales.Server.Controllers;

[ApiController]
[Route("sales")]
[Authorize]
public class MethodsController : ControllerBase
{
}