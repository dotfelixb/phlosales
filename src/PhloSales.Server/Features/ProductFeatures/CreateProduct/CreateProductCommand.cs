using FluentResults;
using MediatR;

namespace PhloSales.Server.Features.ProductFeatures.CreateProduct;

public class CreateProductCommand : IRequest<Result<int>>
{
    public string Name { get; set; } = null!;
}
