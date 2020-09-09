using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftCRP.Common.Models
{
    public class TramitesRequest
    {
        public List<IFormFile> Files { get; set; }

        [FromJson]
        public JsonTramitesRequest JsonTramites { get; set; }
    }
}
