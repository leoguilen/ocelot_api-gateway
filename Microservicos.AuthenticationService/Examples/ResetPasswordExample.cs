using Microservicos.AuthenticationService.Commands.ResetPassword;
using Swashbuckle.AspNetCore.Filters;

namespace Microservicos.AuthenticationService.Examples
{
    public class ResetPasswordExample : IExamplesProvider<ResetPasswordCommand>
    {
        public ResetPasswordCommand GetExamples()
        {
            return new ResetPasswordCommand
            {
                Token = "3cd270d82d4946c311e3a538f9f96ed3b19fe99d242852aadb1caaa3adbe4f04",
                Email = "example_register@email.com",
                Password = "Example321#",
                ConfirmPassword = "Example321#",
            };
        }
    }
}
