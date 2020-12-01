using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Entities
{
    public class Vehicle : IEntity
    {
        public int Id { get; set; }

        public string plate { get; set; }

        public string gps_provider { get; set; }
    }
}
