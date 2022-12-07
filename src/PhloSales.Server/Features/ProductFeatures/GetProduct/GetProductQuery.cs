using FluentResults;
using MediatR;

namespace PhloSales.Server.Features.ProductFeatures.GetProduct;

public class GetProductQuery : IRequest<Result<GetProductQueryResult>>
{
    public int Id { get; set; }
}