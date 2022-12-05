using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhloSales.Core.Features.DashboardFeature.GrossingUnit;
using PhloSales.Data;
using PhloSales.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhloSales.Core.Features.DashboardFeature.OrderedUnit;

public class ListOrderedUnitQueryResult
{
    public List<SalesUnitQueryReesult> Units { get; set; } = null!;
    public int PageSize { get; set; }
}

public class ListOrderedUnitQuery : IRequest<Result<ListOrderedUnitQueryResult>>
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
}

public class ListOrderedUnitQueryHandler : IRequestHandler<ListOrderedUnitQuery, Result<ListOrderedUnitQueryResult>>
{
    private readonly ILogger<ListOrderedUnitQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ListOrderedUnitQueryHandler(ILogger<ListOrderedUnitQueryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ListOrderedUnitQueryResult>> Handle(ListOrderedUnitQuery request, CancellationToken cancellationToken)
    {
        var ou = await _unitOfWork.Repository<Product>()
           .Entities
           .Include(s => s.SalesOrders)
           .Skip(request.Offset)
           .Take(request.Limit)
           .Select(s =>
           new SalesUnitQueryReesult
           {
               Product = s.Name,
               ProductId = s.Id,
               Unit = s.SalesOrders.Count,
               Gross = s.SalesOrders.Sum(t => t.Price),
               HighestPrice = s.SalesOrders.DefaultIfEmpty().Max(t => t == null ? 0 : t.Price),
               LowestPrice = s.SalesOrders.DefaultIfEmpty().Min(t => t == null ? 0 : t.Price),
           })
           .ToListAsync(cancellationToken: cancellationToken);

        var result = new ListOrderedUnitQueryResult
        {
            Units = ou,
            PageSize = ou.Count
        };

        _logger.LogInformation("{0}", JsonSerializer.Serialize(result));
        return Result.Ok(result);
    }
}
