using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.Contract.Shared.Dtos.Posts
{
    public class CreateUpdatePostDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Category")]
        public int[]? CategoryIds { get; set; }
        [Display(Name = "Tag")]
        public int[]? TagIds { get; set; }
        public string? Image { get; set; }
        public string? Content { get; set; }
        public string? Source { get; set; }
        public string? Note { get; set; }

    }
}
