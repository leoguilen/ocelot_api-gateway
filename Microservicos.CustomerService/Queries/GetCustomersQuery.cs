using MediatR;
using Microservicos.CustomerService.Domain;
using System.Collections.Generic;

namespace Microservicos.CustomerService.Queries
{
    public class GetCustomersQuery : IRequest<List<Customer>>
    {
    }
}
