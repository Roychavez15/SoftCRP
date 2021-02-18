using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data.Entities
{
    public class Acceso: IEntity
    {
        public int Id { get; set; }
        public User User { get; set; }

        [Display(Name = "Información")]        
        public bool Informacion { get; set; }

        [Display(Name = "Análisis")]
        public bool Analisis { get; set; }

        [Display(Name = "Seguimiento")]
        public bool Seguimiento { get; set; }

        [Display(Name = "Trámites")]
        public bool Tramites { get; set; }

        [Display(Name = "Capacitaciones")]
        public bool Capacitaciones { get; set; }

        [Display(Name = "Conducción")]
        public bool Conduccion { get; set; }

        [Display(Name = "Mantenimiento")]
        public bool Mantenimiento { get; set; }

        [Display(Name = "Siniestros")]
        public bool Siniestros { get; set; }

        [Display(Name = "Sustitutos")]
        public bool Sustitutos { get; set; }

        [Display(Name = "Gráficos")]
        public bool Graficos { get; set; }

        public bool isActive { get; set; }
    }
}
