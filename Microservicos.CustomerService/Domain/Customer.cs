using System;

namespace Microservicos.CustomerService.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid CreatorId { get; set; }
    }
}
