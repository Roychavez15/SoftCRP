using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoftCRP.Web.Services
{
    public class AlarmaTramites : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHostingEnvironment _environment;

        private Timer timer;
        public AlarmaTramites(
            IServiceScopeFactory serviceScopeFactory,
            IHostingEnvironment environment)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _environment = environment;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //TODO: CAMBIAR EN PRODUCCION A MAS TIEMPO
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(10)); 

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ITramitesRepository>();
                try
                {
                    var tramites = await context.GetTramitesAlarmaAsync();

                    foreach(var tr in tramites)
                    {
                        var context1 = scope.ServiceProvider.GetRequiredService<IUserHelper>();
                        var context2 = scope.ServiceProvider.GetRequiredService<IMailHelper>();

                        var datos = await context1.GetUserByCedulaAsync(tr.Cedula);
                        var user = await context1.GetUserByIdAsync(tr.user.Id);

                        var emails = user.Email + ',' + datos.Email;
                        //TODO: cambiar direccion de correo
                        context2.SendMailAlarma(emails, "Plataforma Clientes",
                            $"<html xmlns='http://www.w3.org/1999/xhtml'>" +
                            $"<head>" +
                            $"<meta http-equiv=" + "Content-Type" + " content=" + "text/html; charset = UTF-8" + " />" +
                            $"<title>" +
                            $"</title>" +
                            $"</head>" +
                            $"<body>" +
                            //$"<h1>Plataforma Clientes Nuevo Trámite</h1>" +
                            $"<hr width=100% align='center' size=30 color='#002D73' style='margin:0px;padding:0px'>" +
                            $"<hr width=100% align='center' size=5 color='#F2AE0B' style='margin:0px;padding:0px'>" +
                            $"<br><br>" +
                            $"<p>Estimado Usuario/a" +
                            $"<p>Al Documento:" + tr.Placa + ", del cliente " + datos.FullName +
                            $"<p>, le quedan 7 días para caducar,"+
                            $"<p>Si corresponde una renovación del documento, por favor gestionarla"+
                            $"<br><br>" +
                            $"<p>Para poder revisar la información de su plataforma ingrese a su cuenta con su usuario y contraseña." +
                            $"<div align='center'><a href='https://clientes.rentingpichincha.com'><img src='https://clientes.rentingpichincha.com/images/email1.png' align='center'></a></div>" +
                            $"<br><br>" +
                            $"<p>Agradecemos su esfuerzo y puntualidad.<br>" +
                            $"<p>Saludos cordiales<br>" +
                            $"<br><br>" +
                            $"<p>Consorcio Pichincha S.A CONDELPI<br>" +
                            $"<p>Av.González Suárez N32-346 y Coruña<br>" +
                            $"<p><img src='https://clientes.rentingpichincha.com/images/call.png' width=30px>Call Center: 1-800 RENTING(736846)<br>" +
                            $"<p><img src='https://clientes.rentingpichincha.com/images/email.png' width=25px>E-Mail: inforenting@condelpi.com<br>" +
                            $"<p><img src='https://clientes.rentingpichincha.com/images/whatsapp.jpg' width=25px>WhatsApp: 0997652137" +
                            $"<p>Quito-Ecuador" +
                            $"<br><br>" +
                            $"<img src='https://clientes.rentingpichincha.com/images/email2.png' width=200px>" +
                            $"<hr width=100% align='center' size=30 color='#002D73' style='margin:0px;padding:0px'>" +
                            $"<hr width=100% align='center' size=5 color='#F2AE0B' style='margin:0px;padding:0px'></body></html>"
                            , tr.archivoTramites.ToList());


                        tr.Estado = "AVISADO";
                        await context.UpdateAsync(tr);
                    }

                    
                }
                catch(Exception ex) 
                {

                }

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
