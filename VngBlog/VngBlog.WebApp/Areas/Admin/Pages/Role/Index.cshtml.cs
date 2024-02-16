using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VngBlog.Infrastructure.EntityFrameworkCore;


namespace VngBlog.WebApp.Areas.Admin.Pages.Role
{

    public class IndexModel : RolePageModel
    {
        public IndexModel(RoleManager<IdentityRole> roleManager, VngBlogDbContext context) : base(roleManager, context)
        {
        }

        public List<IdentityRole> Roles { get; set; }

        public async Task OnGet()
        {
            Roles = await _roleManager.Roles.ToListAsync();
        }
    }
}
