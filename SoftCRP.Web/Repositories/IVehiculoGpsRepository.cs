using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface IVehiculoGpsRepository : IGenericRepository<VehiculoGps>
    {
        IEnumerable<VehiculoGps> GetVehiculosGPSPositionAsync(int dia, int mes, int anio, string userId, string vehiculoId);
        Task InsertaVehiculo(VehiculoGps vehiculoGps);
        IEnumerable<VehiculoGps> GetVehiculosGPSAsync(int dia, int mes, int anio, string userId, string vehiculoId);
        Task<VehiculoGps> GetVehiculoByDateAsync(int dia, int mes, int anio, int vehiculoId);
    }
}
