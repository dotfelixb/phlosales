using AutoMapper;
using FluentResults;
using MediatR;
using PhloSales.Data;
using PhloSales.Data.Entities;

namespace PhloSales.Server.Features.SalesOrderFeatures.CreateSalesOrder;

public class CreateSalesOrderCommandHandler : IRequestHandler<CreateSalesOrderCommandList, Result<int>>
{
    private readonly ILogger<CreateSalesOrderCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSalesOrderCommandHandler(ILogger<CreateSalesOrderCommandHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateSalesOrderCommandList request, CancellationToken cancellationToken)
    {
        var orders = _mapper.Map<List<CreateSalesOrderCommand>, List<SalesOrder>>(request.Request);
        await _unitOfWork.Repository<SalesOrder>().AddAsync(orders);
        var rst = await _unitOfWork.Commit(cancellationToken);
        if (rst < 1)
        {
            var err = "Not able to persist sales order";
            _logger.LogError(err);
            return Result.Fail<int>(err);
        }

        _logger.LogInformation("Sales order create for {0} items", rst);
        return Result.Ok(rst);
    }
}