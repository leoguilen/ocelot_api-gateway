using AutoMapper;
using MediatR;
using Microservicos.CustomerService.Command.CreateCustomer;
using Microservicos.CustomerService.Command.DeleteCustomer;
using Microservicos.CustomerService.Command.UpdateCustomer;
using Microservicos.CustomerService.Domain;
using Microservicos.CustomerService.Extensions;
using Microservicos.CustomerService.Queries;
using Microservicos.CustomerService.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicos.CustomerService.Handler
{
    public class CustomerHandler :
        IRequestHandler<CreateCustomerCommand, Customer>,
        IRequestHandler<UpdateCustomerCommand, int?>,
        IRequestHandler<DeleteCustomerCommand, int?>,
        IRequestHandler<GetCustomerQuery, Customer>,
        IRequestHandler<GetCustomersQuery, List<Customer>>
    {
        private readonly ICustomerService _service;
        private readonly HttpContext _context;
        private readonly IMapper _mapper;

        public CustomerHandler(ICustomerService service,
            IMapper mapper, IHttpContextAccessor accessor)
        {
            _service = service
                ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _context = accessor.HttpContext
                ?? throw new ArgumentNullException(nameof(accessor));
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request);
            customer.CreatorId = Guid.Parse(_context.GetUserId());

            return await _service.AddCustomer(customer);
        }

        public async Task<int?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request);
            return await _service.UpdateCustomer(request.Id.Value, customer);
        }

        public async Task<int?> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteCustomer(request.Id);
        }

        public async Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCustomer(request.Id);
        }

        public async Task<List<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCustomers();
        }
    }
}
