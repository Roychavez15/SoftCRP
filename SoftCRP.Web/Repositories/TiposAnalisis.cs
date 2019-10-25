using Microsoft.AspNetCore.Identity;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public class TiposAnalisis : GenericRepository<TipoAnalisis>, ITiposAnalisis
    {
        private readonly DataContext _dataContext;

        public TiposAnalisis(
            DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<TipoAnalisis> GetTiposAnalisis()
        {
            return _dataContext.TiposAnalisis;
            //throw new NotImplementedException();
        }

        public async Task<IdentityResult> AddTipoAnalisisAsync(TipoAnalisis tipo)
        {
            //return await _dataContext.TiposAnalisis.AddAsync(tipo);
            throw new NotImplementedException();
        }

    }
}
