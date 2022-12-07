using FluentResults;
using MediatR;

namespace PhloSales.Server.Features.SalesOrderFeatures.GetSalesOrder;

public class GetSalesOrderQuery : IRequest<Result<GetSalesOrderQueryResult>>
{
    public int Id { get; set; }
}