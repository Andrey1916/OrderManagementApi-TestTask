using Microsoft.EntityFrameworkCore;
using OrderManagementApi.BusinessLogic.Dtos;
using OrderManagementApi.BusinessLogic.Queries;

namespace OrderManagementApi.Infrastructure.Database.Queries;

public class AddNewOrderCommand : IAddNewOrderCommand
{
    public readonly DbContext _dbContext;

    public AddNewOrderCommand(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(NewOrderRequest request, CancellationToken? cancellationToken = null)
    {
        var order = new Entities.Order
        {
            Id              = Guid.NewGuid(),
            CustomerId      = request.CustomerId,
            DeliveryAddress = request.DeliveryAddress,
            Description     = request.Description,
            Status          = (Entities.OrderStatus)request.Status,
        };

        var orderItems = request.OrderItems.Select(
            oi => new Entities.OrderItem
            {
                Count       = oi.Count,
                OrderId     = order.Id,
                Price       = oi.Price,
                ProductName = oi.ProductName
            });

        await _dbContext.AddAsync(order, cancellationToken ?? default);
        await _dbContext.AddRangeAsync(orderItems, cancellationToken: cancellationToken ?? default);

        await _dbContext.SaveChangesAsync(cancellationToken ?? default);

        return order.Id;
    }
}
