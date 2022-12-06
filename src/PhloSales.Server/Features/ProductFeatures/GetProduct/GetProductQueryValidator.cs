using FluentValidation;

namespace PhloSales.Server.Features.ProductFeatures.GetProduct;

public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
        RuleFor(r=> r.Id).NotEmpty().GreaterThan(0);
    }
}
