using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public class LogRepository : GenericRepository<Log>, ILogRepository
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly IHttpContextAccessor _accesor;

        public LogRepository(
            DataContext dataContext,
            IUserHelper userHelper,
            IHttpContextAccessor accesor) : base(dataContext)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            _accesor = accesor;
        }

        public async Task SaveLogs(string Level, string Message, string Module, string user)
        {
            var usuario = await _userHelper.GetUserAsync(user);
            Log log = new Log
            {
                Fecha=DateTime.Now,
                Level=Level,
                Message=Message,
                Module=Module,
                IP = _accesor.HttpContext.Connection.RemoteIpAddress.ToString(),
                user = usuario
            };

            //await this.CreateAsync(log);
            
            await _dataContext.Logs.AddAsync(log);
            await _dataContext.SaveChangesAsync();
        }
        public async Task SaveLogsGPS(string Level, string Message, string Module, string user)
        {
            var usuario = await _userHelper.GetUserAsync(user);
            Log log = new Log
            {
                Fecha = DateTime.Now,
                Level = Level,
                Message = Message,
                Module = Module,
                IP = "",
                user = usuario
            };

            //await this.CreateAsync(log);

            await _dataContext.Logs.AddAsync(log);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Log>> GetLogsReportesAsync(DateTime Inicio, DateTime Fin)
        {

                return await _dataContext.Logs
                    .Include(u => u.user)
                    .Where(f => f.Fecha >= Inicio && f.Fecha <= Fin.AddDays(1)).ToListAsync();
            
        }
    }
}
