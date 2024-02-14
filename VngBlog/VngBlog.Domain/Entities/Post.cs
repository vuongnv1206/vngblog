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
        public string? AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public virtual AppUser Author { get; set; }
        public int CategoryId { get; set; }
        public string? Image { get; set; }
        public string? Content { get; set; }
        public string? Source { get; set; }

        public string? EntryTag { get; set; }
        public int ViewCount { get; set; }
        public PostStatus Status { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual PostCategory Category { get; set; }


        public virtual ICollection<PostInSeries> PostInSeries { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
        public virtual ICollection<PostActivityLog> PostActivityLogs { get; set; }


    }

    public enum PostStatus
    {
        WaitingForApproval = 1,
        Rejected = 2,
        Published = 3,
        Draft = 4
    }
}
