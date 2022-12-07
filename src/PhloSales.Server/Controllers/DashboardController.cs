using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhloSales.Server.Features.DashboardFeature.GrossingUnit;
using PhloSales.Server.Features.DashboardFeature.OrderedUnit;
using PhloSales.Server.Features.DashboardFeature.SellingUnit;

namespace PhloSales.Server.Controllers;

public class DashboardController : MethodsController
{
    private readonly ILogger<DashboardController> _logger;
    private readonly IMediator _mediatr;

    public DashboardController(ILogger<DashboardController> logger, IMediator mediatr)
    {
        _logger = logger;
        _mediatr = mediatr;
    }

    [HttpGet("dashboard.selling", Name = nameof(SellingUnit))]
    public async Task<IActionResult> SellingUnit([FromQuery] ListSellingUnitQuery query)
    {
        var rst = await _mediatr.Send(query);
        return Ok(rst.Value);
    }

    [HttpGet("dashboard.grossing", Name = nameof(GrossingUnit))]
    public async Task<IActionResult> GrossingUnit([FromQuery] ListGrossingUnitQuery query)
    {
        var rst = await _mediatr.Send(query);
        return Ok(rst.Value);
    }

    [HttpGet("dashboard.ordered", Name = nameof(OrderedUnit))]
    public async Task<IActionResult> OrderedUnit([FromQuery] ListOrderedUnitQuery query)
    {
        var rst = await _mediatr.Send(query);
        return Ok(rst.Value);
    }
}