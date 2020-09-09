using Microsoft.AspNetCore.Http;
using System.Collections.Generic;


namespace SoftCRP.Common.Models
{
    //[ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "json")]
    public class AnalisisRequest
    {
        
        public List<IFormFile> Files { get; set; }

        [FromJson]
        public JsonAnalisisRequest JsonAnalisis { get; set; }
    }
}
