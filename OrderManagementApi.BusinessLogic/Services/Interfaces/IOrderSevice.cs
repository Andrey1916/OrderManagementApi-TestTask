using OrderManagementApi.BusinessLogic.Dtos;

namespace OrderManagementApi.BusinessLogic.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<OrderDetails?> GetAsync(Guid id);
    Task<Guid> CreateOrderAsync(NewOrder order);
    Task<bool> ChangeOrderStatus(Guid orderId, OrderStatus status);
}