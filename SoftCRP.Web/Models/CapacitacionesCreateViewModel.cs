using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class CapacitacionesCreateViewModel
    {

        //[Required]
        //public string cedula { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Tipo")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un Tipo.")]
        public int tipoCapacitacionId { get; set; }
        public IEnumerable<SelectListItem> CapacitacionesTypes { get; set; }

        [Display(Name = "Link")]
        public string Test { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
