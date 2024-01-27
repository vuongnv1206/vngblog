using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Contract.Common;
using VngBlog.Infrastructure.EntityFrameworkCore;

namespace VngBlog.Infrastructure.Common
{
	public class UnitOfWork : IUnitOfWork 
	{
		private readonly VngBlogDbContext _context;
		public UnitOfWork(VngBlogDbContext context)
		{
			_context = context;
		}
		public void Dispose() => _context.Dispose();
		public Task<int> CommitAsync() => _context.SaveChangesAsync();
	}
}
