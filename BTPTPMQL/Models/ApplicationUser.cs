using Microsoft.AspNetCore.Identity;

namespace BTPTPMQL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}