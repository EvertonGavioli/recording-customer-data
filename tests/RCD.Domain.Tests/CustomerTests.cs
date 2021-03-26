using Bogus;
using RCD.Domain.Models;
using System;
using Xunit;

namespace RCD.Domain.Tests
{
    public class CustomerTests
    {
        [Fact(DisplayName = "New Customer Valid")]
        [Trait("Category", "Customer Tests")]
        public void Customer_NewCustomer_ItShouldBeValid()
        {
            // Arrange
            var faker = new Faker("pt_BR");
            var name = faker.Name.FullName();
            var email = faker.Internet.Email(name);

            var customer = new Customer(Guid.NewGuid(), name, email);

            // Act
            var result = customer.IsValid();

            // Assert
            Assert.True(result);
            Assert.Equal(0, customer.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "New Customer Invalid")]
        [Trait("Category", "Customer Tests")]
        public void Customer_NewCustomer_ItShouldBeInvalid()
        {
            // Arrange
            var customer = new Customer(Guid.NewGuid(), "", "");

            // Act
            var result = customer.IsValid();

            // Assert
            Assert.False(result);
            Assert.NotEqual(0, customer.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Edit Customer Name and Email")]
        [Trait("Category", "Customer Tests")]
        public void Customer_Edit_ItShouldBePossibleChangeNameAndEmaiil()
        {
            // Arrange
            var faker = new Faker("pt_BR");
            var name = faker.Name.FullName();
            var email = faker.Internet.Email(name);

            var customer = new Customer(Guid.NewGuid(), "", "");

            // Act
            customer.ChangeName(name);
            customer.ChangeEmail(email);

            var result = customer.IsValid();

            // Assert
            Assert.True(result);
            Assert.Equal(0, customer.ValidationResult.Errors.Count);
            Assert.Equal(name, customer.Name);
            Assert.Equal(email, customer.Email.EmailAddress);
        }

        [Fact(DisplayName = "Add Valid Phone")]
        [Trait("Category", "Customer Tests")]
        public void Customer_AddPhone_ItShouldBePossibleAddValidPhone()
        {
            // Arrange
            var faker = new Faker("pt_BR");
            var name = faker.Name.FullName();
            var email = faker.Internet.Email(name);
            var phoneNumber = faker.Phone.PhoneNumber("(##) ####-####");

            var customer = new Customer(Guid.NewGuid(), name, email);
            var phone = new Phone(PhoneType.local, phoneNumber);

            // Act
            customer.AddPhone(phone);
            var result = customer.IsValid();

            // Assert
            Assert.True(result);
            Assert.Equal(0, customer.ValidationResult.Errors.Count);
            Assert.Equal(1, customer.Phones.Count);
        }

        [Fact(DisplayName = "Add Invalid Phone")]
        [Trait("Category", "Customer Tests")]
        public void Customer_AddPhone_ItShouldBeInvalid()
        {
            // Arrange
            var faker = new Faker("pt_BR");
            var name = faker.Name.FullName();
            var email = faker.Internet.Email(name);
            var phoneNumber = "123";

            var customer = new Customer(Guid.NewGuid(), name, email);
            var phone = new Phone(PhoneType.local, phoneNumber);

            // Act
            customer.AddPhone(phone);
            var result = customer.IsValid();

            // Assert
            Assert.False(result);
            Assert.NotEqual(0, customer.ValidationResult.Errors.Count);
            Assert.Equal(1, customer.Phones.Count);
        }

        [Fact(DisplayName = "Add Valid Address")]
        [Trait("Category", "Customer Tests")]
        public void Customer_AddAddress_ItShouldBePossibleAddValidAddress()
        {
            // Arrange
            var faker = new Faker("pt_BR");
            var name = faker.Name.FullName();
            var email = faker.Internet.Email(name);

            var address = new Address(
                faker.Address.StreetName(),
                faker.Address.BuildingNumber(),
                faker.Address.ZipCode(),
                faker.Address.City(),
                faker.Address.State(),
                faker.Address.Country()
                );

            var customer = new Customer(Guid.NewGuid(), name, email);

            // Act
            customer.AddAddress(address);
            var result = address.IsValid();

            // Assert
            Assert.True(result);
            Assert.Equal(0, address.ValidationResult.Errors.Count);
            Assert.Equal(1, customer.Addresses.Count);
        }

        [Fact(DisplayName = "Add Invalid Address")]
        [Trait("Category", "Customer Tests")]
        public void Customer_AddAddress_ItShouldBeInvalid()
        {
            // Arrange
            var faker = new Faker("pt_BR");
            var name = faker.Name.FullName();
            var email = faker.Internet.Email(name);

            var customer = new Customer(Guid.NewGuid(), name, email);
            var address = new Address("", "", "", "", "", "");

            // Act
            customer.AddAddress(address);
            var result = address.IsValid();

            // Assert
            Assert.False(result);
            Assert.Equal(6, address.ValidationResult.Errors.Count);
            Assert.Equal(1, customer.Addresses.Count);
        }
    }
}
