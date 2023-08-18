using OrderManagementApi.BusinessLogic.Commands;
using OrderManagementApi.BusinessLogic.Dtos;
using OrderManagementApi.BusinessLogic.Exceptions;
using OrderManagementApi.BusinessLogic.Queries;
using OrderManagementApi.BusinessLogic.Services.Interfaces;

namespace OrderManagementApi.BusinessLogic.Services;

public class CustomerService : ICustomerService
{
    private readonly IAddNewCustomerCommand _addNewCustomerCommand;
    private readonly IGetAllCustomersQuery _getAllCustomersQuery;


    public CustomerService(
        IAddNewCustomerCommand addNewCustomerCommand,
        IGetAllCustomersQuery getAllCustomersQuery
        )
    {
        _addNewCustomerCommand = addNewCustomerCommand;
        _getAllCustomersQuery  = getAllCustomersQuery;
    }


    public async Task CreateCustomerAsync(NewCustomer customer)
    {
        var validator = new NewCustomerValidator();

        var results = validator.Validate(customer);

        if (!results.IsValid)
        {
            var exceptions = results.Errors.Select(
                failure => new ArgumentException(failure.ErrorMessage, failure.PropertyName)
                );

            throw new ValidationException("Validation Error", new AggregateException(exceptions));
        }

        await _addNewCustomerCommand.Handle(customer);
    }

    public Task<IEnumerable<Customer>> GetAllAsync()
        => _getAllCustomersQuery.Handle();
}
