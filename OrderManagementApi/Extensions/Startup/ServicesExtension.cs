using OrderManagementApi.BusinessLogic.Commands;
using OrderManagementApi.BusinessLogic.Queries;
using OrderManagementApi.BusinessLogic.Services;
using OrderManagementApi.BusinessLogic.Services.Interfaces;
using OrderManagementApi.Infrastructure.Database.Queries;

namespace OrderManagementApi.Extensions.Startup;

public static class ServicesExtension
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IOrderService, OrderService>();

        builder.Services.AddScoped<IAddNewCustomerCommand, AddNewCustomerCommand>();
        builder.Services.AddScoped<IGetAllCustomersQuery, GetAllCustomersQuery>();
        builder.Services.AddScoped<IGetOrderDetailsQuery, GetOrderDetailsQuery>();
        builder.Services.AddScoped<IGetAllOrdersQuery, GetAllOrdersQuery>();
        builder.Services.AddScoped<IAddNewOrderCommand, AddNewOrderCommand>();
        builder.Services.AddScoped<IGetCustomerQuery, GetCustomerQuery>();
        builder.Services.AddScoped<IChangeOrderStatusCommand, ChangeOrderStatusCommand>();

        return builder;
    }
}
