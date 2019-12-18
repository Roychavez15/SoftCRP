using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface IAnalisisRepository : IGenericRepository<Analisis>
    {
        Task<AnalisisViewModel> GetAnalisis(string cedula);
        Task<Analisis> GetAnalisisByIdAsync(int? id);

        int GetCountAllAnalisis(string nit);
        Task<IEnumerable<Analisis>> GetAnalisisReportesAsync(DateTime Inicio, DateTime Fin, string filter);
    }
}
