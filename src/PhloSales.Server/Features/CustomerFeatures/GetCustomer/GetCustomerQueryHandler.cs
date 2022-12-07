using AutoMapper;
using FluentResults;
using MediatR;
using PhloSales.Data;
using PhloSales.Data.Entities;

namespace PhloSales.Server.Features.CustomerFeatures.GetCustomer;

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Result<GetCustomerQueryResult>>
{
    private readonly ILogger<GetCustomerQueryHandler> _logger;
    private IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCustomerQueryHandler(ILogger<GetCustomerQueryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetCustomerQueryResult>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var c = await _unitOfWork.Repository<Customer>().GetAsync(request.Id);
        if (c == null)
        {
            return Result.Fail("Customer not found!");
        }

        var customer = _mapper.Map<Customer, GetCustomerQueryResult>(c);
        _logger.LogInformation("Customer with Id {0} found", customer.Id);
        return Result.Ok(customer);
    }
}