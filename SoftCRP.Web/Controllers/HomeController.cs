using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;

namespace SoftCRP.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IUserHelper _userHelper;
        private readonly IAnalisisRepository _analisisRepository;
        private readonly INovedadesRepository _novedadesRepository;
        private readonly ITramitesRepository _tramitesRepository;
        private readonly ICapacitacionesRepository _capacitacionesRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IDatosRepository _datosRepository;
        private readonly IVehiculoProvGpsRepository _vehiculoProvGpsRepository;
        private readonly IVehiculoGpsRepository _vehiculoGpsRepository;
        private readonly ICombosHelper _combosHelper;

        public HomeController(
            IUserHelper userHelper,
            IAnalisisRepository analisisRepository,
            INovedadesRepository novedadesRepository,
            ITramitesRepository tramitesRepository,
            ICapacitacionesRepository capacitacionesRepository,
            ILogger<HomeController> logger,
            IDatosRepository datosRepository,
            IVehiculoProvGpsRepository vehiculoProvGpsRepository,
            IVehiculoGpsRepository vehiculoGpsRepository,
            ICombosHelper combosHelper)
        {
            _userHelper = userHelper;
            _analisisRepository = analisisRepository;
            _novedadesRepository = novedadesRepository;
            _tramitesRepository = tramitesRepository;
            _capacitacionesRepository = capacitacionesRepository;
            _logger = logger;
            _datosRepository = datosRepository;
            _vehiculoProvGpsRepository = vehiculoProvGpsRepository;
            _vehiculoGpsRepository = vehiculoGpsRepository;
            _combosHelper = combosHelper;
        }
        public async Task<IActionResult> Index()
        {
            //DashboardViewModel model = new DashboardViewModel();
            //if (!string.IsNullOrEmpty(this.User.Identity.Name))
            //{
            //    var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            //    if (user != null)
            //    {
            //        if (this.User.IsInRole("Cliente"))
            //        {
            //            model.Placas = await _combosHelper.GetComboPlacas(user.Cedula);
            //            model.Meses = _combosHelper.GetComboMes();
            //            model.Anios = _combosHelper.GetComboAnio();
            //            model.VehiculosClientesViewModel = await _datosRepository.GetVehiculosClienteAsync(user.Cedula);
            //            model.novedad = _novedadesRepository.GetNovedadAllNotSolution(user.Cedula);
            //            model.Tramite = _tramitesRepository.GetCountAllTramites(user.Cedula);
            //            model.Capacitaciones = _capacitacionesRepository.GetCountAllCapacitaciones();
            //            model.Analisis = _analisisRepository.GetCountAllAnalisis(user.Cedula);
            //            model.Transacciones = _tramitesRepository.GetCountAllTramites(user.Cedula) + _analisisRepository.GetCountAllAnalisis(user.Cedula);
            //            model.vehiculos = await _vehiculoProvGpsRepository.GetVehiculosAsync(user.Id);
            //            model.vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year,0);
            //            model.Conductores = await _datosRepository.GetConductoresAsync(user.Cedula, "");
            //            model.ingresosTalleres = await _datosRepository.GetIngresosTallerAsync(user.Cedula, "");
            //            model.siniestros = await _datosRepository.GetSiniestrosAsync(user.Cedula,"");
            //        }
            //        else if (this.User.IsInRole("Admin") || this.User.IsInRole("Renting"))
            //        {
            //            model.Clientes = _combosHelper.GetComboClientes();
            //            model.Meses = _combosHelper.GetComboMes();
            //            model.Anios = _combosHelper.GetComboAnio();

            //            var vehiculosTotal = await _datosRepository.GetDatosAutoAllAsync();
            //            model.TotalAutos = vehiculosTotal.Count();
            //            model.novedad = _novedadesRepository.GetNovedadAllNotSolution("");
            //            model.Tramite = _tramitesRepository.GetCountAllTramites("");
            //            model.Capacitaciones = _capacitacionesRepository.GetCountAllCapacitaciones();
            //            model.Analisis = _analisisRepository.GetCountAllAnalisis("");
            //            model.Transacciones = _tramitesRepository.GetCountAllTramites("") + _analisisRepository.GetCountAllAnalisis("");
            //            model.vehiculos = await _vehiculoProvGpsRepository.GetVehiculosAsync("");
            //            model.vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, 0);
            //            model.Conductores = await _datosRepository.GetConductoresAsync("", "");
            //            model.ingresosTalleres = await _datosRepository.GetIngresosTallerAsync("", "");
            //            model.siniestros = await _datosRepository.GetSiniestrosAsync("", "");
            //        }
            //    }
            //}
            DashBoardV2ViewModel model = new DashBoardV2ViewModel();
            if (!string.IsNullOrEmpty(this.User.Identity.Name))
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                if (user != null)
                {
                    if (this.User.IsInRole("Cliente"))
                    {
                        //model.Placas = await _combosHelper.GetComboPlacas(user.Id);
                        model.Placas = await _combosHelper.GetComboPlacasGPS(user.Cedula);
                        model.Meses = _combosHelper.GetComboMes();
                        model.Anios = _combosHelper.GetComboAnio();

                        var vehiculosTotal = await _datosRepository.GetVehiculosClienteAsync(user.Cedula);
                        //model.EstadisticasV1ViewModel.TotalAutos = vehiculosTotal.ToList().Count;

                        //model.EstadisticasV1ViewModel.VehiculosClientesViewModel = vehiculosTotal;
                        //model.EstadisticasV1ViewModel.novedad = _novedadesRepository.GetNovedadAllNotSolution(user.Cedula);
                        //model.EstadisticasV1ViewModel.Tramite = _tramitesRepository.GetCountAllTramites(user.Cedula);
                        //model.EstadisticasV1ViewModel.Capacitaciones = _capacitacionesRepository.GetCountAllCapacitaciones();
                        //model.EstadisticasV1ViewModel.Analisis = _analisisRepository.GetCountAllAnalisis(user.Cedula);
                        //model.EstadisticasV1ViewModel.Transacciones = _tramitesRepository.GetCountAllTramites(user.Cedula) + _analisisRepository.GetCountAllAnalisis(user.Cedula);
                        //model.EstadisticasV2ViewModel.vehiculos = await _vehiculoProvGpsRepository.GetVehiculosAsync(user.Id);
                        //model.EstadisticasV2ViewModel.vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, user.Cedula,"");
                        //model.EstadisticasV2ViewModel.Conductores = await _datosRepository.GetConductoresAsync(user.Cedula, "");
                        //model.EstadisticasV2ViewModel.ingresosTalleres = await _datosRepository.GetIngresosTallerAsync(user.Cedula, "");
                        //model.EstadisticasV2ViewModel.siniestros = await _datosRepository.GetSiniestrosAsync(user.Cedula, "");

                        EstadisticasV1ViewModel estadisticasV1ViewModel = new EstadisticasV1ViewModel
                        {
                            TotalAutos = vehiculosTotal.Count(),
                            novedad = _novedadesRepository.GetNovedadAllNotSolution(user.Cedula),
                            Tramite = _tramitesRepository.GetCountAllTramites(user.Cedula),
                            Capacitaciones = _capacitacionesRepository.GetCountAllCapacitaciones(),
                            Analisis = _analisisRepository.GetCountAllAnalisis(user.Cedula),
                            Transacciones = _tramitesRepository.GetCountAllTramites(user.Cedula) + _analisisRepository.GetCountAllAnalisis(user.Cedula),
                        };

                        EstadisticasV2ViewModel estadisticasV2ViewModel = new EstadisticasV2ViewModel
                        {
                            vehiculos = await _vehiculoProvGpsRepository.GetVehiculosAsync(user.Cedula),
                            vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, user.Cedula, ""),
                            //Conductores = await _datosRepository.GetConductoresAsync(user.Cedula, ""),
                            //ingresosTalleres = await _datosRepository.GetIngresosTallerAsync(user.Cedula, ""),
                            //siniestros = await _datosRepository.GetSiniestrosAsync(user.Cedula, ""),

                        };
                        model.EstadisticasV1ViewModel = estadisticasV1ViewModel;
                        model.EstadisticasV2ViewModel = estadisticasV2ViewModel;
                    }
                    else if (this.User.IsInRole("Admin") || this.User.IsInRole("Renting"))
                    {

                        model.Clientes = _combosHelper.GetComboClientes();
                        await GetPlacas("0");
                        model.Meses = _combosHelper.GetComboMes();
                        model.Anios = _combosHelper.GetComboAnio();

                        var cuantos = await _datosRepository.GetDiasSustitutosAsync("", "");

                        var vehiculosTotal = await _datosRepository.GetDatosAutoAllAsync();

                        

                        EstadisticasV1ViewModel estadisticasV1ViewModel = new EstadisticasV1ViewModel
                        {
                            TotalAutos = vehiculosTotal.Count(),
                            novedad = _novedadesRepository.GetNovedadAllNotSolution(""),
                            Tramite = _tramitesRepository.GetCountAllTramites(""),
                            Capacitaciones = _capacitacionesRepository.GetCountAllCapacitaciones(),
                            Analisis = _analisisRepository.GetCountAllAnalisis(""),
                            Transacciones = _tramitesRepository.GetCountAllTramites("") + _analisisRepository.GetCountAllAnalisis(""),
                        };

                        EstadisticasV2ViewModel estadisticasV2ViewModel = new EstadisticasV2ViewModel
                        {
                            vehiculos = await _vehiculoProvGpsRepository.GetVehiculosAsync(""),
                            vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, "", ""),
                            //Conductores = await _datosRepository.GetConductoresAsync("", ""),
                            //ingresosTalleres = await _datosRepository.GetIngresosTallerAsync("", ""),
                            //siniestros = await _datosRepository.GetSiniestrosAsync("", ""),

                        };
                        model.EstadisticasV1ViewModel = estadisticasV1ViewModel;
                        model.EstadisticasV2ViewModel = estadisticasV2ViewModel;

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

        public async Task<JsonResult> GetPlacas(string UserId)
        {
            //Country country = _context.Countries
            //    .Include(c => c.Departments)
            //    .FirstOrDefault(c => c.Id == countryId);


            //if (country == null)
            //{
            //    return null;
            //}

            //return Json(country.Departments.OrderBy(d => d.Name));
            if (string.IsNullOrEmpty(UserId))
            {
                return null;
            }

            //var placas = await _datosRepository.GetPlacasClienteAsync(UserId);
            var placas = await _combosHelper.GetComboPlacasGPS(UserId);
            if (placas==null)
            {
                return null;
            }
            return Json(placas.ToList().OrderBy(d => d.Text));
        }

        public async Task<IActionResult> GetEstadisticasV2(string UserId)
        {
            if(string.IsNullOrEmpty(UserId))
            {
                UserId = "";
            }

            var resumen = await _datosRepository.GetResumePlacasAsync(UserId, "");

            EstadisticasV2ViewModel estadisticasV2ViewModel = new EstadisticasV2ViewModel
            {
                vehiculos = await _vehiculoProvGpsRepository.GetVehiculosAsync(UserId),
                vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, UserId,""),
                //Conductores = await _datosRepository.GetConductoresAsync(UserId, ""),
                //ingresosTalleres = await _datosRepository.GetIngresosTallerAsync(UserId, ""),
                //siniestros = await _datosRepository.GetSiniestrosAsync(UserId, ""),

            };
            return PartialView("_EstadisticasV2PartialView", estadisticasV2ViewModel);
        }
        public async Task<IActionResult> GetEstadisticasV2Placa(string UserId, string Placa)
        {
            if (string.IsNullOrEmpty(Placa) || Placa=="0")
            {
                Placa = "";
            }
            var resumen = await _datosRepository.GetResumePlacasAsync(UserId, Placa);

            EstadisticasV2ViewModel estadisticasV2ViewModel = new EstadisticasV2ViewModel
            {
                vehiculos = await _vehiculoProvGpsRepository.GetVehiculosAsync(UserId),
                vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, UserId, Placa),
                //Conductores = await _datosRepository.GetConductoresAsync(UserId, Placa),
                //ingresosTalleres = await _datosRepository.GetIngresosTallerAsync(UserId, Placa),
                //siniestros = await _datosRepository.GetSiniestrosAsync(UserId, Placa),

            };
            return PartialView("_EstadisticasV2PartialView", estadisticasV2ViewModel);
        }
    }
}



