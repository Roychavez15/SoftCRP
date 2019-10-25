using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class ClienteViewModel
    {
        public string nit { set; get; }
        public string nombre { set; get; }
        public string correo { set; get; }
        public string correo_factura { set; get; }
    }
}
