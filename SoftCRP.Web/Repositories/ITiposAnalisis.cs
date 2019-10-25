using Microsoft.AspNetCore.Identity;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface ITiposAnalisis:IGenericRepository<TipoAnalisis>
    {
        Task<IdentityResult> AddTipoAnalisisAsync(TipoAnalisis tipo);
        IEnumerable<TipoAnalisis> GetTiposAnalisis();
    }
}
