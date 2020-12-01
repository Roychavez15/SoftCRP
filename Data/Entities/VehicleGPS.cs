using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Entities
{
    public class VehicleGPS : IEntity
    {
        public int Id { get; set; }

        public string date { get; set; }

        public string year { get; set; }
        public string mounth { get; set; }
        public string trips { get; set; }
        public string kilometerstraveled { get; set; }
        public string speeding { get; set; }
        public string hardbraking { get; set; }
        public string sharpacceleration { get; set; }
        public string sharpturn { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }


    }
}
