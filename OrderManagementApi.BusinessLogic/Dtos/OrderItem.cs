using FluentValidation;

namespace OrderManagementApi.BusinessLogic.Dtos;

public record OrderItem
{
    public string ProductName { get; set; } = null!;
    public uint Count { get; set; }
    public decimal Price { get; set; }
}

public class OrderItemValidator : AbstractValidator<OrderItem>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Count)
            .GreaterThan(0u);

        RuleFor(x => x.ProductName)
            .NotEmpty();
    }
}