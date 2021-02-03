using Microservicos.CustomerService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservicos.CustomerService.Repository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAll();
        Task<Customer> Get(Guid id);
        Task<bool> ExistsEmail(string email);
        Task<Customer> Add(Customer customer);
        Task<int> Update(Customer customer);
        Task<int> Delete(Customer customer);
    }
}
