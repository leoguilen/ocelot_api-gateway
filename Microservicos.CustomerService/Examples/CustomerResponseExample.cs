using Microservicos.CustomerService.Domain;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Microservicos.CustomerService.Examples
{
    public class CustomerResponseExample : IExamplesProvider<Customer>
    {
        public Customer GetExamples()
        {
            return new Customer
            {
                Id = Guid.NewGuid(),
                Name = "Swagger",
                LastName = "Example",
                Email = "swagger.example@email.com",
                UserName = "swagger.example",
                CreatorId = Guid.NewGuid()
            };
        }
    }
}
