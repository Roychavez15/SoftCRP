using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data.Entities
{
    public class Vehiculo : IEntity
    {
        public int Id { get; set; }
        public User user { get; set; }

        public string Placa { get; set; }

        public string gps_id { get; set; }
        public string gps_provider { get; set; }


    }
}
