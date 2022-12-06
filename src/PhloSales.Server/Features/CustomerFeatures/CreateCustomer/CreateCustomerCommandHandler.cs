using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using PhloSales.Data;
using PhloSales.Data.Entities;

namespace PhloSales.Server.Features.CustomerFeatures.CreateCustomer;

public class CreateCustomerCommand : IRequest<Result<int>>
{
    public string Name { get; set; }
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<int>>
{
    private readonly ILogger<CreateCustomerCommandHandler> _logger;
    private IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<CreateCustomerCommand,Customer>(request);
        await _unitOfWork.Repository<Customer>().AddAsync(customer);
        var rst = await _unitOfWork.Commit(cancellationToken);
        if (rst < 1)
        {
            var err = "Not able to persist customer";
            _logger.LogError(err);
            return Result.Fail<int>(err);
        }

        _logger.LogInformation("Customer created with Id {0}", customer.Id);
        return Result.Ok(customer.Id);
    }
}