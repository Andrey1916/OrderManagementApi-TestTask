namespace OrderManagementApi.Models;

public record NewOrderStatus
{
    public OrderStatus NewStatus { get; set; }
}
