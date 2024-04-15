using Microsoft.AspNetCore.Identity;

namespace Do_An.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
