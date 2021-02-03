using Microservicos.AuthenticationService.Domain;
using Swashbuckle.AspNetCore.Filters;

namespace Microservicos.AuthenticationService.Examples
{
    public class AuthenticationResultExample : IExamplesProvider<AuthenticationResult>
    {
        public AuthenticationResult GetExamples()
        {
            return new AuthenticationResult
            {
                Message = "Sucesso",
                Success = true,
                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImp0aSI6IjRjZTcxNzQwLTBiYjgtNGI2ZS04NTgxLTBiNzQ1MmY0NDdjNCIsImlhdCI6MTYxMTk3ODc5NiwiZXhwIjoxNjExOTgyMzk3fQ._pmo0m1uZSvkhZccDR9-VGmdF-hvvol6QXEL1IvKeHE"
            };
        }
    }
}
