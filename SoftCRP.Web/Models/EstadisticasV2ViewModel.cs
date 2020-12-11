using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class EstadisticasV2ViewModel
    {
        //v2


        public IEnumerable<Vehiculo> vehiculos { get; set; }
        public IEnumerable<VehiculoGps> vehiculosGps { get; set; }

        //public IEnumerable<ConductoresViewModel> Conductores { get; set; }
        //public IEnumerable<IngresosTallerViewModel> ingresosTalleres { get; set; }
        //public IEnumerable<SiniestrosViewModel> siniestros { get; set; }

    }
}
