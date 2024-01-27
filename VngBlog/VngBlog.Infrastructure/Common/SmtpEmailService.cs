using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VngBlog.Contract.Common;
using VngBlog.Contract.Shared.Emails;
using VngBlog.Infrastructure.Configurations;

namespace VngBlog.Infrastructure.Common
{
	public class SmtpEmailService : IEmailService
	{
		private readonly SMTPEmailSettings _settings;
		private readonly SmtpClient _smtpClient;

		public SmtpEmailService(SMTPEmailSettings settings)
		{
			_settings = settings;
			_smtpClient = new SmtpClient();
		}
		public async Task SendEmailAsync(MailRequest request, CancellationToken cancellationToken = default)
		{
			var emailMessage = new MimeMessage
			{

				Sender = new MailboxAddress(_settings.DisplayName, request.From ?? _settings.From),
				Subject = request.Subject,
				Body = new BodyBuilder
				{
					HtmlBody = request.Body
				}.ToMessageBody() // MimeEntity
			};
			if (request.ToAddresses.Any())
			{
				foreach (var toAddress in request.ToAddresses)
				{
					{
						emailMessage.To.Add(MailboxAddress.Parse(toAddress));
					}

				}
			}
			else
			{
				var toAddress = request.ToAddress;
				emailMessage.To.Add(MailboxAddress.Parse(toAddress));
			}

			try
			{
				await _smtpClient.ConnectAsync(_settings.SMTPServer, _settings.Port,
				_settings.UseSsl, cancellationToken); // Task

				await _smtpClient.AuthenticateAsync(_settings.Username, _settings.Password, cancellationToken);

				await _smtpClient.SendAsync(emailMessage, cancellationToken);

				await _smtpClient.DisconnectAsync(quit: true, cancellationToken);
			}
			catch (Exception ex)
			{
				throw new (ex.Message);
			}
			finally
			{
				await _smtpClient.DisconnectAsync(true, cancellationToken);
				_smtpClient.Dispose();
			}
		}
	}
}
