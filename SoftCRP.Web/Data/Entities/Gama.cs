using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data.Entities
{
    public class Gama : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Gama Sustituto")]
        public string GamaSustituto { get; set; }

        [Display(Name = "Monto por Día")]
        public int Monto { get; set; }
    }
}
