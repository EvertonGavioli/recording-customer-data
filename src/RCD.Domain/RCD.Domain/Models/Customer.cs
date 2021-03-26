
using RCD.Core.DomainObjects;
using RCD.Domain.Models.Validations;
using System;
using System.Collections.Generic;

namespace RCD.Domain.Models
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public ICollection<Phone> Phones { get; private set; }
        public ICollection<Address> Addresses { get; private set; }


        // Ef Constructor
        protected Customer() { }

        public Customer(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = new Email(email);

            Phones = new List<Phone>();
            Addresses = new List<Address>();
        }

        public void AddPhone(Phone phone)
        {
            Phones.Add(phone);
        }

        public void AddAddress(Address address)
        {
            Addresses.Add(address);
        }

        public void ChangeName(string newName)
        {
            Name = newName;
        }

        public void ChangeEmail(string newEmail)
        {
            Email = new Email(newEmail);
        }

        public override bool IsValid()
        {
            ValidationResult = new CustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
