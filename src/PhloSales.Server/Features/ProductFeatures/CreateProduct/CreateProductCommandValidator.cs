using FluentValidation;

namespace PhloSales.Server.Features.ProductFeatures.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(r => r.Name).NotEmpty();
    }
}