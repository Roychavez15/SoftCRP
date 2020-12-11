using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class EstadisticasV1ViewModel
    {
        public int TotalAutos { get; set; }
        public List<VehiculosClientesViewModel> VehiculosClientesViewModel { get; set; }
        public int novedad { get; set; }
        public int Tramite { get; set; }
        public int Analisis { get; set; }
        public int Capacitaciones { get; set; }

        public int Transacciones { get; set; }
    }
}
