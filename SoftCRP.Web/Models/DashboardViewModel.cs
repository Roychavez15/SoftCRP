﻿using Microsoft.AspNetCore.Mvc.Rendering;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class DashboardViewModel
    {
        public List<VehiculosClientesViewModel> VehiculosClientesViewModel { get; set; }
        public int novedad  { get; set; }
        public int Tramite { get; set; }
        public int Analisis { get; set; }
        public int Capacitaciones { get; set; }

        public int Transacciones { get; set; }


        //v2
        public int TotalAutos { get; set; }

        public IEnumerable<Vehiculo> vehiculos { get; set; }
        public IEnumerable<VehiculoGps> vehiculosGps { get; set; }

        public IEnumerable<ConductoresViewModel> Conductores { get; set; }
        public IEnumerable<IngresosTallerViewModel> ingresosTalleres { get; set; }
        public IEnumerable<SiniestrosViewModel> siniestros { get; set; }


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
