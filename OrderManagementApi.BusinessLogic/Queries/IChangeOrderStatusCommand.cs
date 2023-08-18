using OrderManagementApi.BusinessLogic.Commands;
using OrderManagementApi.BusinessLogic.Dtos;

namespace OrderManagementApi.BusinessLogic.Queries;

public interface IChangeOrderStatusCommand : ICommandHandler<ChangeOrderStatusRequest, bool>
{ }

public record ChangeOrderStatusRequest(Guid OrderId, OrderStatus Status);