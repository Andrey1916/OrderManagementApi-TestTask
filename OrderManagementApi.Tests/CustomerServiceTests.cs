using Moq;
using OrderManagementApi.BusinessLogic.Commands;
using OrderManagementApi.BusinessLogic.Dtos;
using OrderManagementApi.BusinessLogic.Exceptions;
using OrderManagementApi.BusinessLogic.Queries;
using OrderManagementApi.BusinessLogic.Services;

namespace OrderManagementApi.Tests;

public class CustomerServiceTests
{

    [Fact]
    public async Task GetAllCustomers_ReturnsAllCustomers()
    {
        // Arrange
        IEnumerable<Customer> customers = new Customer[]
        {
            new Customer
            {
                Id          = Guid.NewGuid(),
                Email       = "mail1@mail.com",
                FirstName   = "FTest1",
                LastName    = "LTest1",
                PhoneNumber = "+12356978156",
            },
            new Customer
            {
                Id          = Guid.NewGuid(),
                Email       = "mail2@mail.com",
                FirstName   = "FTest2",
                LastName    = "LTest2",
                PhoneNumber = "+12356978177",
            },
            new Customer
            {
                Id          = Guid.NewGuid(),
                Email       = "mail3@mail.com",
                FirstName   = "FTest3",
                LastName    = "LTest3",
                PhoneNumber = "+12356978188",
            }
        };

        IAddNewCustomerCommand addNewCustomerCommandMock = null!;

        var getAllCustomersQueryMock = Mock.Of<IGetAllCustomersQuery>(
            query => query.Handle(default) == Task.FromResult(customers)
            );

        var service = new CustomerService(addNewCustomerCommandMock, getAllCustomersQueryMock);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(customers.Count(), result.Count());

        foreach(var customer in customers)
        {
            Assert.Contains(customer, result);
        }
    }

    [Fact]
    public async Task CreateCustomer_CreateWithValidParams_InvokedAddNewCustomerCommand()
    {
        // Arrange
        var newCustomer = new NewCustomer
        {
            FirstName   = "Test",
            LastName    = "Test",
            Email       = "test@mail.com",
            PhoneNumber = "+12356978188"
        };


        var addNewCustomerCommandMock = new Mock<IAddNewCustomerCommand>();
        IGetAllCustomersQuery getAllCustomersQueryMock = null!;

        var service = new CustomerService(addNewCustomerCommandMock.Object, getAllCustomersQueryMock);

        // Act
        await service.CreateCustomerAsync(newCustomer);

        // Assert
        addNewCustomerCommandMock.Verify(m => m.Handle(It.IsAny<NewCustomer>(), default), Times.Once);
    }

    [Fact]
    public async Task CreateCustomer_CreateWithoutEmail_InvokedAddNewCustomerCommand()
    {
        // Arrange
        var newCustomer = new NewCustomer
        {
            FirstName   = "Test",
            LastName    = "Test",
            PhoneNumber = "+12356978188"
        };


        var addNewCustomerCommandMock = new Mock<IAddNewCustomerCommand>();
        IGetAllCustomersQuery getAllCustomersQueryMock = null!;

        var service = new CustomerService(addNewCustomerCommandMock.Object, getAllCustomersQueryMock);

        // Act
        await service.CreateCustomerAsync(newCustomer);

        // Assert
        addNewCustomerCommandMock.Verify(m => m.Handle(It.IsAny<NewCustomer>(), default), Times.Once);
    }

    [Fact]
    public async Task CreateCustomer_CreateWithInvalidParams_ThrownsValidationException()
    {
        // Arrange
        var newCustomer = new NewCustomer();

        var invalidPropNames = new[]
        {
            nameof(NewCustomer.PhoneNumber),
            nameof(NewCustomer.FirstName),
            nameof(NewCustomer.FirstName)
        };

        var addNewCustomerCommandMock = new Mock<IAddNewCustomerCommand>();
        IGetAllCustomersQuery getAllCustomersQueryMock = null!;

        var service = new CustomerService(addNewCustomerCommandMock.Object, getAllCustomersQueryMock);

        // Act + Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            async () => await service.CreateCustomerAsync(newCustomer)
            );

        Assert.True(exception.InnerException is AggregateException);

        var exceptions = (exception.InnerException as AggregateException)!.InnerExceptions;

        Assert.NotEmpty(exceptions);

        foreach(var propName in invalidPropNames)
        {
            Assert.Contains(
                exceptions,
                ex => ex is ArgumentException aex && aex.ParamName == propName);
        }

        addNewCustomerCommandMock.Verify(m => m.Handle(It.IsAny<NewCustomer>(), default), Times.Never);
    }
}
