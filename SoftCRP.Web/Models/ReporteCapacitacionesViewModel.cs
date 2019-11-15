using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class ReporteCapacitacionesViewModel
    {
        [DataType(DataType.Date)]
        public DateTime Inicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fin { get; set; }

        public IEnumerable<Capacitacion> capacitaciones{ get; set; }
    }
}
