using Moq;
using Moq.AutoMock;
using RCD.Domain.Models;
using RCD.Domain.Repository;
using RCD.Domain.Services;
using RCD.Domain.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace RCD.Domain.Tests
{
    [Collection(nameof(CustomerFixtureCollection))]
    public class CustomerServiceTests
    {
        private readonly CustomerFixture _customerFixture;
        private readonly AutoMocker _mocker;

        public CustomerServiceTests(CustomerFixture customerFixture)
        {
            _customerFixture = customerFixture;
            _mocker = new AutoMocker();
        }

        [Fact(DisplayName = "Create a valid Customer with Success")]
        [Trait("Category", "Customer Service Tests")]
        public async void CustomerService_Create_ItShouldBeCreatedACustomerWithSuccess()
        {
            // Arrange
            var customer = _customerFixture.GenerateValidCustomer();
            var customerService = _mocker.CreateInstance<CustomerService>();
            _mocker.GetMock<ICustomerRepository>().Setup(f => f.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            await customerService.Create(customer);

            // Assert
            Assert.True(customer.IsValid());
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.Create(customer), Times.Once);
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Create an invalid Customer with errors")]
        [Trait("Category", "Customer Service Tests")]
        public async void CustomerService_Create_ItShouldBeFailWhenInvalid()
        {
            // Arrange
            var customer = new Customer(Guid.NewGuid(), "", "");
            var customerService = _mocker.CreateInstance<CustomerService>();

            // Act
            await customerService.Create(customer);

            // Assert
            Assert.False(customer.IsValid());
            Assert.NotEqual(0, customer.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Create a Customer with the same email")]
        [Trait("Category", "Customer Service Tests")]
        public async void CustomerService_Create_ItShouldBeFailWhenHasSameEmail()
        {
            // Arrange
            var customer = _customerFixture.GenerateValidCustomer();
            var customerService = _mocker.CreateInstance<CustomerService>();

            IEnumerable<Customer> customersList = new List<Customer>() { customer };

            _mocker.GetMock<ICustomerRepository>().Setup(f => f.Search(It.IsAny<Expression<Func<Customer, bool>>>()))
                .Returns(Task.FromResult(customersList));

            // Act
            var result = await customerService.Create(customer);

            // Assert
            Assert.Null(result);
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.Create(customer), Times.Never);
        }

        [Fact(DisplayName = "Update a valid Customer with Success")]
        [Trait("Category", "Customer Service Tests")]
        public async void CustomerService_Update_ItShouldBeUpdatedACustomerWithSuccess()
        {
            // Arrange
            var customer = _customerFixture.GenerateValidCustomer();
            var customerService = _mocker.CreateInstance<CustomerService>();
            _mocker.GetMock<ICustomerRepository>().Setup(f => f.GetById(customer.Id)).Returns(Task.FromResult(customer));
            _mocker.GetMock<ICustomerRepository>().Setup(f => f.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            await customerService.Update(customer);

            // Assert
            Assert.True(customer.IsValid());
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.Update(customer), Times.Once);
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Update an invalid Customer with errors")]
        [Trait("Category", "Customer Service Tests")]
        public async void CustomerService_Update_ItShouldBeFailWhenInvalid()
        {
            // Arrange
            var customer = new Customer(Guid.NewGuid(), "", "");
            var customerService = _mocker.CreateInstance<CustomerService>();

            // Act
            await customerService.Update(customer);

            // Assert
            Assert.False(customer.IsValid());
            Assert.NotEqual(0, customer.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Update a Customer with an existing email")]
        [Trait("Category", "Customer Service Tests")]
        public async void CustomerService_Update_ItShouldBeFailWhenExistingEmail()
        {
            // Arrange
            var customer = _customerFixture.GenerateValidCustomer();
            var customerService = _mocker.CreateInstance<CustomerService>();

            IEnumerable<Customer> customersList = new List<Customer>() { customer };

            _mocker.GetMock<ICustomerRepository>().Setup(f => f.Search(It.IsAny<Expression<Func<Customer, bool>>>()))
                .Returns(Task.FromResult(customersList));

            // Act
            var result = await customerService.Update(customer);

            // Assert
            Assert.Null(result);
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.Update(customer), Times.Never);
        }

        [Fact(DisplayName = "Delete a Customer with Success")]
        [Trait("Category", "Customer Service Tests")]
        public async void CustomerService_Delete_ItShouldBePossibleDeleteCustomerWithSuccess()
        {
            // Arrange
            var customer = _customerFixture.GenerateValidCustomerWithPhoneAndAddress();
            var customerService = _mocker.CreateInstance<CustomerService>();
            _mocker.GetMock<ICustomerRepository>().Setup(f => f.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            await customerService.Delete(customer);

            // Assert
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.Delete(customer), Times.Once);
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Add a Customer Phone with Success")]
        [Trait("Category", "Customer Service Tests")]
        public async void CustomerService_AddPhone_ItShouldBePossibleAddPhoneCustomerWithSuccess()
        {
            // Arrange
            var customer = _customerFixture.GenerateValidCustomer();
            var phone = _customerFixture.GenerateValidPhone();
            var customerService = _mocker.CreateInstance<CustomerService>();
            _mocker.GetMock<ICustomerRepository>().Setup(f => f.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            await customerService.AddPhone(customer, phone);

            // Assert
            Assert.Equal(1, customer.Phones.Count);
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.Update(customer), Times.Once);
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Delete a Customer Phone with Success")]
        [Trait("Category", "Customer Service Tests")]
        public async void CustomerService_DeletePhone_ItShouldBePossibleDeletePhoneCustomerWithSuccess()
        {
            // Arrange
            var customer = _customerFixture.GenerateValidCustomerWithPhoneAndAddress();
            var customerService = _mocker.CreateInstance<CustomerService>();
            _mocker.GetMock<ICustomerRepository>().Setup(f => f.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            await customerService.DeletePhone(customer, customer.Phones.ToList()[0].Id);

            // Assert
            Assert.Equal(0, customer.Phones.Count);
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.Update(customer), Times.Once);
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Add a Customer Address with Success")]
        [Trait("Category", "Customer Service Tests")]
        public async void CustomerService_AddAddress_ItShouldBePossibleAddAddressCustomerWithSuccess()
        {
            // Arrange
            var customer = _customerFixture.GenerateValidCustomer();
            var address = _customerFixture.GenerateValidAddress();
            var customerService = _mocker.CreateInstance<CustomerService>();
            _mocker.GetMock<ICustomerRepository>().Setup(f => f.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            await customerService.AddAddress(customer, address);

            // Assert
            Assert.Equal(1, customer.Addresses.Count);
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.Update(customer), Times.Once);
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Delete a Customer Address with Success")]
        [Trait("Category", "Customer Service Tests")]
        public async void CustomerService_DeleteAddress_ItShouldBePossibleDeleteAddressCustomerWithSuccess()
        {
            // Arrange
            var customer = _customerFixture.GenerateValidCustomerWithPhoneAndAddress();
            var customerService = _mocker.CreateInstance<CustomerService>();
            _mocker.GetMock<ICustomerRepository>().Setup(f => f.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            await customerService.DeleteAddress(customer, customer.Addresses.ToList()[0].Id);

            // Assert
            Assert.Equal(0, customer.Addresses.Count);
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.Update(customer), Times.Once);
            _mocker.GetMock<ICustomerRepository>().Verify(f => f.UnitOfWork.Commit(), Times.Once);
        }
    }
}
