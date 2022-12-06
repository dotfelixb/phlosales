using FluentValidation;

namespace PhloSales.Server.Features.CustomerFeatures.GetCustomer;

public class GetCustomerQueryValidator:AbstractValidator<GetCustomerQuery>
{
	public GetCustomerQueryValidator()
	{
		RuleFor(r => r.Id).NotEmpty().GreaterThan(0);
	}
}