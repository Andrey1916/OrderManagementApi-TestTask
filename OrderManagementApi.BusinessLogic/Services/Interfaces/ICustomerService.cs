using OrderManagementApi.BusinessLogic.Dtos;

namespace OrderManagementApi.BusinessLogic.Services.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task CreateCustomerAsync(NewCustomer customer);
}