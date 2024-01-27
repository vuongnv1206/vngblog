using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VngBlog.Contract.Domains.Interfaces
{
	public interface IAuditable
	{
		DateTimeOffset? CreatedTime { get; set; }
		DateTimeOffset? LastModifiedTime { get; set; }
		public string? CreatedBy { get; set; }
		public string? LastModifiedBy { get; set; }
	}
}
