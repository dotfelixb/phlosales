using FluentResults;
using MediatR;

namespace PhloSales.Server.Features.SalesOrderFeatures.CreateSalesOrder;

public class CreateSalesOrderCommand
{
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
}

public class CreateSalesOrderCommandList : IRequest<Result<int>>
{
    public List<CreateSalesOrderCommand> Request { get; set; } = null!;
}