using Microservicos.AuthenticationService.Domain;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Microservicos.AuthenticationService.Examples
{
    public class ErrorResponseExample : IExamplesProvider<ErrorResponse>
    {
        public ErrorResponse GetExamples()
        {
            return new ErrorResponse
            {
                Errors = new List<ErrorModel>
                {
                    new ErrorModel
                    {
                        FieldName = "Email",
                        Message = "Invalid email address"
                    }
                }
            };
        }
    }
}
