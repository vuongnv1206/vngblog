using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Contract.Shared.Emails;

namespace VngBlog.Contract.Common
{
	public interface IEmailService
	{
		Task SendEmailAsync(MailRequest request, CancellationToken cancellation = new CancellationToken());
	}
}
