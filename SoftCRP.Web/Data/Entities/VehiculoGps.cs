using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data.Entities
{
    public class VehiculoGps
    {
        public int Id { get; set; }
        public Vehiculo vehiculo { get; set; }

        public int dia { get; set; }
        public int mes { get; set; }
        public int anio { get; set; }

        public int trips { get; set; }
        public int kilometerstraveled { get; set; }
        public int speeding { get; set; }
        public int hardbraking { get; set; }
        public int sharpacceleration { get; set; }
        public int sharpturn { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

    }
}
