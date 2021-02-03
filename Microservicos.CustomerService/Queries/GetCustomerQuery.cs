using MediatR;
using Microservicos.CustomerService.Domain;
using System;

namespace Microservicos.CustomerService.Queries
{
    public class GetCustomerQuery : IRequest<Customer>
    {
        public Guid Id { get; set; }
    }
}
