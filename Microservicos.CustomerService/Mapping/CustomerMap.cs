using AutoMapper;
using Microservicos.CustomerService.Command.CreateCustomer;
using Microservicos.CustomerService.Command.UpdateCustomer;
using Microservicos.CustomerService.Domain;

namespace Microservicos.CustomerService.Mapping
{
    public class CustomerMap : Profile
    {
        public CustomerMap()
        {
            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>()
                .ForMember(x => x.Id, x => x.Ignore());
        }
    }
}
