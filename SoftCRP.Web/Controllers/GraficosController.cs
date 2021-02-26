using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Controllers
{
    public class GraficosController : BaseController
    {
        private readonly IUserHelper _userHelper;
        private readonly ILogger<GraficosController> _logger;
        private readonly IDatosRepository _datosRepository;
        private readonly IVehiculoProvGpsRepository _vehiculoProvGpsRepository;
        private readonly IVehiculoGpsRepository _vehiculoGpsRepository;
        private readonly ICombosHelper _combosHelper;

        public GraficosController(
            IUserHelper userHelper,
            ILogger<GraficosController> logger,
            IDatosRepository datosRepository,
            IVehiculoProvGpsRepository vehiculoProvGpsRepository,
            IVehiculoGpsRepository vehiculoGpsRepository,
            ICombosHelper combosHelper)
        {
            _userHelper = userHelper;
            _logger = logger;
            _datosRepository = datosRepository;
            _vehiculoProvGpsRepository = vehiculoProvGpsRepository;
            _vehiculoGpsRepository = vehiculoGpsRepository;
            _combosHelper = combosHelper;
        }
        public async Task<IActionResult> Index()
        {


            GraficosViewModel graficosViewModel = new GraficosViewModel();

            List<VehiculoGps> vehiculosGps = new List<VehiculoGps>();

            if (!string.IsNullOrEmpty(this.User.Identity.Name))
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                if (user != null)
                {
                    if (this.User.IsInRole("Cliente"))
                    {
                        vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, user.Cedula, "").ToList();
                    }
                    else if(this.User.IsInRole("Admin") || this.User.IsInRole("Renting"))
                    {
                        vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, "", "").ToList();
                    }
                }
            }

            string markers = "[";

            foreach (var gps in vehiculosGps)
            {
                if(!string.IsNullOrEmpty(gps.latitude) && !string.IsNullOrEmpty(gps.longitude))
                {
                    markers += "{";
                    markers += string.Format("'title': '{0}',", gps.vehiculo.Placa);
                    markers += string.Format("'lat': '{0}',", gps.latitude);
                    markers += string.Format("'lng': '{0}',", gps.longitude);
                    markers += string.Format("'description': '{0}'", gps.vehiculo.user.FullName+"-"+ gps.vehiculo.Placa);
                    markers += "},";
                }

            }
            markers += "];";
            ViewBag.Markers = markers;

            graficosViewModel.Clientes = _combosHelper.GetComboClientes();
            graficosViewModel.Markers = markers;


            return View(graficosViewModel);
        }

        public async Task<JsonResult> GetPlacas(string UserId)
        {

            //if (string.IsNullOrEmpty(Anio) && string.IsNullOrEmpty(Mes))
            //{
            //    return null;
            //}

            if (string.IsNullOrEmpty(UserId))
            {
                return null;
            }

            //var placas = await _datosRepository.GetPlacasClienteAsync(UserId);
            var placas = await _combosHelper.GetComboPlacasGPS(UserId);
            if (placas == null)
            {
                return null;
            }
            return Json(placas.ToList().OrderBy(d => d.Text));
        }

        public async Task<JsonResult> GetMapaCliente(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                UserId = "";
            }

            GraficosViewModel graficosViewModel = new GraficosViewModel();

            var vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, UserId, "");

            return Json(vehiculosGps.ToList());
        }
        public async Task<JsonResult> GetMapaAll(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                UserId = "";
            }

            GraficosViewModel graficosViewModel = new GraficosViewModel();

            var vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, "", "");

            return Json(vehiculosGps.ToList());
        }
        public async Task<JsonResult> GetMapaClientePlaca(string UserId, string Placa)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                UserId = "";
            }
            if (string.IsNullOrEmpty(Placa))
            {
                Placa = "";
            }

            GraficosViewModel graficosViewModel = new GraficosViewModel();

            var vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, UserId, Placa);

            return Json(vehiculosGps.ToList());
        }
        public async Task<JsonResult> GetMapaClientePlacaAll(string UserId, string Placa)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                UserId = "";
            }
            if (string.IsNullOrEmpty(Placa))
            {
                Placa = "";
            }

            GraficosViewModel graficosViewModel = new GraficosViewModel();

            var vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, UserId, Placa);

            return Json(vehiculosGps.ToList());
        }
    }
}
