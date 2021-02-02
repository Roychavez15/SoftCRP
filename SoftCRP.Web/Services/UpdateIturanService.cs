using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoftCRP.Web.Services
{
    public class UpdateIturanService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHostingEnvironment _environment;
        //private readonly ILogRepository _logRepository;
        private Timer timer;
        
        public UpdateIturanService(
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

            //timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(12));
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(15));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IIturanRepository>();
                try
                {

                    //var resultados = await context.getDataCarsInfo();
                    //Thread.Sleep(60000);
                    //var deviceInfo = await context.getDataDeviceInfo();
                    //Thread.Sleep(60000);
                    IEnumerable<TripsIturanViewModel> viajes = await context.getTrips();
                    //Thread.Sleep(600000);
                    var ultimoregistro = viajes.OrderBy(t=>t.TID).LastOrDefault();
                    await context.SaveLastRecord(ultimoregistro.TID.ToString());

                    var vehiculos = await context.GetDataVehiculos();

                    foreach (var item in vehiculos)
                    {
                        //await context.getDataTrips(item, clientId, token);
                        var datos = viajes.Where(p => p.PN == item.Placa).OrderBy(f=>f.T1).GroupBy(g=>  new { g.T1.Year, g.T1.Month, g.T1.Day});
                        if(datos!=null)
                        {
                            foreach(var item1 in datos)
                            {
                                var trips = item1.Count();
                                var anio = item1.Key.Year;
                                var mes = item1.Key.Month;
                                var dia = item1.Key.Day;
                                var excesovelocidad = item1.Sum(v => Convert.ToInt32(v.OSO));
                                var excesoaceleracion= item1.Sum(v => Convert.ToInt32(v.ACO));
                                var excesoaceleracioncurva = item1.Sum(v => Convert.ToInt32(v.CAO));
                                var frenadobrusco = item1.Sum(v => Convert.ToInt32(v.DCO));
                                var kilometros= item1.Sum(v => Convert.ToDecimal(v.O2.Replace(".",",")) - Convert.ToDecimal(v.O1.Replace(".", ",")));
                                var longitud = item1.LastOrDefault().X2;
                                var latitud = item1.LastOrDefault().Y2;
                                //foreach (var item2 in item1)
                                //{
                                //    item2.su
                                //}
                                IturanDatosGps ituranDatosGps = new IturanDatosGps
                                {
                                    placa=item.Placa,
                                    viajes= trips,
                                    kilometros=kilometros,
                                    speeding=excesovelocidad,
                                    hardbraking= frenadobrusco,
                                    sharpacceleration=excesoaceleracion,
                                    sharpturn = excesoaceleracioncurva,
                                    latitud=latitud,
                                    longitud= longitud,
                                   
                                };
                                await context.getDataTrips(item, "", ituranDatosGps, dia, mes, anio);
                            }

                        }
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
