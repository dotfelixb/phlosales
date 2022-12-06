using AutoMapper;
using FluentResults;
using MediatR; 
using PhloSales.Data;
using PhloSales.Data.Entities;

namespace PhloSales.Server.Features.ProductFeatures.CreateProduct;

public class CreateProductCommand : IRequest<Result<int>>
{
    public string Name { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
{
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<CreateProductCommand, Product>(request);
        await _unitOfWork.Repository<Product>().AddAsync(product);
        var rst = await _unitOfWork.Commit(cancellationToken);
        if (rst < 1)
        {
            var err = "Not able to persist product";
            _logger.LogError(err);
            return Result.Fail<int>(err);
        }

        _logger.LogInformation("Product created with Id {0}", product.Id);
        return Result.Ok(product.Id);
    }
}
