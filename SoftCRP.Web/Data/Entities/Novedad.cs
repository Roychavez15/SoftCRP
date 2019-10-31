using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data.Entities
{
    public class Novedad : IEntity
    {
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Cedula { get; set; }

        [MaxLength(10, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Placa { get; set; }

        [Required]
        public string Motivo { get; set; }

        [Required]
        public string SubMotivo { get; set; }

        [Required]
        public string ViaIngreso { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public DateTime FechaLocal => Fecha.ToLocalTime();

        public string Observaciones { get; set; }


        public string Solucion { get; set; }

        [Display(Name = "Estado")]
        public string EstadoSolucion { get; set; }

        //public TipoNovedades tipoNovedades { get; set; }
        public ICollection<ArchivoNovedades> archivoNovedades { get; set; }

        public User user { get; set; }
        public User userSolucion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? FechaSolucion { get; set; }
        //public DateTime? FechaLocalSolucion => FechaSolucion.ToLocalTime();
    }
}
