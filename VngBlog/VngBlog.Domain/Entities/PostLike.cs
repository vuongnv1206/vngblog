using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Domain.Abstractions;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.Domain.Entities
{
    public class PostLike : EntityAuditBase<int>
    {
        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
    }
}
