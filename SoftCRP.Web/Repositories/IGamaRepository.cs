using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface IGamaRepository : IGenericRepository<Gama>
    {
        Task<Gama> GetGamaByTypeAsync(string gama);
    }
}
