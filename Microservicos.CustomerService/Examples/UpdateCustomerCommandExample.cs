using Microservicos.CustomerService.Command.UpdateCustomer;
using Swashbuckle.AspNetCore.Filters;

namespace Microservicos.CustomerService.Examples
{
    public class UpdateCustomerCommandExample : IExamplesProvider<UpdateCustomerCommand>
    {
        public UpdateCustomerCommand GetExamples()
        {
            return new UpdateCustomerCommand
            {
                Name = "Swagger",
                LastName = "Example",
                Email = "swagger.example@email.com",
                UserName = "swagger.example"
            };
        }
    }
}
