using FluentValidation;

namespace PhloSales.Server.Features.SalesOrderFeatures.CreateSalesOrder;

public class CreateSalesOrderCommandValidator : AbstractValidator<CreateSalesOrderCommand>
{
    public CreateSalesOrderCommandValidator()
    {
        RuleFor(r => r.CustomerId).NotEmpty().GreaterThan(0);
        RuleFor(r => r.ProductId).NotEmpty().GreaterThan(0);
        RuleFor(r => r.Price).NotEmpty().GreaterThan(0);
    }
}

public class CreateSalesOrderCommandListValidator : AbstractValidator<CreateSalesOrderCommandList>
{
    public CreateSalesOrderCommandListValidator()
    {
        RuleForEach(x => x.Request).SetValidator(new CreateSalesOrderCommandValidator());
    }
}