using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class SubMotivosIncidenciasViewModel
    {
        public int Id { get; set; }
        public string Submotivo { get; set; }
        public string Usuario_asesor { get; set; }
        public int Dias_sla { get; set; }
        public string Correo_solucionadores { get; set; }

    }
}
