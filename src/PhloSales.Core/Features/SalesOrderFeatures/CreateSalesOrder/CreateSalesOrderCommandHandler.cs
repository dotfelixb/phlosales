﻿using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using PhloSales.Data;
using PhloSales.Data.Entities;

namespace PhloSales.Core.Features.SalesOrderFeatures.CreateSalesOrder;

public class CreateSalesOrderCommand 
{
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
}

public class CreateSalesOrderCommandList : IRequest<Result<int>>
{
    public List<CreateSalesOrderCommand> Request { get; set; } = null!;
}

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
        if(rst < 1)
        {
            var err = "Not able to persist sales order";
            _logger.LogError(err);
            return Result.Fail<int>(err);
        }

        _logger.LogInformation("Sales order create for {0} items", rst);
        return Result.Ok(rst);
    }
}