using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class SiniestrosDetalleViewModel
    {
        public string placa { get; set; }
        public string usuario { get; set; }
        public int evento { get; set; }
        public string estado { get; set; }
        public string tiempo_siniestro { get; set; }
        public string cliente { get; set; }
        public string nombre_cliente { get; set; }
        public string tipo_siniestro { get; set; }
        public string anotaciones { get; set; }
        public string fecha_siniestro { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }
    }
}
