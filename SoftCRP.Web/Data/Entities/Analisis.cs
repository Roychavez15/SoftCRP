﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data.Entities
{
    public class Analisis : IEntity
    {

        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Cedula { get; set; }

        [MaxLength(10, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Placa { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public DateTime FechaLocal => Fecha.ToLocalTime();

        public string Observaciones { get; set; }

        [Required]
        public int tipoAnalisisId { get; set; }

        public TipoAnalisis tipoAnalisis { get; set; }
        public ICollection<ArchivoAnalisis> ArchivosAnalisis { get; set; }

        public User user { get; set; }

        [NotMapped]
        public User userCliente
        {
            get;
            set;
        }

    }
}
