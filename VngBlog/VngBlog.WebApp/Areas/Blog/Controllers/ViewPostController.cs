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
        [Route("/post/{categorySlug?}")]
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

        [Route("/post/{postslug}.html")]
        public IActionResult Details(string postslug)
        {
            var categories = GetCategories();
            ViewBag.categories = categories;

            var post = _context.Posts.Where(p => p.Slug == postslug)
                               .Include(p => p.Author)
                               .Include(p => p.PostCategories)
                               .ThenInclude(pc => pc.Category)
                               .Include(p=> p.PostTags)
                               .ThenInclude(pt => pt.Tag)
                               .FirstOrDefault();

            if (post == null)
            {
                return NotFound("Không thấy bài viết");
            }

            Category category = post.PostCategories.FirstOrDefault()?.Category;
            ViewBag.category = category;

            var otherPosts = _context.Posts.Where(p => p.PostCategories.Any(c => c.Category.Id == category.Id))
                                            .Where(p => p.Id != post.Id)
                                            .OrderByDescending(p => p.CreatedTime)
                                            .Take(5);
            ViewBag.otherPosts = otherPosts;
            var tags = post.PostTags.Select(pt => pt.Tag).ToList();
            ViewBag.tags = tags;
            return View(post);
        }


        [Route("/post/tag/{tagName?}")]
        public IActionResult PostsByTag(string tagName)
        {
            var tags = _context.Tags.ToList();
            ViewBag.tags = tags;
            ViewBag.tagName = tagName;

            Tag tag = null;

            if (!string.IsNullOrEmpty(tagName))
            {
                tag = _context.Tags.FirstOrDefault(t => t.Name == tagName);

                if (tag == null)
                {
                    return NotFound("Không thấy tag");
                }
            }

            var posts = _context.Posts
                                .Include(p => p.Author)
                                .Include(p => p.PostTags)
                                    .ThenInclude(pt => pt.Tag)
                                .Include(p => p.PostCategories)
                                    .ThenInclude(pc => pc.Category)
                                .AsQueryable();

            posts = posts.OrderByDescending(p => p.CreatedTime);

            if (tag != null)
            {
                posts = posts.Where(p => p.PostTags.Any(pt => pt.TagId == tag.Id));
            }

            ViewBag.totalPosts = posts.Count();
            ViewBag.tag = tag;

            return View("PostsByTag", posts.ToList());
        }



    }
}
