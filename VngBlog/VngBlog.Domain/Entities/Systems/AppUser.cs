using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace VngBlog.Domain.Entities.Systems
{
    public class AppUser : IdentityUser
    {
        [MaxLength(100)]
        public string? FullName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? Dob { get; set; }
        public string? Avatar { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

    }
}
