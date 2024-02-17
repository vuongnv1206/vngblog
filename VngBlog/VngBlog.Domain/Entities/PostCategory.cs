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
    public class PostCategory : EntityBase<int>
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
    }
}
