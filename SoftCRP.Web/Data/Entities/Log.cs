using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data.Entities
{
    public class Log : IEntity
    {
        public int Id { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public DateTime FechaLocal => Fecha.ToLocalTime();

        public string Level { get; set; }
        public string Message { get; set; }

        public string Module { get; set; }

        public string IP { get; set; }

        public User user { get; set; }
    }
}
