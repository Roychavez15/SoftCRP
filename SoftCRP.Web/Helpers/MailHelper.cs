using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
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
                message.To.Add(new MailboxAddress(multiple_email));
            }

            message.Subject = subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
                
            };
            message.Body = bodyBuilder.ToMessageBody();
            
            using (var client = new SmtpClient())
            {
                client.Connect(smtp, int.Parse(port), false);
                client.Authenticate(from, password);
                client.Send(message);
                client.Disconnect(true);
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

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from));

            message.To.Add(new MailboxAddress(emaildefault));
            foreach (string multiple_email in Multiple)
            {
                message.To.Add(new MailboxAddress(multiple_email));
            }

            message.Subject = subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body

            };
            if (files!=null)
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
                }
            }
            catch(Exception ex)
            {
                var messages = ex.Message;
            }
        }
    }

}
