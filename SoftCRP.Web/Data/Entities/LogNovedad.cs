using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data.Entities
{
    public class LogNovedad : IEntity
    {
        public int Id { get; set; }

        public string Estado {get; set;}
        public string Observaciones { get; set; }
        public DateTime Fecha { get; set; }

        public User Usuario { get; set; }
        public Novedad novedad { get; set; }

    }
}
