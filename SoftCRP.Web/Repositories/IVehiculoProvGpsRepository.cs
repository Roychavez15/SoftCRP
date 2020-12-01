using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface IVehiculoProvGpsRepository : IGenericRepository<Vehiculo>
    {
        Task<Vehiculo> GetVehiculoByClientePlacaAsync(string id, string placa);
        Task<List<Vehiculo>> GetVehiculosGCBAsync();
    }
}
