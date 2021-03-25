using RCD.Core.DomainObjects;
using System.Collections.Generic;

namespace RCD.Domain.Models
{
    public class Address : Entity
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public ICollection<Customer> Customers { get; set; }


        // EF Constructor
        protected Address() { }

        public Address(string street, string number, string zipCode, string city, string state, string country)
        {
            Street = street;
            Number = number;
            ZipCode = zipCode;
            City = city;
            State = state;
            Country = country;
        }
    }
}
