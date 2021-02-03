using Microservicos.CustomerService.Command.DeleteCustomer;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Microservicos.CustomerService.Examples
{
    public class DeleteCustomerCommandExample : IExamplesProvider<DeleteCustomerCommand>
    {
        public DeleteCustomerCommand GetExamples()
        {
            return new DeleteCustomerCommand
            {
                Id = Guid.NewGuid()
            };
        }
    }
}
