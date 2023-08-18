using Microsoft.EntityFrameworkCore;
using OrderManagementApi.BusinessLogic.Dtos;
using OrderManagementApi.BusinessLogic.Queries;

namespace OrderManagementApi.Infrastructure.Database.Queries;

public class GetOrderDetailsQuery : IGetOrderDetailsQuery
{
    public readonly DbContext _dbContext;

    public GetOrderDetailsQuery(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderDetails?> Handle(GetOrderDetailsRequest request, CancellationToken? cancellationToken = null)
    {
        return await _dbContext.Set<Entities.Order>()
            .AsNoTracking()
            .Include(o => o.OrderItems)
            .Select(o => new OrderDetails
            {
                Id              = o.Id,
                CreateDate      = o.CreateDate,
                CustomerId      = o.CustomerId,
                DeliveryAddress = o.DeliveryAddress,
                Description     = o.Description,
                UpdateDate      = o.UpdateDate,
                Status          = (OrderStatus)o.Status,
                OrderItems      = o.OrderItems.Select(
                    oi => new OrderItem
                    {
                        Count       = oi.Count,
                        Price       = oi.Price,
                        ProductName = oi.ProductName
                    })
            })
            .FirstOrDefaultAsync(o => o.Id == request.OrderId);
    }
}
