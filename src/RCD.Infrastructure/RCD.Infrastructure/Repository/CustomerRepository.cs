using Microsoft.EntityFrameworkCore;
using RCD.Core.Data;
using RCD.Domain.Models;
using RCD.Domain.Repository;
using RCD.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RCD.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;


        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllWithPhonesAndAddresses()
        {
            return await _context.Customers.AsNoTracking()
                .Include(f => f.Phones)
                .Include(f => f.Addresses)
                .ToListAsync();
        }

        public async Task<Customer> GetById(Guid id)
        {
            return await _context.Customers.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Customer> GetByIdWithPhonesAndAddresses(Guid id)
        {
            return await _context.Customers
                .Include(f => f.Phones)
                .Include(f => f.Addresses)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Customer>> Search(Expression<Func<Customer, bool>> predicate)
        {
            return await _context.Customers.AsNoTracking().Where(predicate).ToListAsync();
        }

        public void Create(Customer customer)
        {
            _context.Add(customer);
        }

        public void Update(Customer customer)
        {
            _context.Update(customer);
        }

        public void Delete(Customer customer)
        {
            _context.Remove(customer);
        }

        // Phones
        public void DeletePhone(Phone phone)
        {
            _context.Remove(phone);
        }

        public async Task<IEnumerable<Phone>> SearchPhone(Expression<Func<Phone, bool>> predicate)
        {
            return await _context.Phones.AsNoTracking().Where(predicate).ToListAsync();
        }

        // Addresses
        public void DeleteAddress(Address address)
        {
            _context.Remove(address);
        }
        public async Task<IEnumerable<Address>> SearchAddress(Expression<Func<Address, bool>> predicate)
        {
            return await _context.Addresses.AsNoTracking().Where(predicate).ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
