using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class NovedadesViewModel
    {
        public string cedula { get; set; }
        public IEnumerable<Novedad> novedades { get; set; }
    }
}
