using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data.Entities
{
    public class Incidencia:IEntity
    {
        public int Id { get; set; }
        public User User { get; set; }
        
        [Display(Name = "Exceso de Velocidad")]
        [DisplayFormat(DataFormatString = "{0:N1}", ApplyFormatInEditMode = true)]
        public decimal ExcesoVelocidad { get; set; }

        [Display(Name = "Frenazos Bruscos")]
        [DisplayFormat(DataFormatString = "{0:N1}", ApplyFormatInEditMode = true)]
        public decimal FrenazoBrusco { get; set; }

        [Display(Name = "Aceleraciones Bruscas")]
        [DisplayFormat(DataFormatString = "{0:N1}", ApplyFormatInEditMode = true)]
        public decimal AceleracionesBruscas { get; set; }

        [Display(Name = "Giro Brusco")]
        [DisplayFormat(DataFormatString = "{0:N1}", ApplyFormatInEditMode = true)]
        public decimal GiroBrusco { get; set; }
        public bool isActive { get; set; }
    }
}
