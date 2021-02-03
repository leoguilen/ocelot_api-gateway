using Microservicos.AuthenticationService.Commands.ForgotPasswordRecovery;
using Swashbuckle.AspNetCore.Filters;

namespace Microservicos.AuthenticationService.Examples
{
    public class ForgotPasswordRecoveryExample : IExamplesProvider<ForgotPasswordRecoveryCommand>
    {
        public ForgotPasswordRecoveryCommand GetExamples()
        {
            return new ForgotPasswordRecoveryCommand
            {
                Email = "user_email@provider.com",
            };
        }
    }
}
