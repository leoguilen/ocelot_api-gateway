using Microsoft.AspNetCore.Identity;

namespace Microservicos.AuthenticationService.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
