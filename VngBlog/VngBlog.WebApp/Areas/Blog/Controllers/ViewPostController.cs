using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using VngBlog.Domain.Entities.Systems;
using VngBlog.Infrastructure.EntityFrameworkCore;

namespace VngBlog.WebApp.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class ViewPostController : Controller
    {
        private readonly ILogger<ViewPostController> _logger;
        private readonly VngBlogDbContext _context;

        public ViewPostController(ILogger<ViewPostController> logger, VngBlogDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // /post
        // /post/{categorySlug?}
        [Route("/post/categorySlug?")]
        public IActionResult Index(string categorySlug)
        {
            var categories = GetCategories();
            ViewBag.categories = categories;
            ViewBag.categoryslug = categorySlug;

            Category category = null;

            if (!string.IsNullOrEmpty(categorySlug))
            {
                category = _context.Categories.Where(c => c.Slug == categorySlug)
                                    .Include(c => c.CategoryChildren)
                                    .FirstOrDefault();

                if (category == null)
                {
                    return NotFound("Không thấy category");
                }
            }

            var posts = _context.Posts
                                .Include(p => p.Author)
                                .Include(p => p.PostCategories)
                                .ThenInclude(p => p.Category)
                                .AsQueryable();

            posts.OrderByDescending(p => p.CreatedTime);

            if (category != null)
            {
                var ids = new List<int>();
                category.ChildCategoryIds(null, ids);
                ids.Add(category.Id);
                posts = posts.Where(p => p.PostCategories.Where(pc => ids.Contains(pc.CategoryId)).Any());
            }

            ViewBag.totalPosts = posts.Count();



            ViewBag.category = category;

            return View(posts.ToList());

        }

        private List<Category> GetCategories()
        {
            var categories = _context.Categories
                            .Include(c => c.CategoryChildren)
                            .AsEnumerable()
                            .Where(c => c.CategoryParent == null)
                            .ToList();
            return categories;
        }
    }
}
