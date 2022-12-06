using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore; 
using PhloSales.Data;
using PhloSales.Data.Entities;
using System.Text.Json;

namespace PhloSales.Server.Features.DashboardFeature.SellingUnit;

public class ListSellingUnitQueryResult
{
    public List<SalesUnitQueryReesult> Units { get; set; } = null!;
    public int PageSize { get; set; }
}

public class ListSellingUnitQuery : IRequest<Result<ListSellingUnitQueryResult>>
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
}

public class ListSellingUnitQueryHandler : IRequestHandler<ListSellingUnitQuery, Result<ListSellingUnitQueryResult>>
{
    private readonly ILogger<ListSellingUnitQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ListSellingUnitQueryHandler(ILogger<ListSellingUnitQueryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ListSellingUnitQueryResult>> Handle(ListSellingUnitQuery request, CancellationToken cancellationToken)
    {
        var su = await _unitOfWork.Repository<Product>()
            .Entities
            .Include(s => s.SalesOrders)
            .OrderByDescending(s => s.SalesOrders.Count)
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

        var result = new ListSellingUnitQueryResult
        {
            Units = su,
            PageSize = su.Count
        };

        _logger.LogInformation("{0}", JsonSerializer.Serialize(result));
        return Result.Ok(result);
    }
}