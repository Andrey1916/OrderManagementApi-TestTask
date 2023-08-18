using OrderManagementApi.BusinessLogic.Commands;
using OrderManagementApi.BusinessLogic.Dtos;

namespace OrderManagementApi.BusinessLogic.Queries;

public interface IAddNewOrderCommand : ICommandHandler<NewOrderRequest, Guid>
{ }

public record NewOrderRequest
{
    public string? Description { get; set; }
    public string DeliveryAddress { get; set; } = null!;

    public Guid CustomerId { get; set; }

    public OrderStatus Status { get; set; }

    public IEnumerable<OrderItem> OrderItems { get; set; } = null!;
}