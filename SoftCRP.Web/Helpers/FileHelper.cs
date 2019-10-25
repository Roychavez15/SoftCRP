using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Helpers
{
    public class FileHelper : IFileHelper
    {
        [DisableRequestSizeLimit]
        public async Task<string> UploadFileAsync(IFormFile File, string modulo)
        {
            var guid = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(File.FileName);
            var file = $"{guid}_{File.FileName}";
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"wwwroot\\Files\\{modulo}",
                file);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await File.CopyToAsync(stream);                
            }

            return $"~/Files/{modulo}/{file}";
        }
    }

}

