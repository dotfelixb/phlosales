using FluentResults;
using MediatR;

namespace PhloSales.Server.Features.CustomerFeatures.GetCustomer;

public class GetCustomerQuery : IRequest<Result<GetCustomerQueryResult>>
{
    public int Id { get; set; }
}
