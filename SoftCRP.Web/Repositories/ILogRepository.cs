using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface ILogRepository : IGenericRepository<Log>
    {
        Task SaveLogsGPS(string Level, string Message, string Module, string user);
        Task SaveLogs(string Level, string Message, string Module, string user);
        Task<IEnumerable<Log>> GetLogsReportesAsync(DateTime Inicio, DateTime Fin);
    }
}
