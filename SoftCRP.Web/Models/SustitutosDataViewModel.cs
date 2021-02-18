using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class SustitutosDataViewModel
    {
        public DashBoardV2ViewModel DashBoardV2ViewModel { get; set; }

        public IEnumerable<DiasSustitutosViewModel> DiasSustitutosViewModel { get; set; }

        public SustitutosCuantosViewModel SustitutosCuantosViewModel { get; set; }
    }
}
