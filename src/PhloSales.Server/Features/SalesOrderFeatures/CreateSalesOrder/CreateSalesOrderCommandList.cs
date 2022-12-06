using FluentResults;
using MediatR;

namespace PhloSales.Server.Features.SalesOrderFeatures.CreateSalesOrder;

public class CreateSalesOrderCommandList : IRequest<Result<int>>
{
    public List<CreateSalesOrderCommand> Request { get; set; } = null!;
}
