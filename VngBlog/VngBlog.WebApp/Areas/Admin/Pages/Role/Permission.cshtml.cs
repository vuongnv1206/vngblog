//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using VngBlog.Contract.Systems.Roles;
//using VngBlog.Infrastructure.EnityFrameworkCore;

//namespace VngBlog.WebApp.Areas.Admin.Pages.Role
//{
//    public class PermissionModel : RolePageModel
//    {
//        private readonly IRoleService _roleService;

//        public PermissionModel(
//            RoleManager<IdentityRole> roleManager,
//            VngBlogDbContext context,
//            IRoleService roleService) : base(roleManager, context)
//        {
//            _roleService = roleService;
//        }

//        [BindProperty(SupportsGet = true)]
//        public PermissionDto PermissionDto { get; set; }

//        public async Task<IActionResult> OnGetAsync(string roleid)
//        {
//            PermissionDto = await _roleService.GetAllRolePermission(roleid);
//            return Page();
//        }

//        public async Task<IActionResult> OnPostAsync(string roleid)
//        {
            
//            var roleClaimsUpdate = new List<RoleClaimDto>();

//            for (int i = 0; i < PermissionDto.RoleClaims.Count; i++)
//            {
//                roleClaimsUpdate.Add(new RoleClaimDto
//                {
//                    DisplayName = PermissionDto.RoleClaims[i].DisplayName,
//                    Selected = PermissionDto.RoleClaims[i].Selected,
//                    Type = PermissionDto.RoleClaims[i].Type,
//                    Value = PermissionDto.RoleClaims[i].Value
//                });
//            }

//            var updateValues = new PermissionDto
//            {
//                RoleId = roleid,
//                RoleClaims = roleClaimsUpdate
//            };

//            await _roleService.SavePermission(updateValues);
//            StatusMessage = "Updated permission successfully!";
//            return Page();
//        }



//    }
//}
