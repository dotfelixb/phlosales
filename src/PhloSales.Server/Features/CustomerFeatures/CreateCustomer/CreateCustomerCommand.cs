using FluentResults;
using MediatR;

namespace PhloSales.Server.Features.CustomerFeatures.CreateCustomer;

public class CreateCustomerCommand : IRequest<Result<int>>
{
    public string Name { get; set; } = null!;
}