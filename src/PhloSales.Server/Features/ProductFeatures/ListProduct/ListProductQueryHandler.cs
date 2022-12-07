using AutoMapper;
using FluentResults;
using MediatR;
using PhloSales.Data;
using PhloSales.Data.Entities;

namespace PhloSales.Server.Features.ProductFeatures.ListProduct;

public class ListProductQueryResult
{
    public List<ProductQueryResult> Products { get; set; } = null!;
    public int PageSize { get; set; }
}

public class ListProductQuery : IRequest<Result<ListProductQueryResult>>
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
}

public class ListProductQueryHandler : IRequestHandler<ListProductQuery, Result<ListProductQueryResult>>
{
    private readonly ILogger<ListProductQueryHandler> _logger;
    private IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ListProductQueryHandler(ILogger<ListProductQueryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ListProductQueryResult>> Handle(ListProductQuery request, CancellationToken cancellationToken)
    {
        var ps = await _unitOfWork.Repository<Product>().GetAsync();
        var products = _mapper.Map<List<Product>, List<ProductQueryResult>>(ps);
        var productsCount = products.Count;
        var result = new ListProductQueryResult
        {
            Products = products,
            PageSize = productsCount
        };

        _logger.LogInformation("Found {0} products", productsCount);
        return Result.Ok(result);
    }
}