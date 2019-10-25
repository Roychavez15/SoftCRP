using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Helpers
{
    public interface IFileHelper
    {
        Task<string> UploadFileAsync(IFormFile File, string modulo);
    }
}
