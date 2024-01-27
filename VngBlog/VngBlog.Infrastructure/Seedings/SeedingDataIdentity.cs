using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Domain.Systems;
using VngBlog.Infrastructure.EntityFrameworkCore;

namespace VngBlog.Infrastructure.Seedings
{
	public class SeedingDataIdentity
	{
		public async Task SeedData(VngBlogDbContext context)
		{

			var roleUserId = Guid.NewGuid().ToString();
			var roleAdminId = Guid.NewGuid().ToString();

			if (!context.Roles.Any())
			{
				await context.Roles.AddRangeAsync(
				new IdentityRole
				{
					Id = roleUserId,
					Name = "User",
					NormalizedName = "USER",
					ConcurrencyStamp = Guid.NewGuid().ToString()
				},
				new IdentityRole
				{
					Id = roleAdminId,
					Name = "Admin",
					NormalizedName = "ADMIN",
					ConcurrencyStamp = Guid.NewGuid().ToString(),

				});
				await context.SaveChangesAsync();
			}
			var hasher = new PasswordHasher<IdentityUser>();
			var adminId = Guid.NewGuid().ToString();
			var userId = Guid.NewGuid().ToString();
			if (!context.Users.Any())
			{
				await context.Users.AddRangeAsync(
					 new AppUser
					 {
						 Id = adminId,
						 FullName = "Nguyen Van Vuong",
						 Email = "admin@gmail.com",
						 NormalizedEmail = "ADMIN@GMAIL.COM",
						 UserName = "admin@gmail.com",
						 NormalizedUserName = "ADMIN@GMAIL.COM",
						 PasswordHash = hasher.HashPassword(null, "Abcd@1234"),
						 EmailConfirmed = true,
						 SecurityStamp = Guid.NewGuid().ToString(),
						 LockoutEnabled = false,
						 DateCreated = DateTime.Now,
					 },
				 new AppUser
				 {
					 Id = userId,
					 FullName = "Nguyen Hoang",
					 Email = "user@gmail.com",
					 NormalizedEmail = "USER@GMAIL.COM",
					 UserName = "user@gmail.com",
					 NormalizedUserName = "USER@GMAIL.COM",
					 PasswordHash = hasher.HashPassword(null, "Abcd@1234"),
					 EmailConfirmed = true,
					 SecurityStamp = Guid.NewGuid().ToString(),
					 LockoutEnabled = false,
					 DateCreated = DateTime.Now
				 });

				await context.UserRoles.AddRangeAsync(
					new IdentityUserRole<string>
					{
						RoleId = roleAdminId,
						UserId = adminId
					},
				new IdentityUserRole<string>
				{
					RoleId = roleUserId,
					UserId = userId
				});
				await context.SaveChangesAsync();
			}
		}
	}
}
