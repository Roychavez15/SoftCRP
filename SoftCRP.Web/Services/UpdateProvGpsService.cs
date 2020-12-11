using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftCRP.Web.Data;
using SoftCRP.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoftCRP.Web.Services
{
    public class UpdateProvGpsService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHostingEnvironment _environment;

        private readonly string fileName = "File 1.txt";
        private Timer timer;
        public UpdateProvGpsService(
            IServiceScopeFactory serviceScopeFactory,
            IHostingEnvironment environment
            )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _environment = environment;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {

            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IDatosRepository>();
                try
                {
                    await context.GetDatosAutoAllGpsAsync();
                }
                catch { }
                
            }
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
