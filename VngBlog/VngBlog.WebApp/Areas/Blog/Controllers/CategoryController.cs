using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VngBlog.Domain.Entities.Systems;
using VngBlog.Infrastructure.EntityFrameworkCore;

namespace VngBlog.WebApp.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("Admin/Blog/Category/[Action]/{id?}")]
    public class CategoryController : Controller
    {
        private readonly VngBlogDbContext _context;

        public CategoryController(VngBlogDbContext context)
        {
            _context = context;
        }

        // GET: Blog/Category
        public async Task<IActionResult> Index()
        {
            var query = _context.Categories
                .Include(c => c.CategoryParent)
                .Include(c => c.CategoryChildren)
                .Where(c => c.CategoryParent == null);

            return View(await query.ToListAsync());
        }

        // GET: Blog/Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.CategoryParent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Blog/Category/Create
        public async Task<IActionResult> CreateAsync()
        {
            var categories = await _context.Categories
               .Include(c => c.CategoryParent)
               .Include(c => c.CategoryChildren)
               .Where(c => c.CategoryParent == null)
               .ToListAsync();
            categories.Insert(0, new Category()
            {
                Id = -1,
                Name = "Không có danh muc cha",
                IsActive = true
            });

            var items = new List<Category>();
            CreateSelectItems(categories, items, 0);

            ViewData["ParentId"] = new SelectList(items, "Id", "Name");
            return View();
        }

        // POST: Blog/Category/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Slug,ParentId,IsActive")] Category category)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(m => m.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }


            if (ModelState.IsValid)
            {
                if (category.ParentId == -1) category.ParentId = null;
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var categories = await _context.Categories
              .Include(c => c.CategoryParent)
              .Include(c => c.CategoryChildren)
              .Where(c => c.CategoryParent == null).ToListAsync();

            categories.Insert(0, new Category()
            {
                Id = -1,
                Name = "Không có danh mục cha",
                IsActive = true
            });

            var items = new List<Category>();
            CreateSelectItems(categories, items, 0);

            ViewData["ParentId"] = new SelectList(items, "Id", "Name");
            
            return View(category);
        }

        private void CreateSelectItems(List<Category> source, List<Category> des, int level)
        {
            string prefix = string.Concat(Enumerable.Repeat("----", level));
            foreach (var category in source)
            {
              
                des.Add(new Category()
                {
                    Id = category.Id,
                    Name = prefix + " " + category.Name,
                    IsActive = true,
                });
                if (category.CategoryChildren?.Count > 0)
                {
                    CreateSelectItems(category.CategoryChildren.ToList(), des, level + 1);
                }
            }
        }

        // GET: Blog/Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories
            .Include(c => c.CategoryParent)
            .Include(c => c.CategoryChildren)
            .Where(c => c.CategoryParent == null).ToListAsync();

            categories.Insert(0, new Category()
            {
                Id = -1,
                Name = "Không có danh mục cha",
                IsActive = true
            });

            var items = new List<Category>();
            CreateSelectItems(categories, items, 0);

            ViewData["ParentId"] = new SelectList(items, "Id", "Name");

          
            return View(category);
        }

        // POST: Blog/Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Slug,ParentId,IsActive")]Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
            bool canUpdate = true;

            if (category.ParentId == category.Id)
            {
                ModelState.AddModelError(string.Empty, "Phải chọn danh mục cha khác");
                canUpdate = false;
            }

            if (ModelState.IsValid && category.ParentId != category.Id)
            {
                try
                {
                    if (category.ParentId == -1) category.ParentId = null;
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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

            var categories = await _context.Categories
            .Include(c => c.CategoryParent)
            .Include(c => c.CategoryChildren)
            .Where(c => c.CategoryParent == null).ToListAsync();

            categories.Insert(0, new Category()
            {
                Id = -1,
                Name = "Không có danh mục cha",
                IsActive = true
            });

            var items = new List<Category>();
            CreateSelectItems(categories, items, 0);

            ViewData["ParentId"] = new SelectList(items, "Id", "Name");

            return View(category);
        }

        // GET: Blog/Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.CategoryParent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Blog/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories
                .Include(x => x.CategoryChildren).FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            foreach (var cCategory in category.CategoryChildren)
            {
                cCategory.ParentId = category.ParentId;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
