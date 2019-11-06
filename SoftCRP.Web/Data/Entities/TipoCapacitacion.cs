using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data.Entities
{
    public class TipoCapacitacion : IEntity
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "The field {0} only can contain a maximum {1} characters")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Tipo { get; set; }
    }
}
