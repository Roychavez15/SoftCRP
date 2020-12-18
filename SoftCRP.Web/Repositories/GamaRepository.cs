using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public class GamaRepository : GenericRepository<Gama>, IGamaRepository
    {
        private readonly DataContext _dataContext;

        public GamaRepository(
            DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Gama> GetGamaByTypeAsync(string gama)
        {
            return await _dataContext.gamas
                .Where(g => g.GamaSustituto.ToUpper() == gama.ToUpper())
                .FirstOrDefaultAsync();
        }
    }
}
