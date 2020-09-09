using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftCRP.Common.Models
{
    public class JsonTramitesRequest
    {
        [Required]
        public string cedula { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int TipoTramiteId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string PlacaId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Mes { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Anio { get; set; }
        public string Observaciones { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public Guid UserId { get; set; }
    }
}
