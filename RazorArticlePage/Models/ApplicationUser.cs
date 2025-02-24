using Microsoft.AspNetCore.Identity;

namespace RazorArticlePage.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }

}