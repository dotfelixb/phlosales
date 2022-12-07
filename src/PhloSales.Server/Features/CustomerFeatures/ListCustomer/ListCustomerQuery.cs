using FluentResults;
using MediatR;

namespace PhloSales.Server.Features.CustomerFeatures.ListCustomer;

public class ListCustomerQuery : IRequest<Result<ListCustomerQueryResult>>
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
}