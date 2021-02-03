using MediatR;
using System;

namespace Microservicos.CustomerService.Command.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<int?>
    {
        public Guid Id { get; set; }
    }
}
