using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhloSales.Server.Features.ProductFeatures.CreateProduct;
using PhloSales.Server.Features.ProductFeatures.GetProduct;
using PhloSales.Server.Features.ProductFeatures.ListProduct;

namespace PhloSales.Server.Controllers;

public class ProductController : MethodsController
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediatr;

    public ProductController(ILogger<ProductController> logger, IMediator mediatr)
    {
        _logger = logger;
        _mediatr = mediatr;
    }

    [HttpGet("product.get", Name = nameof(GetProduct))]
    public async Task<IActionResult> GetProduct([FromQuery] GetProductQuery query)
    {
        var rst = await _mediatr.Send(query);
        if (rst.IsFailed) { return NotFound($"Product with Id {query.Id} not found"); }
        return Ok(rst.Value);
    }

    [HttpGet("product.list", Name = nameof(ListProduct))]
    public async Task<IActionResult> ListProduct([FromQuery] ListProductQuery query)
    {
        var rst = await _mediatr.Send(query);
        return Ok(rst.Value);
    }

    [HttpPost("product.create", Name = nameof(CreateProduct))]
    public async Task<IActionResult> CreateProduct(CreateProductCommand command)
    {
        var rst = await _mediatr.Send(command);
        if (rst.IsFailed) { return BadRequest(rst); }

        var routeData = new { id = rst.Value };
        return CreatedAtAction(actionName: nameof(GetProduct), routeValues: routeData, value: routeData);
    }
}