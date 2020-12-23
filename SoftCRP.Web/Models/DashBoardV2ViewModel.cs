using Microsoft.AspNetCore.Mvc.Rendering;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class DashBoardV2ViewModel
    {
        
        public EstadisticasV1ViewModel EstadisticasV1ViewModel { get; set; }

        public EstadisticasV2ViewModel EstadisticasV2ViewModel { get; set; }

        public EstadisticasViewModel EstadisticasViewModel { get; set; }


        public string ClienteId { get; set; }
        public IEnumerable<SelectListItem> Clientes { get; set; }

        public string PlacaId { get; set; }
        public IEnumerable<SelectListItem> Placas { get; set; }


        [Display(Name = "Mes")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Placa.")]
        public string MesId { get; set; }
        public IEnumerable<SelectListItem> Meses { get; set; }

        [Display(Name = "Año")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Placa.")]
        public string AnioId { get; set; }
        public IEnumerable<SelectListItem> Anios { get; set; }
    }
}
