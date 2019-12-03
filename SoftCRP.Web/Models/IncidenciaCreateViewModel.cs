using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class IncidenciaCreateViewModel
    {
        public string Placa { get; set; }
        public int submotivo { get; set; }
        public string observacion { get; set; }
        public string usuario { get; set; }
        public string motivo { get; set; }
    }
}
