using RCD.Domain.Models;
using System;
using System.Threading.Tasks;

namespace RCD.Domain.Services
{
    public interface ICustomerService : IDisposable
    {
        Task<Customer> Create(Customer customer);
        Task<Customer> Update(Customer customer);
        Task<bool> Delete(Customer customer);

        // Phones
        Task<Customer> AddPhone(Customer customer, Phone phone);
        Task<Customer> DeletePhone(Customer customer, Guid phoneId);

        // Addresses
        Task<Customer> AddAddress(Customer customer, Address address);
        Task<Customer> DeleteAddress(Customer customer, Guid addressId);
    }
}
