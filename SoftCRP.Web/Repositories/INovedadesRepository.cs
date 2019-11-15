using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface INovedadesRepository : IGenericRepository<Novedad>
    {
        Task<NovedadesViewModel> GetNovedadAsync(string cedula);
        Task<Novedad> GetNovedadByIdAsync(int? id);

        int GetNovedadAllNotSolution(string nit);

        Task<IEnumerable<Novedad>> GetNovedadReportesAsync(DateTime Inicio, DateTime Fin);
    }
}
