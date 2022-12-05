using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhloSales.Core.Features.CustomerFeatures.CreateCustomer;
using PhloSales.Core.Features.CustomerFeatures.GetCustomer;
using PhloSales.Core.Features.CustomerFeatures.ListCustomer;

namespace PhloSales.Server.Controllers;

public class CustomerController : MethodsController
{
    private readonly ILogger<CustomerController> _logger;
    private readonly IMediator _mediatr;

    public CustomerController(ILogger<CustomerController> logger, IMediator mediatr)
    {
        _logger = logger;
        _mediatr = mediatr;
    }

    [HttpGet("customer.get", Name = nameof(GetCustomer))]
    public async Task<IActionResult> GetCustomer([FromQuery] GetCustomerQuery query)
    {
        var rst = await _mediatr.Send(query);
        if (rst.IsFailed) { return NotFound($"Customer with Id {query.Id} not found"); }
        return Ok(rst.Value);
    }

    [HttpGet("customer.list", Name = nameof(ListCustomer))]
    public async Task<IActionResult> ListCustomer([FromQuery] ListCustomerQuery query)
    {
        var rst = await _mediatr.Send(query);
        return Ok(rst.Value);
    }

    [HttpPost("customer.create", Name = nameof(CreateCustomer))]
    public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
    {
        var rst = await _mediatr.Send(command);
        if (rst.IsFailed) { return BadRequest(rst); }

        var routeData = new { id = rst.Value };
        return CreatedAtAction(actionName: nameof(GetCustomer), routeValues: routeData, value: routeData);
    }
}
