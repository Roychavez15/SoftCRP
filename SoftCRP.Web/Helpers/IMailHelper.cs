﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Helpers
{
    public interface IMailHelper
    {
        void SendMail(string to, string subject, string body);
        void SendMailAttachment(string to, string subject, string body, List<IFormFile> files);
    }

}
