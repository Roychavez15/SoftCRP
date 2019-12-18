using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;

namespace SoftCRP.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IAnalisisRepository _analisisRepository;
        private readonly INovedadesRepository _novedadesRepository;
        private readonly ITramitesRepository _tramitesRepository;
        private readonly ICapacitacionesRepository _capacitacionesRepository;
        private readonly IDatosRepository _datosRepository;

        public HomeController(
            IUserHelper userHelper,
            IAnalisisRepository analisisRepository,
            INovedadesRepository novedadesRepository,
            ITramitesRepository tramitesRepository,
            ICapacitacionesRepository capacitacionesRepository,
            IDatosRepository datosRepository)
        {
            _userHelper = userHelper;
            _analisisRepository = analisisRepository;
            _novedadesRepository = novedadesRepository;
            _tramitesRepository = tramitesRepository;
            _capacitacionesRepository = capacitacionesRepository;
            _datosRepository = datosRepository;
        }
        public async Task<IActionResult> Index()
        {

            //List<VehiculosClientesViewModel> Vehiculos = new List<VehiculosClientesViewModel>();

            //if (!string.IsNullOrEmpty(this.User.Identity.Name))
            //{
            //    var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            //    if (user != null)
            //    {                    
            //        if (this.User.IsInRole("Cliente"))
            //        {
            //            Vehiculos = await _datosRepository.GetVehiculosClienteAsync(user.Cedula);
            //        }
            //    }
            //}
            DashboardViewModel model = new DashboardViewModel();
            if (!string.IsNullOrEmpty(this.User.Identity.Name))
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                if (user != null)
                {
                    if (this.User.IsInRole("Cliente"))
                    {
                        //Vehiculos = await _datosRepository.GetVehiculosClienteAsync(user.Cedula);
                        model.VehiculosClientesViewModel = await _datosRepository.GetVehiculosClienteAsync(user.Cedula);
                        model.novedad = _novedadesRepository.GetNovedadAllNotSolution(user.Cedula);
                        model.Tramite = _tramitesRepository.GetCountAllTramites(user.Cedula);
                        model.Capacitaciones = _capacitacionesRepository.GetCountAllCapacitaciones();
                        model.Analisis = _analisisRepository.GetCountAllAnalisis(user.Cedula);
                        model.Transacciones = _tramitesRepository.GetCountAllTramites(user.Cedula) + _analisisRepository.GetCountAllAnalisis(user.Cedula);
                    }
                }
            }

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }
    }
}
