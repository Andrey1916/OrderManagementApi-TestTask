using Microsoft.AspNetCore.Mvc;
using OrderManagementApi.BusinessLogic.Exceptions;
using OrderManagementApi.BusinessLogic.Services.Interfaces;
using OrderManagementApi.Models;

using Dtos = OrderManagementApi.BusinessLogic.Dtos;

namespace OrderManagementApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class OrderController : ControllerBase
{

    private readonly IOrderService _orderService;

    public OrderController(
        IOrderService orderService
        )
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> Get()
    {
        var ordersDtos = await _orderService.GetAllAsync();

        var orders = ordersDtos.Select(
            o => new Order
            {
                CreateDate      = o.CreateDate,
                CustomerId      = o.CustomerId,
                DeliveryAddress = o.DeliveryAddress,
                Description     = o.Description,
                Id              = o.Id,
                TotalPrice      = o.TotalPrice,
                UpdateDate      = o.UpdateDate,
                Status          = (OrderStatus)o.Status
            });

        return Ok(orders);
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<Order>> Get(Guid orderId)
    {
        var ordersDetailsDto = await _orderService.GetAsync(orderId);

        if (ordersDetailsDto is null)
        {
            return NotFound();
        }

        var orderDetails = new OrderDetails
        {
            CreateDate      = ordersDetailsDto.CreateDate,
            CustomerId      = ordersDetailsDto.CustomerId,
            DeliveryAddress = ordersDetailsDto.DeliveryAddress,
            Description     = ordersDetailsDto.Description,
            Id              = ordersDetailsDto.Id,
            UpdateDate      = ordersDetailsDto.UpdateDate,
            Status          = (OrderStatus)ordersDetailsDto.Status,
            OrderItems      = ordersDetailsDto.OrderItems.Select(
                oi => new OrderItem
                {
                    Count       = oi.Count,
                    Price       = oi.Price,
                    ProductName = oi.ProductName,
                })
        };

        return Ok(orderDetails);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody]NewOrder order)
    {
        var newOrder = new Dtos.NewOrder
        {
            CustomerId      = order.CustomerId,
            Description     = order.Description,
            DeliveryAddress = order.DeliveryAddress,
            OrderItems      = order.OrderItems.Select(
                oi => new Dtos.OrderItem
                {
                    Count       = oi.Count,
                    Price       = oi.Price,
                    ProductName = oi.ProductName,
                })
        };

        try
        {
            Guid orderId = await _orderService.CreateOrderAsync(newOrder);

            return Ok(
                new { OrderId = orderId }
                );
        }
        catch (ValidationException vex)
        {
            var validationDetails = vex.InnerException is AggregateException aggregateException
                ? aggregateException.InnerExceptions.Select(ex => ex.Message)
                : new string[] { vex.InnerException?.Message ?? "Validation Error" };

            return BadRequest(
                new
                {
                    vex.Message,
                    Details = validationDetails
                });
        }
    }

    [HttpPatch("{orderId}")]
    public async Task<IActionResult> ChangeOrderStatus(Guid orderId, [FromBody]NewOrderStatus newOrderStatus)
    {
        bool isChanged = await _orderService.ChangeOrderStatus(orderId, (Dtos.OrderStatus)newOrderStatus.NewStatus);

        return isChanged
            ? Ok()
            : NotFound();
    }
}