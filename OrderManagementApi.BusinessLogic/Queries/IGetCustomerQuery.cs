using OrderManagementApi.BusinessLogic.Commands;
using OrderManagementApi.BusinessLogic.Dtos;

namespace OrderManagementApi.BusinessLogic.Queries;

public interface IGetCustomerQuery : IQueryHandler<GetCustomerRequest, Customer?>
{ }

public record GetCustomerRequest(Guid CustomerId);
