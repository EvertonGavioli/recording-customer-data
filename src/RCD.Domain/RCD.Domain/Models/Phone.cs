using RCD.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RCD.Domain.Models
{
    public class Phone : Entity
    {
        public PhoneType PhoneType { get; private set; }
        public string PhoneNumber { get; private set; }

        public ICollection<Customer> Customers { get; set; }
        
        
        // Ef Constructor
        protected Phone () 
        { }

        public Phone(PhoneType phoneType, string phoneNumber)
        {
            PhoneType = phoneType;
            PhoneNumber = phoneNumber;
        }

        public static bool Validate(string phoneNumber)
        {
            var regexPhone = new Regex(@"^\(\d{2}\) \d{4,5}\-\d{4}$");
            return regexPhone.IsMatch(phoneNumber);
        }
    }
}
