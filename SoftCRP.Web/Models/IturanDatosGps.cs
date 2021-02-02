using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class IturanDatosGps
    {
        public string placa { get; set; }
        public int viajes { get; set; }
        public decimal kilometros { get; set; }
        public int speeding { get; set; } //exceso de velocidad
        public int hardbraking { get; set; } //frenado brusco
        public int sharpacceleration { get; set; } //aceleracion brusca
        public int sharpturn { get; set; } //giro brusco
        public string latitud { get; set; }
        public string longitud { get; set; }
    }
}
