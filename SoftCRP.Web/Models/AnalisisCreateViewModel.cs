﻿using Microsoft.AspNetCore.Mvc.Rendering;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class AnalisisCreateViewModel
    {
        //public int id { get; set; }

        [Required]
        public string cedula { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Tipo")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un Tipo.")]
        public int TipoAnalisisId { get; set; }
        public IEnumerable<SelectListItem> AnalisisTypes { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Placa")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Placa.")]
        public string PlacaId { get; set; }
        public IEnumerable<SelectListItem> Placas { get; set; }
        public string Observaciones { get; set; }
    }
}