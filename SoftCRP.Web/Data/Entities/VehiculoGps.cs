using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data.Entities
{
    public class VehiculoGps : IEntity
    {
        public int Id { get; set; }
        public Vehiculo vehiculo { get; set; }

        public int dia { get; set; }
        public int mes { get; set; }
        public int anio { get; set; }

        public int trips { get; set; }
        public decimal kilometerstraveled { get; set; }
        public int speeding { get; set; }
        public int hardbraking { get; set; }
        public int sharpacceleration { get; set; }
        public int sharpturn { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal score { get; set; }
        public int ahorro { get; set; }

        public int conductores { get; set; }
        public int talleres { get; set; }
        public int siniestros { get; set; }

        public string usuario { get; set; }

    }
}
