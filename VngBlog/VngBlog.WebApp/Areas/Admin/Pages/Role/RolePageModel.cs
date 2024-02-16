using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using VngBlog.Infrastructure.EntityFrameworkCore;

namespace VngBlog.WebApp.Areas.Admin.Pages.Role
{
    public class RolePageModel : PageModel
    {
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly VngBlogDbContext _context;

        [TempData]
        public string StatusMessage { get; set; }
        public RolePageModel(RoleManager<IdentityRole> roleManager, VngBlogDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
    }
}
