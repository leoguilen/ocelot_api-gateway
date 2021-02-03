using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Microservicos.CustomerService.Command.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<int?>
    {
        [JsonIgnore]
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
