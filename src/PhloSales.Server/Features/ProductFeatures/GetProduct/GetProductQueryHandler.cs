using AutoMapper;
using FluentResults;
using MediatR; 
using PhloSales.Data;
using PhloSales.Data.Entities;

namespace PhloSales.Server.Features.ProductFeatures.GetProduct;

public class GetProductQueryResult : ProductQueryResult
{
}

public class GetProductQuery : IRequest<Result<GetProductQueryResult>>
{
    public int Id { get; set; }
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<GetProductQueryResult>>
{
    private readonly ILogger<GetProductQueryHandler> _logger;
    private IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(ILogger<GetProductQueryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetProductQueryResult>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var p = await _unitOfWork.Repository<Product>().GetAsync(request.Id);
        if (p == null)
        {
            return Result.Fail("Product not found!");
        }

        var product = _mapper.Map<Product, GetProductQueryResult>(p);
        _logger.LogInformation("Product with Id {0} found", product.Id);
        return Result.Ok(product);
    }
}
