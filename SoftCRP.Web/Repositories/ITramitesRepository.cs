using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface ITramitesRepository : IGenericRepository<Tramite>
    {
        Task<IEnumerable<Tramite>> GetTramitesAlarmaAsync();
        Task<TramitesViewModel> GetTramiteAsync(string cedula);
        Task<Tramite> GetTramiteByIdAsync(int? id);
        int GetCountAllTramites(string nit);
        Task<IEnumerable<Tramite>> GetTramiteReportesAsync(DateTime Inicio, DateTime Fin, string filter, string Ciudad);
    }
}
