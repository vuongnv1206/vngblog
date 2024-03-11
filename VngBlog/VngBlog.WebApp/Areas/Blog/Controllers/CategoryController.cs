using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VngBlog.Domain.Entities.Systems;
using VngBlog.Infrastructure.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using VngBlog.Contract.Shared.Dtos.Categories;
using AutoMapper;

namespace VngBlog.WebApp.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("Admin/Blog/Category/[Action]/{id?}")]
    public class CategoryController : Controller
    {
        private readonly VngBlogDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public CategoryController(VngBlogDbContext context, HttpClient httpClient, IMapper mapper)
        {
            _context = context;
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:5001/api/Category");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var topLevelCategories = JsonConvert.DeserializeObject<List<Category>?>(content);
                return View(topLevelCategories);
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync($"https://localhost:5001/api/Category/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<Category>(content);
                return View(category);
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }

        // GET: Blog/Category/Create
        public async Task<IActionResult> CreateAsync()
        {
            var allCategories = await _context.Categories
        .Include(c => c.CategoryParent)
        .Include(c => c.CategoryChildren)
        .ToListAsync();

            // Lọc ra các categories không có parent (cấp cao nhất)
            var topLevelCategories = allCategories.Where(c => c.CategoryParent == null).ToList();

       
            topLevelCategories.Insert(0, new Category()
            {
                Id = -1,
                Name = "Không có danh muc cha",
                IsActive = true
            });

            var items = new List<Category>();
            CreateSelectItems(topLevelCategories, items, 0);

            ViewData["ParentId"] = new SelectList(items, "Id", "Name");
            return View();
        }

        // POST: Blog/Category/Create
      
        [HttpPost]
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

                var categoryDto = _mapper.Map<CreateUpdateCategoryDto>(category);
                var jsonContent = JsonConvert.SerializeObject(categoryDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:5001/api/Category", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle error
                    return View("Error");
                }
            }
            #region
            var allCategories = await _context.Categories
       .Include(c => c.CategoryParent)
       .Include(c => c.CategoryChildren)
       .ToListAsync();

            // Lọc ra các categories không có parent (cấp cao nhất)
            var topLevelCategories = allCategories.Where(c => c.CategoryParent == null).ToList();

            topLevelCategories.Insert(0, new Category()
            {
                Id = -1,
                Name = "Không có danh mục cha",
                IsActive = true
            });

            var items = new List<Category>();
            CreateSelectItems(topLevelCategories, items, 0);

            ViewData["ParentId"] = new SelectList(items, "Id", "Name");
            #endregion
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

            var allCategories = await _context.Categories
        .Include(c => c.CategoryParent)
        .Include(c => c.CategoryChildren)
        .ToListAsync();

            // Lọc ra các categories không có parent (cấp cao nhất)
            var topLevelCategories = allCategories.Where(c => c.CategoryParent == null).ToList();

            topLevelCategories.Insert(0, new Category()
            {
                Id = -1,
                Name = "Không có danh mục cha",
                IsActive = true
            });

            var items = new List<Category>();
            CreateSelectItems(topLevelCategories, items, 0);

            ViewData["ParentId"] = new SelectList(items, "Id", "Name");

          
            return View(category);
        }

        // POST: Blog/Category/Edit/5
        [HttpPost]
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

            // Kiem tra thiet lap muc cha phu hop
            if (canUpdate && category.ParentId != null)
            {

                var childCates = await _context.Categories
                            .Include(c => c.CategoryChildren)
                             .Where(c => c.ParentId == category.Id)
                             .ToListAsync();
               


                // Func check Id 
                Func<List<Category>, bool> checkCateIds = null;
                checkCateIds = (cates) =>
                {
                    foreach (var cate in cates)
                    {
                        Console.WriteLine(cate.Name);
                        if (cate.Id == category.ParentId)
                        {
                            canUpdate = false;
                            ModelState.AddModelError(string.Empty, "Phải chọn danh mục cha khácXX");
                            return true;
                        }
                        if (cate.CategoryChildren != null)
                            return checkCateIds(cate.CategoryChildren.ToList());

                    }
                    return false;
                };
                // End Func 
                checkCateIds(childCates.ToList());
            }

            if (ModelState.IsValid && category.ParentId != category.Id)
            {
                try
                {
                    if (category.ParentId == -1) category.ParentId = null;
                    //_context.Update(category);
                    //await _context.SaveChangesAsync();
                    var categoryDto = _mapper.Map<CreateUpdateCategoryDto>(category);
                    var jsonContent = JsonConvert.SerializeObject(categoryDto);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PutAsync($"https://localhost:5001/api/Category/{id}", content);
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

            var allCategories = await _context.Categories
       .Include(c => c.CategoryParent)
       .Include(c => c.CategoryChildren)
       .ToListAsync();

            // Lọc ra các categories không có parent (cấp cao nhất)
            var topLevelCategories = allCategories.Where(c => c.CategoryParent == null).ToList();

            topLevelCategories.Insert(0, new Category()
            {
                Id = -1,
                Name = "Không có danh mục cha",
                IsActive = true
            });

            var items = new List<Category>();
            CreateSelectItems(topLevelCategories, items, 0);

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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:5001/api/Category/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }


        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
