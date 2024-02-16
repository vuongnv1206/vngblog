using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using VngBlog.Infrastructure.EntityFrameworkCore;

namespace VngBlog.WebApp.Areas.Admin.Pages.Role
{

    public class EditModel : RolePageModel
    {
        public EditModel(RoleManager<IdentityRole> roleManager, VngBlogDbContext context) : base(roleManager, context)
        {
        }

        public class InputModel
        {
            [Display(Name = "Role Name")]
            [Required(ErrorMessage = "Phải nhập {0}")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} phải dài {2} đến {1} ký tự")]
            public string Name { get; set; }
        }

        [BindProperty]
        public InputModel Input { set; get; }
        public IdentityRole role { get; set; }

        public async Task<IActionResult> OnGet(string roleid)
        {
            if (roleid == null) return NotFound("Not found role");

            role = await _roleManager.FindByIdAsync(roleid);
            if (role != null)
            {
                Input = new InputModel()
                {
                    Name = role.Name
                };
                return Page();
            }
            return NotFound("Not found role");
        }



        public async Task<IActionResult> OnPostAsync(string roleid)
        {
            if (roleid == null) return NotFound("Not found rolee");
            role = await _roleManager.FindByIdAsync(roleid);
            if (role == null) return NotFound("Not found role");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            role.Name = Input.Name;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                StatusMessage = $"Bạn vừa đổi tên: {Input.Name}";
                return RedirectToPage("./Index");
            }
            else
            {
                result.Errors.ToList().ForEach(error => {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }
            return Page();
        }
    }
}

