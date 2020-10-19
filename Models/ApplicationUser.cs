using Microsoft.AspNetCore.Identity;

namespace Shortner.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
    }
}