using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using VngBlog.Domain.Abstractions;

namespace VngBlog.Domain.Entities.Systems
{
    [Index(nameof(Slug), IsUnique = true)]
    public class PostCategory : EntityAuditBase<int>
    {
        [MaxLength(250)]
        public required string Name { set; get; }
        public  string? Slug { set; get; }
        public int? ParentId { set; get; }
        public bool? IsActive { set; get; }
        [ForeignKey(nameof(ParentId))]
        public virtual PostCategory Parent { get; set; }

    }
}
