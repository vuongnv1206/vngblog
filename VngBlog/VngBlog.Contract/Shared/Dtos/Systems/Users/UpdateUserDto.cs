using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VngBlog.Domain.Entities.Systems
{
    public class UpdateUserDto
    {
        public string FullName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Avatar { get; set; }
    }
}
