using FluentValidation;

namespace PhloSales.Server.Features.SalesOrderFeatures.GetSalesOrder;

public class GetSalesOrderQueryValidator:AbstractValidator<GetSalesOrderQuery>
{
    public GetSalesOrderQueryValidator()
    {
        RuleFor(r => r.Id).NotEmpty().GreaterThan(0);
    }
}
