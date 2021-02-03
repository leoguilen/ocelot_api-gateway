using Microservicos.CustomerService.Command.CreateCustomer;
using Swashbuckle.AspNetCore.Filters;

namespace Microservicos.CustomerService.Examples
{
    public class CreateCustomerCommandExample : IExamplesProvider<CreateCustomerCommand>
    {
        public CreateCustomerCommand GetExamples()
        {
            return new CreateCustomerCommand
            {
                Name = "Swagger",
                LastName = "Example",
                Email = "swagger.example@email.com",
                UserName = "swagger.example"
            };
        }
    }
}
