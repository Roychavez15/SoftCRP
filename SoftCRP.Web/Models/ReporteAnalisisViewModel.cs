﻿using Microsoft.AspNetCore.Mvc.Rendering;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class ReporteAnalisisViewModel
    {
        [DataType(DataType.Date)]
        public DateTime Inicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fin { get; set; }

        public IEnumerable<Analisis> Analises { get; set; }

        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Cliente")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un Tipo.")]
        public string ClienteId { get; set; }
        public IEnumerable<SelectListItem> Clientes { get; set; }
        
        //public User user { get; set; }
    }
}
