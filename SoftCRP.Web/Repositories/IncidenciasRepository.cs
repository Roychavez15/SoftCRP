using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public class IncidenciasRepository : GenericRepository<Incidencia>, IIncidenciasRepository
    {
        private readonly DataContext _dataContext;

        public IncidenciasRepository(
            DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Incidencia> GetIncidenciaByNitAsync(string Nit)
        {

            return await _dataContext.incidencias
                 .Include(u => u.User)
                 .Where(u => u.User.Cedula == Nit)
                 .FirstOrDefaultAsync();
        }
        public async Task<Incidencia> GetIncidenciaByIdAsync(int Id)
        {

            return await _dataContext.incidencias
                 .Include(u => u.User)
                 .Where(u => u.Id==Id)
                 .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Incidencia>> GetListIncidenciasAsync()
        {

            return await _dataContext.incidencias
                .Include(u => u.User)
                .ToListAsync();
        }
    }
}
