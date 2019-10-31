using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class VehiculosClientesViewModel
    {
        [Display(Name = "Codigo")]
        public string codigo_activo { get; set; }
        public string nom_cliente { get; set; }
        public string nit_cliente { get; set; }

        [Display(Name = "Placa")]
        public string placa { get; set; }

        [Display(Name = "Estado")]
        public string historial_vh { get; set; }




    }
}
