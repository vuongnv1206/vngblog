using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VngBlog.Domain.Abstractions;

namespace VngBlog.Domain.Entities.Systems
{
    public class Tag : EntityBase<int>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }

    }
}
