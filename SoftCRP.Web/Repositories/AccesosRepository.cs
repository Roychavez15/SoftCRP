using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public class AccesosRepository : GenericRepository<Acceso>, IAccesosRepository
    {
        private readonly DataContext _dataContext;
        private readonly IDatosRepository _datosRepository;

        public AccesosRepository(DataContext dataContext,
            IDatosRepository datosRepository) : base(dataContext)
        {
            _dataContext = dataContext;
            _datosRepository = datosRepository;
        }
        public async Task<IEnumerable<Acceso>> GetListAccesosAsync()
        {
            
            return await _dataContext.accesos
                .Include(u => u.User)
                .ToListAsync();
        }
        public async Task<int> Actualiza(IEnumerable<Acceso> accesos)
        {
            _dataContext.accesos.UpdateRange(accesos);
            return await _dataContext.SaveChangesAsync();
        }
        public async Task<Acceso> GetAcceso(string user)
        {
            return await _dataContext.accesos
                .Include(u => u.User)
                .Where(c => c.User.UserName == user)
                .FirstOrDefaultAsync();
        }
    }
}
