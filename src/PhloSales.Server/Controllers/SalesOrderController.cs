using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhloSales.Server.Features.SalesOrderFeatures.CreateSalesOrder;
using PhloSales.Server.Features.SalesOrderFeatures.GetSalesOrder;
using PhloSales.Server.Features.SalesOrderFeatures.ListSalesOrder;

namespace PhloSales.Server.Controllers;

public class SalesOrderController : MethodsController
{
    private readonly ILogger<SalesOrderController> _logger;
    private readonly IMediator _mediatr;

    public SalesOrderController(ILogger<SalesOrderController> logger, IMediator mediatr)
    {
        _logger = logger;
        _mediatr = mediatr;
    }

    [HttpGet("salesorder.get", Name = nameof(GetSalesOrder))]
    public async Task<IActionResult> GetSalesOrder([FromQuery] GetSalesOrderQuery query)
    {
        var rst = await _mediatr.Send(query);
        if (rst.IsFailed) { return NotFound($"Sales order with Id {query.Id} not found"); }
        return Ok(rst.Value);
    }

    [HttpGet("salesorder.list", Name = nameof(ListSalesOrder))]
    public async Task<IActionResult> ListSalesOrder([FromQuery] ListSalesOrderQuery query)
    {
        var rst = await _mediatr.Send(query);
        return Ok(rst.Value);
    }

    [HttpPost("salesorder.create", Name = nameof(CreateSalesOrder))]
    public async Task<IActionResult> CreateSalesOrder(List<CreateSalesOrderCommand> commands)
    {
        var command = new CreateSalesOrderCommandList { Request = commands };
        var rst = await _mediatr.Send(command);
        if (rst.IsFailed) { return BadRequest(rst); }

        return CreatedAtAction(nameof(ListSalesOrder), null, null);
    }
}