using OrderManagementApi.BusinessLogic.Commands;
using OrderManagementApi.BusinessLogic.Dtos;

namespace OrderManagementApi.BusinessLogic.Queries;

public interface IGetAllOrdersQuery : IQueryHandler<IEnumerable<Order>>
{ }

// TODO: add pagination