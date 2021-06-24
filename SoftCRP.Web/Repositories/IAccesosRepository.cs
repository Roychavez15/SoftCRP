using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface IAccesosRepository : IGenericRepository<Acceso>
    {
        Task<int> CreateAcceso(User user);
        Task<Acceso> GetAcceso(string user);
        Task<int> Actualiza(IEnumerable<Acceso> accesos);
        Task<IEnumerable<Acceso>> GetListAccesosAsync();
    }
}
