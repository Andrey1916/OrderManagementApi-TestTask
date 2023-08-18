using Moq;
using OrderManagementApi.BusinessLogic.Dtos;
using OrderManagementApi.BusinessLogic.Queries;
using OrderManagementApi.BusinessLogic.Services;

namespace OrderManagementApi.Tests;

public class OrderServiceTests
{
    [Fact]
    public async Task GetAllOrders_ReturnsAllCustomers()
    {
        // Arrange
        IEnumerable<Order> orders = new Order[]
        {
            new Order
            {
                Id              = Guid.NewGuid(),
                CustomerId      = Guid.NewGuid(),
                Description     = "Test",
                DeliveryAddress = "TestAddress",
                TotalPrice      = 100m
            },
            new Order
            {
                Id              = Guid.NewGuid(),
                CustomerId      = Guid.NewGuid(),
                Description     = "Test",
                DeliveryAddress = "TestAddress",
                TotalPrice      = 1001m
            },
            new Order
            {
                Id              = Guid.NewGuid(),
                CustomerId      = Guid.NewGuid(),
                Description     = "Test",
                DeliveryAddress = "TestAddress",
                TotalPrice      = 1002m
            }
        };

        IGetOrderDetailsQuery getOrderDetailsQuery = null!;
        IAddNewOrderCommand addNewOrderCommand = null!;
        IGetCustomerQuery getCustomerQuery = null!;
        IChangeOrderStatusCommand changeOrderStatusCommand = null!;

        var getAllOrders = Mock.Of<IGetAllOrdersQuery>(
            query => query.Handle(default) == Task.FromResult(orders)
            );

        var service = new OrderService(getOrderDetailsQuery, getAllOrders, addNewOrderCommand, getCustomerQuery, changeOrderStatusCommand);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(orders.Count(), result.Count());

        foreach (var customer in orders)
        {
            Assert.Contains(customer, result);
        }
    }


    [Fact]
    public async Task Get_GetSpecificOrderWithDetails_ReturnOrderWithOrderItems()
    {
        // Arrange
        var order = new OrderDetails
        {
            Id              = Guid.NewGuid(),
            CustomerId      = Guid.NewGuid(),
            Description     = "Test",
            DeliveryAddress = "TestAddress",
            OrderItems      = new List<OrderItem>
            {
                new OrderItem
                {
                    Count       = 1,
                    Price       = 10,
                    ProductName = "Product 1"
                },
                new OrderItem
                {
                    Count       = 2,
                    Price       = 10,
                    ProductName = "Product 2"
                },
                new OrderItem
                {
                    Count       = 5,
                    Price       = 20,
                    ProductName = "Product 3"
                }
            }
        };

        IGetAllOrdersQuery getAllOrders = null!;
        IAddNewOrderCommand addNewOrderCommand = null!;
        IGetCustomerQuery getCustomerQuery = null!;
        IChangeOrderStatusCommand changeOrderStatusCommand = null!;

        var getOrderDetailsQuery = Mock.Of<IGetOrderDetailsQuery>(
            query => query.Handle(It.IsAny<GetOrderDetailsRequest>(), default) == Task.FromResult(order)
            );

        var service = new OrderService(
            getOrderDetailsQuery,
            getAllOrders,
            addNewOrderCommand,
            getCustomerQuery,
            changeOrderStatusCommand);

        // Act
        var result = await service.GetAsync(order.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(order, result);

        Assert.NotEmpty(result.OrderItems);
        Assert.Equal(order.OrderItems.Count(), result.OrderItems.Count());

        foreach (var item in order.OrderItems)
        {
            Assert.Contains(item, result.OrderItems);
        }
    }

    [Fact]
    public async Task Get_GetSpecificOrderByInvalidId_ReturnNull()
    {
        // Arrange
        IGetAllOrdersQuery getAllOrders = null!;
        IAddNewOrderCommand addNewOrderCommand = null!;
        IGetCustomerQuery getCustomerQuery = null!;
        IChangeOrderStatusCommand changeOrderStatusCommand = null!;

        var getOrderDetailsQuery = Mock.Of<IGetOrderDetailsQuery>(
            query => query.Handle(It.IsAny<GetOrderDetailsRequest>(), default) == Task.FromResult<OrderDetails?>(null)
            );

        var service = new OrderService(
            getOrderDetailsQuery,
            getAllOrders,
            addNewOrderCommand,
            getCustomerQuery,
            changeOrderStatusCommand);

        // Act
        var result = await service.GetAsync(Guid.Empty);

        // Assert
        Assert.Null(result);
    }

    public async Task CreateOrder_CreateOrderWithValidData_ReturnOrderId()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
        Guid customerId = Guid.NewGuid();

        var newOrder = new NewOrder
        {
            CustomerId      = customerId,
            DeliveryAddress = "TestAddress",
            Description     = "Description",
            OrderItems      = new OrderItem[]
            {
                new OrderItem
                {
                    Count       = 1,
                    Price       = 10,
                    ProductName = "Product 1"
                },
                new OrderItem
                {
                    Count       = 2,
                    Price       = 10,
                    ProductName = "Product 2"
                },
            }
        };

        var customer = new Customer
        {
            Id = customerId
        };

        IGetAllOrdersQuery getAllOrders = null!;
        IGetOrderDetailsQuery getOrderDetailsQuery = null!;
        IChangeOrderStatusCommand changeOrderStatusCommand = null!;

        var getCustomerQuery = Mock.Of<IGetCustomerQuery>(
            query => query.Handle(It.IsAny<GetCustomerRequest>(), default) == Task.FromResult<Customer?>(customer)
            );

        var addNewOrderCommand = Mock.Of<IAddNewOrderCommand>(
            query => query.Handle(It.IsAny<NewOrderRequest>(), default) == Task.FromResult(orderId)
            );

        var service = new OrderService(
            getOrderDetailsQuery,
            getAllOrders,
            addNewOrderCommand,
            getCustomerQuery,
            changeOrderStatusCommand);

        // Act
        var result = await service.CreateOrderAsync(newOrder);

        // Assert
        Assert.Equal(orderId, result);
    }
}
