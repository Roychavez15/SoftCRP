using Microsoft.AspNetCore.Hosting;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Repositories;
using System;
using System.Collections.Generic;
using System.IO;

namespace SoftCRP.Web.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly ILogRepository _logRepository;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public MailHelper(
            ILogRepository logRepository,
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment)
        {
            _logRepository = logRepository;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public void SendMail(string to, string subject, string body)
        {
            var from = _configuration["Mail:From"];
            var smtp = _configuration["Mail:Smtp"];
            var port = _configuration["Mail:Port"];
            var password = _configuration["Mail:Password"];
            var emaildefault = _configuration["Mail:default"];

            string[] Multiple = to.Split(',');

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from));

            message.To.Add(new MailboxAddress(emaildefault));



            foreach (string multiple_email in Multiple)
            {
                message.To.Add(new MailboxAddress(multiple_email.Trim()));
            }

            message.Subject = subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body

            };
            message.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(smtp, int.Parse(port), SecureSocketOptions.StartTls);
                    client.Authenticate(from, password);
                    client.Send(message);
                    client.Disconnect(true);
                    
                };
                
            }
            catch (Exception ex)
            {
                var messages = ex.Message;
            }
        }

        public void SendMailAttachment(string to, string subject, string body, List<IFormFile> files)
        {
            var from = _configuration["Mail:From"];
            var smtp = _configuration["Mail:Smtp"];
            var port = _configuration["Mail:Port"];
            var password = _configuration["Mail:Password"];
            var emaildefault = _configuration["Mail:default"];

            string[] Multiple = to.Split(',');
            string[] Multipledef = emaildefault.Split(',');

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from));

           // message.To.Add(new MailboxAddress(emaildefault));
            foreach (string multiple_email in Multiple)
            {
                if(!string.IsNullOrEmpty(multiple_email.Trim()))
                message.To.Add(new MailboxAddress(multiple_email.Trim()));
            }
            foreach (string multiple_email_def in Multipledef)
            {
                if (!string.IsNullOrEmpty(multiple_email_def.Trim()))
                    message.To.Add(new MailboxAddress(multiple_email_def.Trim()));
            }

            message.Subject = subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };
            if (files != null)
            {
                foreach (IFormFile file in files)
                {
                    bodyBuilder.Attachments.Add(file.FileName, file.OpenReadStream());
                }
            }
            
            message.Body = bodyBuilder.ToMessageBody();
            //message.Attachments = bodyBuilder.Attachments.;
            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(smtp, int.Parse(port), SecureSocketOptions.StartTls);
                    client.Authenticate(from, password);
                    client.Send(message);
                    client.Disconnect(true);
                };
                
            }
            catch (Exception ex)
            {
                var messages = ex.Message;
                //await _logRepository.SaveLogs("Correo Enviado",)
            }
        }

        public void SendMailAlarma(string to, string subject, string body, List<ArchivoTramites> files)
        {
            var from = _configuration["Mail:From"];
            var smtp = _configuration["Mail:Smtp"];
            var port = _configuration["Mail:Port"];
            var password = _configuration["Mail:Password"];
            var emaildefault = _configuration["Mail:tramites"];

            string[] Multiple = to.Split(',');
            string[] Multipledef = emaildefault.Split(',');

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from));

            // message.To.Add(new MailboxAddress(emaildefault));
            foreach (string multiple_email in Multiple)
            {
                if (!string.IsNullOrEmpty(multiple_email.Trim()))
                    message.To.Add(new MailboxAddress(multiple_email.Trim()));
            }
            foreach (string multiple_email_def in Multipledef)
            {
                if (!string.IsNullOrEmpty(multiple_email_def.Trim()))
                    message.To.Add(new MailboxAddress(multiple_email_def.Trim()));
            }

            message.Subject = subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };

            if (files != null)
            {
                foreach (var file in files)
                {
                    var filePath = Path.Combine($"{_hostingEnvironment.WebRootPath}\\{file.ArchivoPath.Substring(1).Replace("/", @"\")}");
                    bodyBuilder.Attachments.Add(filePath);
                }
            }

            message.Body = bodyBuilder.ToMessageBody();
            //message.Attachments = bodyBuilder.Attachments.;
            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(smtp, int.Parse(port), SecureSocketOptions.StartTls);
                    client.Authenticate(from, password);
                    client.Send(message);
                    client.Disconnect(true);
                };

            }
            catch (Exception ex)
            {
                var messages = ex.Message;
                //await _logRepository.SaveLogs("Correo Enviado",)
            }
        }
    }

}
