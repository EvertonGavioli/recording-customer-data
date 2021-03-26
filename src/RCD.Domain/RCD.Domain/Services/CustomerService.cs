using RCD.Core.Notifications;
using RCD.Core.Services;
using RCD.Domain.Models;
using RCD.Domain.Models.Validations;
using RCD.Domain.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RCD.Domain.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository,
                               INotifier notifier) : base(notifier)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Create(Customer customer)
        {
            if (!ExecuteValidation(new CustomerValidation(), customer))
            {
                return null;
            }

            if (EmailInUse(customer.Email.EmailAddress))
            {
                Notify("Já existe um cliente com esse E-mail informado.");
                return null;
            }

            _customerRepository.Create(customer);

            await _customerRepository.UnitOfWork.Commit();

            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            if (!ExecuteValidation(new CustomerValidation(), customer))
            {
                return null;
            }

            if (_customerRepository.Search(f => f.Email.EmailAddress == customer.Email.EmailAddress && f.Id != customer.Id).Result.Any())
            {
                Notify("Já existe um cliente com esse E-mail informado.");
                return null;
            }

            var updatedCustomer = await _customerRepository.GetById(customer.Id);
            updatedCustomer.ChangeName(customer.Name);
            updatedCustomer.ChangeEmail(customer.Email.EmailAddress);

            _customerRepository.Update(updatedCustomer);

            await _customerRepository.UnitOfWork.Commit();

            return updatedCustomer;
        }

        public async Task<bool> Delete(Customer customer)
        {
            foreach (var phone in customer.Phones)
            {
                DeleteCustomerPhone(phone);
            }

            foreach (var address in customer.Addresses)
            {
                DeleteCustomerAddress(address);
            }

            _customerRepository.Delete(customer);

            return await _customerRepository.UnitOfWork.Commit();
        }

        #region Phones

        public async Task<Customer> AddPhone(Customer customer, Phone phone)
        {
            if (!Phone.Validate(phone.PhoneNumber))
            {
                Notify("Telefone informado é inválido.");
                return null;
            }
                       
            if (customer.Phones.Any(f => f.PhoneNumber == phone.PhoneNumber))
            {
                return customer;
            }

            Phone phoneToAdd = phone;

            if (phone.PhoneType == PhoneType.local)
            {
                var existingPhone = _customerRepository.SearchPhone(f => f.PhoneType == PhoneType.local && f.PhoneNumber == phone.PhoneNumber).Result.FirstOrDefault();

                if(existingPhone != null)
                {
                    phoneToAdd = existingPhone;
                }
            }

            customer.AddPhone(phoneToAdd);

            _customerRepository.Update(customer);

            await _customerRepository.UnitOfWork.Commit();

            return customer;
        }

        public async Task<Customer> DeletePhone(Customer customer, Guid phoneId)
        {
            var phone = customer.Phones.FirstOrDefault(f => f.Id == phoneId);

            if (phone == null)
            {
                return customer;
            }

            DeleteCustomerPhone(phone);

            customer.Phones.Remove(phone);

            _customerRepository.Update(customer);

            await _customerRepository.UnitOfWork.Commit();

            return customer;
        }

        #endregion

        #region Addresses

        public async Task<Customer> AddAddress(Customer customer, Address address)
        {
            if (CustomerAlreadyThisAddress(customer, address))
            {
                return customer;
            }

            Address addressToAdd = address;

            var existingAddress = SearchIfExistingAddress(address);

            if (existingAddress != null)
            {
                addressToAdd = existingAddress;
            }

            customer.AddAddress(addressToAdd);

            _customerRepository.Update(customer);

            await _customerRepository.UnitOfWork.Commit();

            return customer;
        }

        public async Task<Customer> DeleteAddress(Customer customer, Guid addressId)
        {
            var address = customer.Addresses.FirstOrDefault(f => f.Id == addressId);

            if (address == null)
            {
                return customer;
            }

            DeleteCustomerAddress(address);

            customer.Addresses.Remove(address);

            _customerRepository.Update(customer);

            await _customerRepository.UnitOfWork.Commit();

            return customer;
        }

        #endregion

        #region Private Methods

        private bool EmailInUse(string email)
        {
            return _customerRepository.Search(f => f.Email.EmailAddress == email).Result.Any();
        }

        private void DeleteCustomerPhone(Phone phone)
        {
            var customersWithThisPhone = _customerRepository.Search(f => f.Phones.Any(p => p.Id == phone.Id)).Result;

            if (customersWithThisPhone.Count() <= 1)
            {
                _customerRepository.DeletePhone(phone);
            }
        }

        private void DeleteCustomerAddress(Address address)
        {
            var customersWithThisAddress = _customerRepository.Search(f => f.Addresses.Any(p => p.Id == address.Id)).Result;

            if (customersWithThisAddress.Count() <= 1)
            {
                _customerRepository.DeleteAddress(address);
            }
        }

        private bool CustomerAlreadyThisAddress(Customer customer, Address address)
        {
            return customer.Addresses.Any(f =>
                f.Street == address.Street &&
                f.Number == address.Number &&
                f.ZipCode == address.ZipCode &&
                f.City == address.City &&
                f.State == address.State &&
                f.Country == address.Country);
        }

        private Address SearchIfExistingAddress(Address address)
        {
            return _customerRepository.SearchAddress(f =>
                f.Street == address.Street &&
                f.Number == address.Number &&
                f.ZipCode == address.ZipCode &&
                f.City == address.City &&
                f.State == address.State &&
                f.Country == address.Country).Result.FirstOrDefault();
        }

        #endregion

        public void Dispose()
        {
            _customerRepository?.Dispose();
        }
    }
}
