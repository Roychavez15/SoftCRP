﻿using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface ICapacitacionesRepository : IGenericRepository<Capacitacion>
    {
        //Task<CapacitacionesViewModel> GetCapacitaciones(string cedula);
        Task<CapacitacionesViewModel> GetCapacitacionesAsync();
        Task<Capacitacion> GetCapacitacionByIdAsync(int? id);
        int GetCountAllCapacitaciones();

        Task<IEnumerable<Capacitacion>> GetCapacitacionReportesAsync(DateTime Inicio, DateTime Fin);
    }
}
