using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Contract.Shared.Dtos.Categories;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.Contract.Shared.Dtos.Posts
{
    public class PostDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string? Content { get; set; }
        public string Slug { get; set; }

        public string? Description { get; set; }
        public string? Image { get; set; }
        public int ViewCount { get; set; }

        public string? Source { get; set; }
        public string? EntryTag { get; set; }
        public DateTimeOffset? CreatedTime { get; set; }
        public DateTimeOffset? LastModifiedTime { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }

        public PostCategoryDto PostCategoryDto { get; set; }
    }
}
