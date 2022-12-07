using AutoMapper;
using FluentResults;
using MediatR;
using PhloSales.Data;
using PhloSales.Data.Entities;

namespace PhloSales.Server.Features.SalesOrderFeatures.GetSalesOrder;

public class GetSalesOrderQueryHandler : IRequestHandler<GetSalesOrderQuery, Result<GetSalesOrderQueryResult>>
{
    private readonly ILogger<GetSalesOrderQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSalesOrderQueryHandler(ILogger<GetSalesOrderQueryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetSalesOrderQueryResult>> Handle(GetSalesOrderQuery request, CancellationToken cancellationToken)
    {
        var s = await _unitOfWork.Repository<SalesOrder>().GetAsync(request.Id);
        if (s == null)
        {
            return Result.Fail("Sales order not found!");
        }

        s.Customer = await _unitOfWork.Repository<Customer>().GetAsync(s.CustomerId);
        s.Product = await _unitOfWork.Repository<Product>().GetAsync(s.ProductId);
        var salesOrder = _mapper.Map<SalesOrder, GetSalesOrderQueryResult>(s);
        _logger.LogInformation("Sales order with Id {0} found", salesOrder.Id);
        return Result.Ok(salesOrder);
    }
}