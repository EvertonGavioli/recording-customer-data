using Bogus;
using RCD.Domain.Models;
using System;
using Xunit;

namespace RCD.Domain.Tests.Fixtures
{
    [CollectionDefinition(nameof(CustomerFixtureCollection))]
    public class CustomerFixtureCollection : ICollectionFixture<CustomerFixture>
    { }

    public class CustomerFixture : IDisposable
    {
        public Customer GenerateValidCustomer()
        {
            var faker = new Faker("pt_BR");
            var name = faker.Name.FullName();
            var email = faker.Internet.Email(name);

            return new Customer(Guid.NewGuid(), name, email);
        }

        public Customer GenerateValidCustomerWithPhoneAndAddress()
        {
            var faker = new Faker("pt_BR");
            var name = faker.Name.FullName();
            var email = faker.Internet.Email(name);

            var phoneNumber = faker.Phone.PhoneNumber("(##) ####-####");
            Phone phone = new Phone(PhoneType.local, phoneNumber);

            var address = new Address(
                faker.Address.StreetName(),
                faker.Address.BuildingNumber(),
                faker.Address.ZipCode(),
                faker.Address.City(),
                faker.Address.State(),
                faker.Address.Country()
                );

            var customer = new Customer(Guid.NewGuid(), name, email);
            customer.AddPhone(phone);
            customer.AddAddress(address);

            return customer;
        }

        public Phone GenerateValidPhone()
        {
            var faker = new Faker("pt_BR");
            
            var phoneNumber = faker.Phone.PhoneNumber("(##) ####-####");
            return new Phone(PhoneType.local, phoneNumber);
        }

        public Address GenerateValidAddress()
        {
            var faker = new Faker("pt_BR");

            var address = new Address(
                faker.Address.StreetName(),
                faker.Address.BuildingNumber(),
                faker.Address.ZipCode(),
                faker.Address.City(),
                faker.Address.State(),
                faker.Address.Country()
                );

            return address;
        }

        public void Dispose()
        { }
    }
}
