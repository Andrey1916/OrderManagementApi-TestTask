using Microsoft.EntityFrameworkCore;
using OrderManagementApi.BusinessLogic.Dtos;
using OrderManagementApi.BusinessLogic.Queries;

namespace OrderManagementApi.Infrastructure.Database.Queries;

public class GetAllOrdersQuery : IGetAllOrdersQuery
{
    public readonly DbContext _dbContext;

    public GetAllOrdersQuery(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Order>> Handle(CancellationToken? cancellationToken = null)
    {
        return await _dbContext.Set<Entities.Order>()
            .AsNoTracking()
            .Include(o => o.OrderItems)
            .Select(o => new Order
            {
                CreateDate      = o.CreateDate,
                CustomerId      = o.CustomerId,
                DeliveryAddress = o.DeliveryAddress,
                Description     = o.Description,
                Id              = o.Id,
                Status          = (OrderStatus)o.Status,
                UpdateDate      = o.UpdateDate,
                TotalPrice      = o.OrderItems.Sum(
                    oi => oi.Count * oi.Price
                    )
            })
            .ToListAsync();
    }
}
