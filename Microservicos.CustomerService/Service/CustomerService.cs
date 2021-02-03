using Microservicos.CustomerService.Domain;
using Microservicos.CustomerService.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservicos.CustomerService.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository repository,
            ILogger<CustomerService> logger)
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            if (await _repository.ExistsEmail(customer.Email))
            {
                _logger.LogWarning("Email {0} already exists", customer.Email);
                return null;
            }

            var insertedCustomer = await _repository.Add(customer);
            _logger.LogInformation("Created new customer id {0}", insertedCustomer.Id);
#if DEBUG
            _logger.LogInformation("New customer data: {0}", JsonSerializer.Serialize(insertedCustomer));
#endif

            return insertedCustomer;
        }

        public async Task<int?> DeleteCustomer(Guid id)
        {
            var customer = await _repository.Get(id);

            if (customer is null)
            {
                _logger.LogWarning("Customer with id {0} not found", id);
                return null;
            }

            var cmdResult = await _repository.Delete(customer);
            _logger.LogInformation("Customer with id {0} was deleted", id);

            return cmdResult;
        }

        public async Task<Customer> GetCustomer(Guid id)
        {
            var customer = await _repository.Get(id);

            if (customer is null)
            {
                _logger.LogWarning("Customer with id {0} not found", id);
                return null;
            }

            _logger.LogInformation("Returned customer with id {0}", id);
#if DEBUG
            _logger.LogInformation("Returned customer data: {0}", JsonSerializer.Serialize(customer));
#endif
            return customer;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var customers = await _repository.GetAll();
            _logger.LogInformation("Returned {0} customers", customers.Count);

            return customers;
        }

        public async Task<int?> UpdateCustomer(Guid id, Customer customer)
        {
            var customer_ = await _repository.Get(id);

            if (customer_ is null)
            {
                _logger.LogWarning("Customer with id {0} not found", id);
                return null;
            }

            customer_.Name = customer.Name;
            customer_.LastName = customer.LastName;
            customer_.Email = customer.Email;
            customer_.UserName = customer.UserName;

            var cmdResult = await _repository.Update(customer_);
            _logger.LogInformation("Customer with id {0} was updated", id);

            return cmdResult;
        }
    }
}
