using OrderManagementApi.BusinessLogic.Commands;
using OrderManagementApi.BusinessLogic.Dtos;

namespace OrderManagementApi.BusinessLogic.Queries;

public interface IAddNewCustomerCommand : ICommandHandler<NewCustomer>
{ }