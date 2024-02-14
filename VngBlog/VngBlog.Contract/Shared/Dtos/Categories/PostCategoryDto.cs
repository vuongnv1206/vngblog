using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.Contract.Shared.Dtos.Categories
{
    public class PostCategoryDto
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string Slug { set; get; }
        public int? ParentId { set; get; }
        public bool? IsActive { set; get; }
        public PostCategory Parent { get; set; }
    }
}
