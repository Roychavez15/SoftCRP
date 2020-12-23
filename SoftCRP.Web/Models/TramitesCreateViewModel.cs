using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class TramitesCreateViewModel
    {

        [Required]
        public string cedula { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Tipo")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un Tipo.")]
        public int TipoTramiteId { get; set; }
        public IEnumerable<SelectListItem> TramitesTypes { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Placa")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Placa.")]
        public string PlacaId { get; set; }
        public IEnumerable<SelectListItem> Placas { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Mes")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Placa.")]
        public string MesId { get; set; }
        public IEnumerable<SelectListItem> Meses { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Año")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Placa.")]
        public string AnioId { get; set; }
        public IEnumerable<SelectListItem> Anios { get; set; }

        public string Observaciones { get; set; }

        public List<IFormFile> Files { get; set; }

        
        public IEnumerable<SelectListItem> Ciudades { get; set; }
        [Display(Name = "Ciudad")]
        public string CiudadId { get; set; }

        public IEnumerable<SelectListItem> Dias { get; set; }
        [Display(Name = "Dia")]
        public string DiaId { get; set; }


        public string validez { get; set; }
        public string fechas { get; set; }
    }
}
