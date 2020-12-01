using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface IVehiculoGpsRepository : IGenericRepository<VehiculoGps>
    {
        Task<VehiculoGps> GetVehiculoByDateAsync(int dia, int mes, int anio, int vehiculoId);
    }
}
