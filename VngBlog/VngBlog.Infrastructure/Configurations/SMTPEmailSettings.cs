﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VngBlog.Infrastructure.Configurations
{
	public class SMTPEmailSettings
	{
		public string DisplayName { get; set; }
		public bool EnableVerification { get; set; }
		public string From { get; set; }
		public string SMTPServer { get; set; }
		public bool UseSsl { get; set; }
		public int Port { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
