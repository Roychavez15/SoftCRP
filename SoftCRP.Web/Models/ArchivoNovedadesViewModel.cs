using Microsoft.AspNetCore.Http;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class ArchivoNovedadesViewModel: ArchivoNovedades
    {
        [Display(Name = "Archivo")]
        public IFormFile Archivo { get; set; }

        public string nombre { get; set; }
    }
}
