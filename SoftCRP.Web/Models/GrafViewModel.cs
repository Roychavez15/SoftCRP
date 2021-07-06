using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class GrafViewModel
    {
        public string Proceso { get; set; }
        public string SubProceso { get; set; }
        public string Fecha { get; set; }
        public string Cliente { get; set; }
        public string Placa { get; set; }

        public string Usuario { get; set; }
        public string Validez { get; set; }
    }
}
