using MediatR;
using Microservicos.CustomerService.Command.CreateCustomer;
using Microservicos.CustomerService.Command.DeleteCustomer;
using Microservicos.CustomerService.Command.UpdateCustomer;
using Microservicos.CustomerService.Constants;
using Microservicos.CustomerService.Domain;
using Microservicos.CustomerService.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservicos.CustomerService.Controllers.v1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retornar todos clientes
        /// </summary>
        /// <response code="200">Lista de clientes retornada</response>
        [HttpGet(ApiRoutes.Customer.GetAll)]
        [ProducesResponseType(200, Type = typeof(List<Customer>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator
                .Send(new GetCustomersQuery { });

            return Ok(result);
        }

        /// <summary>
        /// Retorna cliente por id
        /// </summary>
        /// <response code="200">Cliente retornado</response>
        /// <response code="404">Nenhum cliente encontrado</response>
        [HttpGet(ApiRoutes.Customer.Get)]
        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _mediator.Send(
                new GetCustomerQuery
                {
                    Id = id
                });

            if (result is null)
                return NotFound(id);

            return Ok(result);
        }

        /// <summary>
        /// Adicionar cliente
        /// </summary>
        /// <response code="201">Cliente adicionado</response>
        /// <response code="400">Erro de validação ao adicionar cliente</response>
        [HttpPost(ApiRoutes.Customer.Add)]
        [ProducesResponseType(201, Type = typeof(Customer))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Add([FromBody] CreateCustomerCommand request)
        {
            var result = await _mediator.Send(request);

            if (result is null)
                return BadRequest(new
                {
                    Error = "Unexpected error when add customer"
                });

            return Created("", result);
        }

        /// <summary>
        /// Atualizar cliente
        /// </summary>
        /// <response code="200">Cliente atualizado</response>
        /// <response code="400">Erro de validação ao adicionar cliente</response>
        /// <response code="404">Nenhum cliente encontrado</response>
        [HttpPut(ApiRoutes.Customer.Update)]
        [Authorize(Policy = "DeleteUpdatePermission")]
        [ProducesResponseType(201, Type = typeof(Customer))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromRoute] Guid id,
            [FromBody] UpdateCustomerCommand request)
        {
            request.Id = id;
            var result = await _mediator.Send(request);

            if (result is null)
                return NotFound(id);

            if (result.Value < 1)
                return BadRequest(new
                {
                    Error = "Unexpected error when update customer"
                });

            return Ok(new
            {
                Message = $"Customer with id {id} was updated"
            });
        }

        /// <summary>
        /// Deletar cliente
        /// </summary>
        /// <response code="204">Cliente deletado</response>
        /// <response code="404">Nenhum cliente encontrado</response>
        [HttpDelete(ApiRoutes.Customer.Delete)]
        [Authorize(Policy = "DeleteUpdatePermission")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _mediator.Send(
                new DeleteCustomerCommand
                {
                    Id = id
                });

            if (result is null)
                return NotFound(id);

            return NoContent();
        }
    }
}
