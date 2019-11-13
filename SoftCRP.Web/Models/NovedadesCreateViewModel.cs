using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class NovedadesCreateViewModel
    {


        [Required]
        public string cedula { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Tipo")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un Tipo.")]
        public string MotivoId { get; set; }
        public IEnumerable<SelectListItem> MotivoTypes { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "SubMotivo")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un Tipo.")]
        public string SubMotivoId { get; set; }
        public IEnumerable<SelectListItem> SubMotivoTypes { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Vía Ingreso")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un Tipo.")]
        public string ViaIngresoId { get; set; }
        public IEnumerable<SelectListItem> ViaIngresoTypes { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Placa")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Placa.")]
        public string PlacaId { get; set; }
        public IEnumerable<SelectListItem> Placas { get; set; }


        [Display(Name = "Novedad")]
        public string Observaciones { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
