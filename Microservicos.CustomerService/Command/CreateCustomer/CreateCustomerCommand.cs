using MediatR;
using Microservicos.CustomerService.Domain;

namespace Microservicos.CustomerService.Command.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
