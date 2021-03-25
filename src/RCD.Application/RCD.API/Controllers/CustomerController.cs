using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RCD.API.ViewModels;
using RCD.Core.Notifications;
using RCD.Domain.Models;
using RCD.Domain.Repository;
using RCD.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RCD.API.Controllers
{
    [Route("api/v1/customers")]
    public class CustomerController : MainController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository,
                                  ICustomerService customerService,
                                  IMapper mapper,
                                  INotifier notifier) : base(notifier)
        {
            _customerRepository = customerRepository;
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetAll()
        {
            var customers = _mapper.Map<IEnumerable<CustomerViewModel>>(await _customerRepository.GetAllWithPhonesAndAddresses());

            return ApiResponse(customers);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerViewModel>> GetById(Guid id)
        {
            var customer = _mapper.Map<CustomerViewModel>(await _customerRepository.GetByIdWithPhonesAndAddresses(id));

            if (customer == null) return NotFound();

            return ApiResponse(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> Create(BaseCustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid) return ApiResponse(ModelState);

            var customer = await _customerService.Create(_mapper.Map<Customer>(customerViewModel));

            return ApiResponse(_mapper.Map<CustomerViewModel>(customer));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CustomerViewModel>> Update(Guid id, BaseCustomerViewModel customerViewModel)
        {
            if (id != customerViewModel.Id)
            {
                NotifyError("Os Id's informados são diferentes.");
                return ApiResponse();
            }

            if (!ModelState.IsValid) return ApiResponse(ModelState);

            var customer = await _customerService.Update(_mapper.Map<Customer>(customerViewModel));

            return ApiResponse(_mapper.Map<CustomerViewModel>(customer));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CustomerViewModel>> Delete(Guid id)
        {
            var customer = await _customerRepository.GetByIdWithPhonesAndAddresses(id);

            if (customer == null) return NotFound();

            await _customerService.Delete(customer);

            return ApiResponse(_mapper.Map<CustomerViewModel>(customer));
        }

        #region Phones

        [HttpPost("{customerId:guid}/phones")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> AddPhone(Guid customerId, PhoneViewModel phoneViewModel)
        {
            var customer = await _customerRepository.GetByIdWithPhonesAndAddresses(customerId);

            if (customer == null) return NotFound();

            var updatedCustomer = await _customerService.AddPhone(customer, _mapper.Map<Phone>(phoneViewModel));

            return ApiResponse(_mapper.Map<CustomerViewModel>(updatedCustomer));
        }

        [HttpDelete("{customerId:guid}/phones/{phoneId:guid}")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> DeletePhone(Guid customerId, Guid phoneId)
        {
            var customer = await _customerRepository.GetByIdWithPhonesAndAddresses(customerId);

            if (customer == null) return NotFound();

            var updatedCustomer = await _customerService.DeletePhone(customer, phoneId);

            return ApiResponse(_mapper.Map<CustomerViewModel>(updatedCustomer));
        }

        #endregion

        #region Addresses

        [HttpPost("{customerId:guid}/addresses")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> AddAddress(Guid customerId, AddressViewModel addressViewModel)
        {
            var customer = await _customerRepository.GetByIdWithPhonesAndAddresses(customerId);

            if (customer == null) return NotFound();

            var updatedCustomer = await _customerService.AddAddress(customer, _mapper.Map<Address>(addressViewModel));

            return ApiResponse(_mapper.Map<CustomerViewModel>(updatedCustomer));
        }

        [HttpDelete("{customerId:guid}/addresses/{addressId:guid}")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> DeleteAddress(Guid customerId, Guid addressId)
        {
            var customer = await _customerRepository.GetByIdWithPhonesAndAddresses(customerId);

            if (customer == null) return NotFound();

            var updatedCustomer = await _customerService.DeleteAddress(customer, addressId);

            return ApiResponse(_mapper.Map<CustomerViewModel>(updatedCustomer));
        }

        #endregion
    }
}
