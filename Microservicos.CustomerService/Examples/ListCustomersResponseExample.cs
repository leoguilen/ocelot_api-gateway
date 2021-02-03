using Microservicos.CustomerService.Domain;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace Microservicos.CustomerService.Examples
{
    public class ListCustomersResponseExample : IExamplesProvider<List<Customer>>
    {
        public List<Customer> GetExamples()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Swagger",
                    LastName = "Example 1",
                    Email = "swagger.example1@email.com",
                    UserName = "swagger.example1",
                    CreatorId = Guid.NewGuid()
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Swagger",
                    LastName = "Example 2",
                    Email = "swagger.example2@email.com",
                    UserName = "swagger.example2",
                    CreatorId = Guid.NewGuid()
                }
            };
        }
    }
}
