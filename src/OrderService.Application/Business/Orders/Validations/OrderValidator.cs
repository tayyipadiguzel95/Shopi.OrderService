using FluentValidation;
using OrderService.Application.Business.Orders.Requests;

namespace OrderService.Application.Business.Orders.Validations;

/// <summary>
/// OrderValidator
/// </summary>
public class OrderValidator : AbstractValidator<CreateOrderRequest>
{
    public OrderValidator()
    {
        RuleFor(c => c.CustomerName).NotEmpty().WithMessage("");
        RuleFor(c => c.StoreName).NotEmpty().WithMessage("");
        RuleFor(c => c.BrandId).GreaterThan(0).WithMessage("BrandID must be greater than 0").NotNull().WithMessage("");
    }
}