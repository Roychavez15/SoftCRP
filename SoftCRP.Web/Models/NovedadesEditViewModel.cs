using Microsoft.AspNetCore.Mvc.Rendering;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class NovedadesEditViewModel
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Motivo { get; set; }
        public string SubMotivo { get; set; }
        public string Via { get; set; }

        public string Novedad { get; set; }

        public string Placa { get; set; }
        public string Solucion { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Estado")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Placa.")]
        public string EstadoId { get; set; }
        public IEnumerable<SelectListItem> Estados { get; set; }


        public ICollection<ArchivoNovedades> archivoNovedades { get; set; }
    }
}
