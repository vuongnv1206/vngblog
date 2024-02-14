using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using VngBlog.Domain.Entities.Systems;
using System.ComponentModel.DataAnnotations;

namespace VngBlog.Contract.Shared.Dtos.Systems.Users
{
    public class UserDto
    {
        public string Id { get; set; }
        [MaxLength(100)]
        public string? FullName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? Dob { get; set; }
        public string? Avatar { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IList<string> Roles { get; set; }
        //public DateTime? LastLoginDate { get; set; }
    }
}
