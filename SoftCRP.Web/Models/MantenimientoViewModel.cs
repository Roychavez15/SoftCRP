using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class MantenimientoViewModel
    {
        public DashBoardV2ViewModel DashBoardV2ViewModel { get; set; }

        public IEnumerable<ResumenPlacasViewModel> ResumenPlacasViewModel { get; set; }

        public DashboardMantViewModel DashboardMantViewModel { get; set; }

    }
}
