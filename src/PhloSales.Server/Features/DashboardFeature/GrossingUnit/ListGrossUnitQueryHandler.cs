using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore; 
using PhloSales.Data;
using PhloSales.Data.Entities;
using System.Text.Json;

namespace PhloSales.Server.Features.DashboardFeature.GrossingUnit;

public class ListGrossingUnitQueryResult
{
    public List<SalesUnitQueryReesult> Units { get; set; } = null!;
    public int PageSize { get; set; }
}

public class ListGrossingUnitQuery : IRequest<Result<ListGrossingUnitQueryResult>>
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
}

public class ListGrossUnitQueryHandler : IRequestHandler<ListGrossingUnitQuery, Result<ListGrossingUnitQueryResult>>
{
    private readonly ILogger<ListGrossUnitQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ListGrossUnitQueryHandler(ILogger<ListGrossUnitQueryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ListGrossingUnitQueryResult>> Handle(ListGrossingUnitQuery request, CancellationToken cancellationToken)
    {
        var gu = await _unitOfWork.Repository<Product>()
           .Entities
           .Include(s => s.SalesOrders)
           .OrderByDescending(s => s.SalesOrders.Sum(t => t.Price))
           .Skip(request.Offset)
           .Take(request.Limit)
           .Select(s =>
           new SalesUnitQueryReesult
           {
               Product = s.Name,
               ProductId = s.Id,
               Unit = s.SalesOrders.Count,
               Gross = s.SalesOrders.Sum(t => t.Price)
           })
           .ToListAsync(cancellationToken: cancellationToken);

        var result = new ListGrossingUnitQueryResult
        {
            Units = gu,
            PageSize = gu.Count
        };

        _logger.LogInformation("{0}", JsonSerializer.Serialize(result));
        return Result.Ok(result);
    }
}