using Microsoft.EntityFrameworkCore;
using OrderManagementApi.BusinessLogic.Dtos;
using OrderManagementApi.BusinessLogic.Queries;

namespace OrderManagementApi.Infrastructure.Database.Queries;

public class GetCustomerQuery : IGetCustomerQuery
{
    public readonly DbContext _dbContext;

    public GetCustomerQuery(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer?> Handle(GetCustomerRequest request, CancellationToken? cancellationToken = null)
    {
        var customer = await _dbContext.Set<Entities.Customer>()
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(c => c.Id == request.CustomerId);

        return customer == null
            ? null
            : new Customer
            {
                Id          = customer.Id,
                Email       = customer.Email,
                FirstName   = customer.FirstName,
                LastName    = customer.LastName,
                PhoneNumber = customer.PhoneNumber
            };
    }
}
