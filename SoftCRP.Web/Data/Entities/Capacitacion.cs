using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data.Entities
{
    public class Capacitacion : IEntity
    {

        public int Id { get; set; }

        //[MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        //public string Cedula { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public DateTime FechaLocal => Fecha.ToLocalTime();

        public string Test { get; set; }

        [Required]
        public int tipoCapacitacionId { get; set; }

        public TipoCapacitacion tipoCapacitacion { get; set; }
        public ICollection<ArchivoCapacitaciones> archivoCapacitaciones { get; set; }

        public User user { get; set; }
    }
}
