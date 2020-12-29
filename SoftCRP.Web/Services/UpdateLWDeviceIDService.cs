using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftCRP.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoftCRP.Web.Services
{
    public class UpdateLWDeviceIDService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Timer timer;
        public UpdateLWDeviceIDService(
            IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
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
                var context = scope.ServiceProvider.GetRequiredService<ILocationWorldRepository>();
                try
                {
                    //await _logRepository.SaveLogsGPS("Success", "Enviando Proceso CGB", "CGB", "");
                    var token = context.getToken();
                    var clientId = context.getClientId(token);
                    var dispositivos= context.getDataDevices(token);
                    //var resp = (JObject)JsonConvert.DeserializeObject(dispositivos[1].ToString());
                    IList<JToken> results = dispositivos["items"].Children().ToList();

                    //actualiza Id proveedor GPS
                    foreach (JToken result in results)
                    {
                        var ID = result["id"].ToString();
                        var automotor = context.getAutomotors(ID, token).Result;

                        await context.UpdateIdAutomotor(ID, automotor["plate"].Value<string>());
                    }

                    ////procesar datos gps

                    //await context.GetDataVehiculos(token, clientId);
                    var vehiculos = await context.GetDataVehiculos();

                    foreach (var item in vehiculos)
                    {
                        await context.getDataTrips(item, clientId, token);
                    }

                }
                catch (Exception ex)
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
