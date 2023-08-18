using Microsoft.AspNetCore.Mvc;
using OrderManagementApi.BusinessLogic.Exceptions;
using OrderManagementApi.BusinessLogic.Services.Interfaces;
using OrderManagementApi.Models;

using Dtos = OrderManagementApi.BusinessLogic.Dtos;

namespace OrderManagementApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(
        ICustomerService customerService
        )
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> Get()
    {
        var customersDtos = await _customerService.GetAllAsync();

        var customers = customersDtos.Select(
            cus => new Customer
            {
                Email       = cus.Email,
                FirstName   = cus.FirstName,
                Id          = cus.Id,
                LastName    = cus.LastName,
                PhoneNumber = cus.PhoneNumber
            });

        return Ok(customers);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] NewCustomer customer)
    {
        var newCustomer = new Dtos.NewCustomer
        {
            PhoneNumber = customer.PhoneNumber,
            LastName    = customer.LastName,
            FirstName   = customer.FirstName,
            Email       = customer.Email
        };

        try
        {
            await _customerService.CreateCustomerAsync(newCustomer);
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

        return Ok();
    }
}