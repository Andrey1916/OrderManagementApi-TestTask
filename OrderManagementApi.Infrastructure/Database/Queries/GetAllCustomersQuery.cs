using Microsoft.EntityFrameworkCore;
using OrderManagementApi.BusinessLogic.Commands;
using OrderManagementApi.BusinessLogic.Dtos;

namespace OrderManagementApi.Infrastructure.Database.Queries;

public class GetAllCustomersQuery : IGetAllCustomersQuery
{
    public readonly DbContext _dbContext;

    public GetAllCustomersQuery(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Customer>> Handle(CancellationToken? cancellationToken = null)
    {
        return await _dbContext.Set<Entities.Customer>()
            .AsNoTracking()
            .Select(c => new Customer
            {
                Id          = c.Id,
                FirstName   = c.FirstName,
                LastName    = c.LastName,
                Email       = c.Email,
                PhoneNumber = c.PhoneNumber
            })
            .ToListAsync();
    }
}
