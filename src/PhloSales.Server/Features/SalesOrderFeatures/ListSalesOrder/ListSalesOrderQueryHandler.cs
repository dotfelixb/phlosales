using AutoMapper;
using FluentResults;
using MediatR;
using PhloSales.Data;
using PhloSales.Data.Entities;

namespace PhloSales.Server.Features.SalesOrderFeatures.ListSalesOrder;

public class ListSalesOrderQueryResult
{
    public List<SalesOrderQueryResult> SalesOrders { get; set; } = null!;
    public int PageSize { get; set; }
}

public class ListSalesOrderQuery : IRequest<Result<ListSalesOrderQueryResult>>
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
}

public class ListSalesOrderQueryHandler : IRequestHandler<ListSalesOrderQuery, Result<ListSalesOrderQueryResult>>
{
    private readonly ILogger<ListSalesOrderQueryHandler> _logger;
    private IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ListSalesOrderQueryHandler(ILogger<ListSalesOrderQueryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ListSalesOrderQueryResult>> Handle(ListSalesOrderQuery request, CancellationToken cancellationToken)
    {
        var so = await _unitOfWork.Repository<SalesOrder>().GetAsync();
        foreach (var order in so)
        {
            order.Customer = await _unitOfWork.Repository<Customer>().GetAsync(order.CustomerId);
            order.Product = await _unitOfWork.Repository<Product>().GetAsync(order.ProductId);
        }
        var salesOrders = _mapper.Map<List<SalesOrder>, List<SalesOrderQueryResult>>(so);
        var salesOrdersCount = salesOrders.Count;
        var result = new ListSalesOrderQueryResult
        {
            SalesOrders = salesOrders,
            PageSize = salesOrdersCount
        };

        _logger.LogInformation("Found {0} products", salesOrdersCount);
        return Result.Ok(result);
    }
}