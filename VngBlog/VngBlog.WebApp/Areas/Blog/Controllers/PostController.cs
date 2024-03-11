using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Elfie.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VngBlog.Domain.Entities;
using VngBlog.Domain.Entities.Systems;
using VngBlog.Infrastructure.EntityFrameworkCore;
using VngBlog.WebApp.Areas.Blog.Models;
using VngBlog.WebApp.Helpers;

namespace VngBlog.WebApp.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("admin/blog/post/[action]/{id?}")]
    public class PostController : Controller
    {
        private readonly VngBlogDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        public PostController(VngBlogDbContext context, UserManager<AppUser> userManager,IMapper mapper, HttpClient httpClient)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        // GET: Blog/Post
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:5001/api/Post");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Post>?>(content);
                ViewBag.TotalPost = data.Count();
                return View(data);
            }
            else
            {
                return View("Error");
            }
        }

        // GET: Blog/Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync($"https://localhost:5001/api/Post/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var post = JsonConvert.DeserializeObject<Post>(content);
                return View(post);
            }
            else
            {
                // Handle error
                return View("Error");
            }

        }

        // GET: Blog/Post/Create
        public async Task<IActionResult> CreateAsync()
        {
            var categories = await _context.Categories.ToListAsync();

            ViewData["categories"] = new MultiSelectList(categories, "Id", "Name");

            return View();
        }

        // POST: Blog/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Slug,Description,Image,Content,Source,Note,CategoryIds")] CreatePostModel post)
        {

            var categories = await _context.Categories.ToListAsync();
            ViewData["categories"] = new MultiSelectList(categories, "Id", "Name");

            if (post.Slug == null)
            {
                post.Slug = SlugHelper.GenerateSlug(post.Name);
            }

            if (await _context.Posts.AnyAsync(p => p.Slug == post.Slug))
            {
                ModelState.AddModelError("Slug", "Nhập chuỗi Url khác");
                return View(post);
            }


            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(this.User);
                post.AuthorId = user.Id;
                post.Status = PostStatus.Published;
                _context.Add(post);

                if (post.CategoryIds != null)
                {
                    foreach (var CateId in post.CategoryIds)
                    {
                        _context.Add(new PostCategory()
                        {
                            CategoryId = CateId,
                            Post = post
                        });
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(post);
        }

        // GET: Blog/Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var post = await _context.Posts.FindAsync(id);
            var post = await _context.Posts.Include(p => p.PostCategories).FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            var postEdit = new CreatePostModel()
            {
                Id = post.Id,
                Name = post.Name,
                Content = post.Content,
                Note = post.Note,
                Source = post.Source,
                ViewCount = post.ViewCount,
                
                Image = post.Image,
                Description = post.Description,
                Slug = post.Slug,
                Status = PostStatus.WaitingForApproval,
                CategoryIds = post.PostCategories.Select(pc => pc.CategoryId).ToArray()
            };

            var categories = await _context.Categories.ToListAsync();
            ViewData["categories"] = new MultiSelectList(categories, "Id", "Name");
            return View(postEdit);
        }

        // POST: Blog/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Slug,Description,Image,Content,Source,Note,Id,CategoryIds")] CreatePostModel post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }
            var categories = await _context.Categories.ToListAsync();
            ViewData["categories"] = new MultiSelectList(categories, "Id", "Name");

            if (post.Slug == null)
            {
                post.Slug = SlugHelper.GenerateSlug(post.Name);
            }

            if (await _context.Posts.AnyAsync(p => p.Slug == post.Slug && p.Id != id))
            {
                ModelState.AddModelError("Slug", "Nhập chuỗi Url khác");
                return View(post);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var postUpdate = await _context.Posts.Include(p => p.PostCategories).FirstOrDefaultAsync(p => p.Id == id);
                    if (postUpdate == null)
                    {
                        return NotFound();
                    }

                    postUpdate.Name = post.Name;
                    postUpdate.Description = post.Description;
                    postUpdate.Content = post.Content;
                    postUpdate.Status = PostStatus.WaitingForApproval;
                    postUpdate.Slug = post.Slug;
                    postUpdate.Image = post.Image;
                    postUpdate.Note = post.Note;
                    postUpdate.Source = post.Source;
                    postUpdate.ViewCount = post.ViewCount;
                    postUpdate.Image = post.Image;
                    // Update PostCategory
                    if (post.CategoryIds == null) post.CategoryIds = new int[] { };

                    var oldCateIds = postUpdate.PostCategories.Select(c => c.CategoryId).ToArray();
                    var newCateIds = post.CategoryIds;

                    var removeCatePosts = from postCate in postUpdate.PostCategories
                                          where (!newCateIds.Contains(postCate.CategoryId))
                                          select postCate;
                    _context.PostCategories.RemoveRange(removeCatePosts);

                    var addCateIds = from CateId in newCateIds
                                     where !oldCateIds.Contains(CateId)
                                     select CateId;

                    foreach (var CateId in addCateIds)
                    {
                        _context.PostCategories.Add(new PostCategory()
                        {
                            PostId = id,
                            CategoryId = CateId
                        });
                    }

                    _context.Update(postUpdate);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", post.AuthorId);
            return View(post);
        }

        // GET: Blog/Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Blog/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
