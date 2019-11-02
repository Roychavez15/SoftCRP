using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data.Entities
{
    public class ArchivoCapacitaciones : IEntity
    {
        public int Id { get; set; }

        public string ArchivoPath { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public DateTime FechaLocal => Fecha.ToLocalTime();

        public string TipoArchivo { get; set; }

        public long tamanio { get; set; }

        public Capacitacion capacitacion { get; set; }

        public User user { get; set; }
    }
}
