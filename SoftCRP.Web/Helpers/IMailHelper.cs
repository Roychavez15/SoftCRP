using Microsoft.AspNetCore.Http;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Helpers
{
    public interface IMailHelper
    {
        void SendMailAlarma(string to, string subject, string body, List<ArchivoTramites> files);
        void SendMail(string to, string subject, string body);
        void SendMailAttachment(string to, string subject, string body, List<IFormFile> files);
    }

}
