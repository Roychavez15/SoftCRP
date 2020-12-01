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
    }
}
