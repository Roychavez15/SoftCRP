using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class EstadoIncidenciaViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Estado { get; set; }
    }
}
