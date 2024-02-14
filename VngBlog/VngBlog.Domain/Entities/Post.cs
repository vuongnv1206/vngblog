using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using VngBlog.Domain.Abstractions;

namespace VngBlog.Domain.Entities.Systems
{
    [Table("Posts")]
    [Index(nameof(Slug), IsUnique = true)]
    public class Post : EntityAuditBase<int>
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Description { get; set; }

        public Guid CategoryId { get; set; }
        public string? Image { get; set; }
        public string? Content { get; set; }
        public string? Source { get; set; }

        public string? Tags { get; set; }

        public int ViewCount { get; set; }
        public PostStatus Status { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual PostCategory Category { get; set; }


    }

    public enum PostStatus
    {
        Draft = 1,
        WaitingForApproval = 2,
        Rejected = 3,
        Published = 4
    }
}
