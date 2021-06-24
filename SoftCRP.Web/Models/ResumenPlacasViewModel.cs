using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class ResumenPlacasViewModel
    {
        public string Nit_cliente { get; set; }
        public string Cliente { get; set; }
        public string placa { get; set; }
        public string usuario { get; set; }
        public string evento { get; set; }
        public string tipo { get; set; }
        public string estado { get; set; }
        public string detalle_cita { get; set; }
        public string detalle_oc { get; set; }
        public string usuario_asesor { get; set; }
        public string ciudad_ult_mmto { get; set; }
        public string ult_rutina { get; set; }
        public string fecha_mmto { get; set; }

        public string anio { get; set; }

        public string mes { get; set; }
        public string mesletras { get; set; }
    }
}
