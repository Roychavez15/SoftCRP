using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class SiniestrosDataViewModel
    {
        public DashBoardV2ViewModel DashBoardV2ViewModel { get; set; }
        public SiniestrosViewModel SiniestrosViewModel { get; set; }
        public IEnumerable<SiniestrosDetalleViewModel> SiniestrosDetalleViewModel { get; set; }
    }
}
