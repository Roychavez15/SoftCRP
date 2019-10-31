using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class DashboardViewModel
    {
        public List<VehiculosClientesViewModel> VehiculosClientesViewModel { get; set; }
        public int novedad  { get; set; }

    }
}
