using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public class VehiculoGpsRepository : GenericRepository<VehiculoGps>, IVehiculoGpsRepository
    {
        private readonly DataContext _dataContext;

        public VehiculoGpsRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<VehiculoGps> GetVehiculoByDateAsync(int dia, int mes, int anio, int vehiculoId)
        {

            return await _dataContext.vehiculosGps
                .Where(c => c.vehiculo.Id==vehiculoId && c.dia==dia && c.mes==mes && c.anio==anio)
                .FirstOrDefaultAsync();
        }
        public IEnumerable<VehiculoGps> GetVehiculosGPSAsync(int dia, int mes, int anio, string userId, string vehiculoId)
        {
            if (userId != "")
            {
                if (vehiculoId != "")
                {
                    var consultas = _dataContext.vehiculosGps
                    .Include(v => v.vehiculo)
                    .ThenInclude(u => u.user)
                    .Where(v => v.vehiculo.user.Cedula == userId && v.vehiculo.Placa==vehiculoId)
                    .OrderByDescending(p => p.anio)
                    .OrderByDescending(m => m.mes)
                    .OrderByDescending(d => d.dia)
                    .GroupBy(v => v.vehiculo)
                    .ToList()
                    .Select(g => new { g.Key, ultimo = g.FirstOrDefault() }).ToList()
                    .Select(ve => ve.ultimo);

                    return consultas.Count() > 0 ? consultas : null;
                }
                else
                {


                    var consultas = _dataContext.vehiculosGps
                    .Include(v => v.vehiculo)
                    .ThenInclude(u => u.user)
                    .Where(v => v.vehiculo.user.Cedula == userId)
                    .OrderByDescending(p => p.anio)
                    .OrderByDescending(m => m.mes)
                    .OrderByDescending(d => d.dia)
                    .GroupBy(v => v.vehiculo)
                    .ToList()
                    .Select(g => new { g.Key, ultimo = g.FirstOrDefault() }).ToList()
                    .Select(ve => ve.ultimo);

                    return consultas.Count()>0 ? consultas : null;
                }
            }
            else
            {
                var consultas = _dataContext.vehiculosGps
                .Include(v => v.vehiculo)
                .ThenInclude(u => u.user)
                .OrderByDescending(p => p.anio)
                .OrderByDescending(m => m.mes)
                .OrderByDescending(d => d.dia)
                .GroupBy(v => v.vehiculo)
                .ToList()
                .Select(g => new { g.Key, ultimo = g.FirstOrDefault() }).ToList()
                .Select(ve => ve.ultimo);

                return consultas.Count() > 0 ? consultas : null;
            }

        }
        public async Task InsertaVehiculo(VehiculoGps vehiculoGps)
        {
            await _dataContext.vehiculosGps.AddAsync(vehiculoGps);
            await _dataContext.SaveChangesAsync();
        }
    }
}
