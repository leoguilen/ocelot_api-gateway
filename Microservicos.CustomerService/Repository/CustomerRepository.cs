using Microservicos.CustomerService.Data;
using Microservicos.CustomerService.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservicos.CustomerService.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;

        public CustomerRepository(CustomerDbContext context)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Customer> Add(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<int> Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows;
        }

        public async Task<bool> ExistsEmail(string email)
        {
            return await _context.Customers.AnyAsync(x => x.Email == email);
        }

        public async Task<Customer> Get(Guid id)
        {
            return await _context.Customers
                .AsNoTracking()
                .FirstAsync(x => x.Id == id);
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _context.Customers
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> Update(Customer customer)
        {
            _context.Customers.Update(customer);
            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows;
        }
    }
}
