using System.ComponentModel.DataAnnotations;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.WebApp.Areas.Blog.Models
{
    public class CreatePostModel : Post
    {
        [Display(Name = "Category")]
        public int[]? CategoryIds { get; set; }

        public int[]? TagIds { get; set; }
    }
}
