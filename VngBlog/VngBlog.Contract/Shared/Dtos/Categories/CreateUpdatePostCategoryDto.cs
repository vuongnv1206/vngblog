using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VngBlog.Contract.Shared.Dtos.Categories
{
    public class CreateUpdatePostCategoryDto
    {
        [MaxLength(250)]
        public required string Name { set; get; }
        public required string Slug { set; get; }
        public int? ParentId { set; get; }
        public bool IsActive { set; get; }
    }
}
