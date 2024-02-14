using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Contract.Shared.Dtos.Systems.Roles;

namespace VngBlog.Contract.Shared.Extensions
{
	public static class ClaimExtensions
	{
		public static void GetPermissions(this List<RoleClaimDto> allPermissions, Type policy)
		{
			FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
			foreach (FieldInfo fi in fields)
			{
				var attribute = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
				string displayName = fi.GetValue(null).ToString();
				var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
				if (attributes.Length > 0)
				{
					var description = (DescriptionAttribute)attribute[0];
					displayName = description.Description;
				}
				allPermissions.Add(new RoleClaimDto { Value = fi.GetValue(null).ToString(), Type = "Permission", DisplayName = displayName });
			}
		}
		public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string permission)
		{
			var allClaims = await roleManager.GetClaimsAsync(role);
			if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
			{
				await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
			}
		}
	}
}
