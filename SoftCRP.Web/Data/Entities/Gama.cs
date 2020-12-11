using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data.Entities
{
    public class Gama : IEntity
    {
        public int Id { get; set; }
        public string GamaSustituto { get; set; }
        public int Monto { get; set; }
    }
}
