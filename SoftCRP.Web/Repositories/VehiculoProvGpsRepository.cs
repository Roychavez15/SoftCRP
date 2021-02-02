using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public class VehiculoProvGpsRepository : GenericRepository<Vehiculo>, IVehiculoProvGpsRepository
    {
        private readonly DataContext _dataContext;

        public VehiculoProvGpsRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<Vehiculo>> GetVehiculosAsync(string id)
        {
            if(id!="")
            {
                return await _dataContext.vehiculos
                    .Include(u=>u.user)
                            .Where(c => c.user.Cedula == id)
                            .ToListAsync();
            }
            else
            {
                return await _dataContext.vehiculos
                    .Include(u => u.user)
                            .ToListAsync();
            }

        }
        public async Task<Vehiculo> GetVehiculoByClientePlacaAsync(string id, string placa)
        {

            return await _dataContext.vehiculos
                .Where(c => c.user.Id == id && c.Placa == placa)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Vehiculo>> GetVehiculosGCBAsync()
        {

            return await _dataContext.vehiculos
                .Include(u=> u.user)
                .Where(c => c.gps_provider.Contains("CGB"))
                .ToListAsync();
        }
        public async Task<Vehiculo> GetVehiculoByIdAsync(int Id)
        {

            return await _dataContext.vehiculos
                .Include(u => u.user)
                .Where(c =>c.Id==Id)
                .FirstOrDefaultAsync();
        }
    }
}
