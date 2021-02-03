using Microservicos.CustomerService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservicos.CustomerService.Service
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomer(Guid id);
        Task<Customer> AddCustomer(Customer customer);
        Task<int?> UpdateCustomer(Guid id, Customer customer);
        Task<int?> DeleteCustomer(Guid id);
    }
}
