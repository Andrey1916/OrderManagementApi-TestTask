using OrderManagementApi.BusinessLogic.Dtos;
using OrderManagementApi.BusinessLogic.Exceptions;
using OrderManagementApi.BusinessLogic.Queries;
using OrderManagementApi.BusinessLogic.Services.Interfaces;
using System;

namespace OrderManagementApi.BusinessLogic.Services;

public class OrderService : IOrderService
{
    private readonly IGetOrderDetailsQuery _getOrderDetailsQuery;
    private readonly IGetAllOrdersQuery _getAllOrders;
    private readonly IAddNewOrderCommand _addNewOrderCommand;
    private readonly IGetCustomerQuery _getCustomerQuery;
    private readonly IChangeOrderStatusCommand _changeOrderStatusCommand;


    public OrderService(
        IGetOrderDetailsQuery getOrderDetailsQuery,
        IGetAllOrdersQuery getAllOrders,
        IAddNewOrderCommand addNewOrderCommand,
        IGetCustomerQuery getCustomerQuery,
        IChangeOrderStatusCommand changeOrderStatusCommand)
    {
        _getOrderDetailsQuery     = getOrderDetailsQuery;
        _getAllOrders             = getAllOrders;
        _addNewOrderCommand       = addNewOrderCommand;
        _getCustomerQuery         = getCustomerQuery;
        _changeOrderStatusCommand = changeOrderStatusCommand;
    }


    public async Task<Guid> CreateOrderAsync(NewOrder order)
    {
        var validator = new NewOrderValidator();

        var results = validator.Validate(order);

        if (!results.IsValid)
        {
            var exceptions = results.Errors.Select(
                failure => new ArgumentException(failure.ErrorMessage, failure.PropertyName)
                );

            throw new ValidationException("Validation Error", new AggregateException(exceptions));
        }

        var customer = await _getCustomerQuery.Handle(
            new GetCustomerRequest(order.CustomerId)
            );

        if (customer is null)
        {
            throw new ValidationException(
                "Validation Error",
                new AggregateException(
                    new ArgumentException($"Customer with Id { order.CustomerId } is not exists.")
                    )
                );
        }

        var request = new NewOrderRequest
        {
            CustomerId      = order.CustomerId,
            Status          = OrderStatus.Created,
            OrderItems      = order.OrderItems,
            DeliveryAddress = order.DeliveryAddress,
            Description     = order.Description
        };

        Guid newOrderId = await _addNewOrderCommand.Handle(request);

        return newOrderId;
    }

    public Task<OrderDetails?> GetAsync(Guid id)
    {
        var request = new GetOrderDetailsRequest(id);

        return _getOrderDetailsQuery.Handle(request);
    }

    public Task<IEnumerable<Order>> GetAllAsync()
        => _getAllOrders.Handle();

    public async Task<bool> ChangeOrderStatus(Guid orderId, OrderStatus status)
    {
        var request = new ChangeOrderStatusRequest(orderId, status);

        bool isChanged = await _changeOrderStatusCommand.Handle(request);

        return isChanged;
    }
}
