using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VngBlog.Domain.Abstractions;

namespace VngBlog.Domain.Entities.Systems
{
    public class PostActivityLog : EntityAuditBase<int>
    {

        public int PostId { get; set; }

        public PostStatus FromStatus { set; get; }

        public PostStatus ToStatus { set; get; }

        [MaxLength(500)]
        public string? Note { set; get; }

        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
    }
}
