using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using VngBlog.Domain.Abstractions;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace VngBlog.Domain.Entities.Systems
{
    [Index(nameof(Slug), IsUnique = true)]
    public class Category : EntityAuditBase<int>
    {
        [MaxLength(250)]
        public string Name { set; get; }
        public string? Slug { set; get; }
        [DisplayName("CategoryParent")]
        public int? ParentId { set; get; }
        public bool IsActive { set; get; }
        [ForeignKey(nameof(ParentId))]
        [JsonIgnore]
        public virtual Category? CategoryParent { get; set; }
        // Các Category con
        public ICollection<Category>? CategoryChildren { get; set; }

        public void ChildCategoryIds(ICollection<Category> childcates, List<int> lists)
        {
            if (childcates == null)
                childcates = this.CategoryChildren;

            foreach (Category category in childcates)
            {
                lists.Add(category.Id);
                ChildCategoryIds(category.CategoryChildren, lists);

            }
        }

        public List<Category> ListParents()
        {
            List<Category> li = new List<Category>();
            var parent = this.CategoryParent;
            while (parent != null)
            {
                li.Add(parent);
                parent = parent.CategoryParent;

            }
            li.Reverse();
            return li;
        }

    }
}
