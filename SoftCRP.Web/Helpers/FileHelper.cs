using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Helpers
{
    public class FileHelper : IFileHelper
    {
        private readonly IFileProvider _fileProvider;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileHelper(
            IFileProvider fileProvider,
            IHostingEnvironment hostingEnvironment)
        {
            _fileProvider = fileProvider;
            _hostingEnvironment = hostingEnvironment;
        }

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

        public FileContentResult GetFile(string filename)
        {
            var filePath = Path.Combine($"{_hostingEnvironment.WebRootPath}\\{filename.Substring(1).Replace("/", @"\")}");

            var mimeType = this.GetMimeType(filename);

            byte[] fileBytes=null;

            if (File.Exists(filePath))
            {
                fileBytes = File.ReadAllBytes(filePath);
            }
            else
            {
                // Code to handle if file is not present
            }

            return new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = filename
            };
        }
        private string GetMimeType(string fileName)
        {
            // Make Sure Microsoft.AspNetCore.StaticFiles Nuget Package is installed
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }

}

