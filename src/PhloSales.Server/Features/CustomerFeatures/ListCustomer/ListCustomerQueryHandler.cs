using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using PhloSales.Data;
using PhloSales.Data.Entities;

namespace PhloSales.Server.Features.CustomerFeatures.ListCustomer;

public class ListCustomerQueryResult
{
    public List<CustomerQueryResult> Customers { get; set; }
    public int PageSize { get; set; }
}

public class ListCustomerQuery : IRequest<Result<ListCustomerQueryResult>>
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
}

public class ListCustomerQueryHandler : IRequestHandler<ListCustomerQuery, Result<ListCustomerQueryResult>>
{
    private readonly ILogger<ListCustomerQueryHandler> _logger;
    private IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ListCustomerQueryHandler(ILogger<ListCustomerQueryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ListCustomerQueryResult>> Handle(ListCustomerQuery request, CancellationToken cancellationToken)
    {
        var cs = await _unitOfWork.Repository<Customer>().GetAsync();
        var customers = _mapper.Map<List<Customer>, List<CustomerQueryResult>>(cs);
        var customersCount = customers.Count;
        var result = new ListCustomerQueryResult
        {
            Customers = customers,
            PageSize = customersCount
        };

        _logger.LogInformation("Found {0} customers", customersCount);
        return Result.Ok(result);
    }
}