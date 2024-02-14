using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VngBlog.Domain.Abstractions;

namespace VngBlog.Domain.Entities.Systems
{
    [Table("Series")]
    [Index(nameof(Slug), IsUnique = true)]
    public class Series : EntityAuditBase<int>
    {

        public required string Name { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }

        public string? Slug { get; set; }
        public string? Image { set; get; }
        public string? Content { get; set; }

        public virtual ICollection<PostInSeries> PostInSeries { get; set; }

    }
}
