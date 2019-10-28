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

        //public async Task<IActionResult> Download(string file)
        //{
        //    //var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
        //    var filePath = Path.Combine(
        //        Directory.GetCurrentDirectory(),
        //        $"wwwroot\\",
        //        file);

        //    if (!System.IO.File.Exists(filePath))
        //    {

        //    }
        //        //return NotFound();

        //    var memory = new MemoryStream();
        //    using (var stream = new FileStream(filePath, FileMode.Open))
        //    {
        //        await stream.CopyToAsync(memory);
        //    }
        //    memory.Position = 0;

        //    //return PhysicalFile()
        //    return File(memory, GetContentType(filePath), file);
        //}

        public FileStreamResult GetFileAsStream(string file)
        {
            //var filePath = Path.Combine(
            //    Directory.GetCurrentDirectory(),
            //    $"wwwroot",
            //    file);

            var filePath = Path.Combine($"{_hostingEnvironment.WebRootPath}\\{file.Substring(1).Replace("/",@"\")}");

            //var rut = _hostingEnvironment.WebRootPath;

            var stream = _fileProvider
                .GetFileInfo(filePath)
                .CreateReadStream();

            return new FileStreamResult(stream, GetContentType(filePath));
        }
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        //Task<string> IFileHelper.UploadFileAsync(IFormFile File, string modulo)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<FileStreamResult> IFileHelper.GetFileAsStream(string file)
        //{
        //    throw new NotImplementedException();
        //}
    }

}

