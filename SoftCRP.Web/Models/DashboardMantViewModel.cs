using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class DashboardMantViewModel
    {
        public int vehiculo_ingresado { get; set; }
        public int cita_confirmada { get; set; }
        public int vehiculo_fuera_taller { get; set; }
        public int vehiculo_registrado_ingreso { get; set; }

    }
}
