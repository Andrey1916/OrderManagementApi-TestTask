using OrderManagementApi.BusinessLogic.Commands;
using OrderManagementApi.BusinessLogic.Dtos;

namespace OrderManagementApi.BusinessLogic.Queries;

public interface IGetOrderDetailsQuery : IQueryHandler<GetOrderDetailsRequest, OrderDetails?>
{ }

public record GetOrderDetailsRequest(Guid OrderId);
