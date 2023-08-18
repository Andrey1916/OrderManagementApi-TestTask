using FluentValidation;

namespace OrderManagementApi.BusinessLogic.Dtos;

public record NewOrder
{
    public string? Description { get; set; }
    public string DeliveryAddress { get; set; } = null!;

    public Guid CustomerId { get; set; }

    public IEnumerable<OrderItem> OrderItems { get; set; } = null!;
}

public class NewOrderValidator : AbstractValidator<NewOrder>
{
    public NewOrderValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty();

        RuleFor(x => x.DeliveryAddress)
            .NotEmpty();

        RuleFor(x => x.OrderItems)
            .NotEmpty();

        RuleForEach(x => x.OrderItems)
            .SetValidator(new OrderItemValidator());
    }
}