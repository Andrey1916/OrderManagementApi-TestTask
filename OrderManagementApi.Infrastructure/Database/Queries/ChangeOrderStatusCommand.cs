using Microsoft.EntityFrameworkCore;
using OrderManagementApi.BusinessLogic.Queries;

namespace OrderManagementApi.Infrastructure.Database.Queries;

public class ChangeOrderStatusCommand : IChangeOrderStatusCommand
{
    public readonly DbContext _dbContext;

    public ChangeOrderStatusCommand(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(ChangeOrderStatusRequest request, CancellationToken? cancellationToken = null)
    {
        var order = await _dbContext.Set<Entities.Order>()
            .FirstOrDefaultAsync(o => o.Id == request.OrderId);

        if (order is null)
        {
            return false;
        }

        order.Status = (Entities.OrderStatus)request.Status;

        await _dbContext.SaveChangesAsync();

        return true;
    }
}
