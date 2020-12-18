using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftCRP.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoftCRP.Web.Services
{
    public class UpdateCGBService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHostingEnvironment _environment;
        //private readonly ILogRepository _logRepository;
        private Timer timer;
        public UpdateCGBService(
            IServiceScopeFactory serviceScopeFactory,
            IHostingEnvironment environment
            //ILogRepository logRepository
            )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _environment = environment;
            //_logRepository = logRepository;
        }
        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(12));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IDatosRepository>();
                try
                {
                    //await _logRepository.SaveLogsGPS("Success", "Enviando Proceso CGB", "CGB", "");
                    await context.ProcesoCGB();
                }
                catch(Exception ex)
                { 
                    //await _logRepository.SaveLogsGPS("Error", ex.Message, "CGB", "");  
                }
                
            }
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {

            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
