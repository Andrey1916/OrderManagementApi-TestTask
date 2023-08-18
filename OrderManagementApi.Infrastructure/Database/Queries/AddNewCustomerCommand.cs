using Microsoft.EntityFrameworkCore;
using OrderManagementApi.BusinessLogic.Dtos;
using OrderManagementApi.BusinessLogic.Queries;
using OrderManagementApi.Infrastructure.Database.Entities;

namespace OrderManagementApi.Infrastructure.Database.Queries;

public class AddNewCustomerCommand : IAddNewCustomerCommand
{
    public readonly DbContext _dbContext;

    public AddNewCustomerCommand(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(NewCustomer request, CancellationToken? cancellationToken = null)
    {
        var customer = new Entities.Customer
        {
            Id          = Guid.NewGuid(),
            Email       = request.Email,
            FirstName   = request.FirstName,
            LastName    = request.LastName,
            PhoneNumber = request.PhoneNumber
        };

        await _dbContext.AddAsync(customer, cancellationToken ?? default);

        await _dbContext.SaveChangesAsync(cancellationToken ?? default);
    }
}
