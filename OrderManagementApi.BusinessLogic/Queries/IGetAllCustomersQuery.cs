using OrderManagementApi.BusinessLogic.Dtos;

namespace OrderManagementApi.BusinessLogic.Commands;

public interface IGetAllCustomersQuery : IQueryHandler<IEnumerable<Customer>>
{ }
