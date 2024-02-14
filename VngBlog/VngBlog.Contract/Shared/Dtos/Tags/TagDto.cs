using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VngBlog.Contract.Shared.Dtos.Tags
{
    public class TagDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
