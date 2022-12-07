using FluentValidation;

namespace PhloSales.Server.Features.CustomerFeatures.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(r => r.Name).NotEmpty();
    }
}