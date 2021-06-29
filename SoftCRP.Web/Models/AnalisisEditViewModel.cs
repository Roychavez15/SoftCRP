using Microsoft.AspNetCore.Http;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class AnalisisEditViewModel
    {
        public int id { get; set; }

        [Required]
        public string cedula { get; set; }

        public TipoAnalisis tipoAnalisis { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Placa")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Placa.")]
        public string Placa { get; set; }

        public string Observaciones { get; set; }
        public ICollection<ArchivoAnalisis> ArchivosAnalisis { get; set; }

        [Display(Name = "Correo")]
        public string SendEmail { get; set; }
    }
}
