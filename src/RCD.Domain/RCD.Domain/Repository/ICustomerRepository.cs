using RCD.Core.Data;
using RCD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RCD.Domain.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<IEnumerable<Customer>> GetAllWithPhonesAndAddresses();
        Task<Customer> GetById(Guid id);
        Task<Customer> GetByIdWithPhonesAndAddresses(Guid id);

        Task<IEnumerable<Customer>> Search(Expression<Func<Customer, bool>> predicate);

        void Create(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);


        // Phones
        void DeletePhone(Phone phone);
        Task<IEnumerable<Phone>> SearchPhone(Expression<Func<Phone, bool>> predicate);


        // Addresses
        void DeleteAddress(Address address);
        Task<IEnumerable<Address>> SearchAddress(Expression<Func<Address, bool>> predicate);
    }
}
