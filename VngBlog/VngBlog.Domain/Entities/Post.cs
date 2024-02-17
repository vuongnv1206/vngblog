using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
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
        [DisplayName("Author")]
        public string? AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public virtual AppUser Author { get; set; }
        public string? Image { get; set; }
        public string? Content { get; set; }
        public string? Source { get; set; }
        
        public string? EntryTag { get; set; }
        public int ViewCount { get; set; }
        public PostStatus Status { get; set; }
        public string? Note { get; set; }

        public virtual ICollection<PostCategory> PostCategories { get; set; }


        public virtual ICollection<PostInSeries> PostInSeries { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
        public virtual ICollection<PostActivityLog> PostActivityLogs { get; set; }


    }

    public enum PostStatus
    {
        WaitingForApproval = 1,
        Published = 2,
        Rejected = 3,
        Draft = 4
    }
}
