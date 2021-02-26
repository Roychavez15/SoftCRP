using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class GraficosViewModel
    {
        public string ClienteId { get; set; }
        public IEnumerable<SelectListItem> Clientes { get; set; }

        public string PlacaId { get; set; }
        public IEnumerable<SelectListItem> Placas { get; set; }

        public string Markers { get; set; }
    }
}
