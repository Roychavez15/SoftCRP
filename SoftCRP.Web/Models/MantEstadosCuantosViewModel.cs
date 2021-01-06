using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class MantEstadosCuantosViewModel
    {
        public string Nit_cliente { get; set; }
        public string Cliente { get; set; }
        public string id_estado { get; set; }
        public string estado { get; set; }
        public string cuantos { get; set; }
    }
}
