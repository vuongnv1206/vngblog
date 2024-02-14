using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.Contract.Shared.Dtos.Posts
{
    public class CreateUpdatePostDto
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? Image { get; set; }
        public string? Content { get; set; }
        public string? Source { get; set; }
        public string[]? EntryTag { get; set; }

    }
}
