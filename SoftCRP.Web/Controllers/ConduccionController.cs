using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftCRP.Web.Data;
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
    public class ConduccionController : BaseController
    {
        private readonly IUserHelper _userHelper;
        private readonly IVehiculoProvGpsRepository _vehiculoProvGpsRepository;
        private readonly IVehiculoGpsRepository _vehiculoGpsRepository;
        private readonly ILogger<ConduccionController> _logger;
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IDatosRepository _datosRepository;
        public ConduccionController(
            IUserHelper userHelper,
            IVehiculoProvGpsRepository vehiculoProvGpsRepository,
            IVehiculoGpsRepository vehiculoGpsRepository,
            ILogRepository logRepository,
            IDatosRepository datosRepository,
            ILogger<ConduccionController> logger,
            DataContext context,
            ICombosHelper combosHelper)
        {
            _userHelper = userHelper;
            _logger = logger;
            _vehiculoProvGpsRepository = vehiculoProvGpsRepository;
            _vehiculoGpsRepository = vehiculoGpsRepository;
            _context = context;
            _datosRepository = datosRepository;
            _combosHelper = combosHelper;
        }
        public async Task<IActionResult> Index()
        {
            DashBoardV2ViewModel model = new DashBoardV2ViewModel();
            if (!string.IsNullOrEmpty(this.User.Identity.Name))
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                if (user != null)
                {
                    if (this.User.IsInRole("Cliente"))
                    {
                        model.Anios = _combosHelper.GetComboAnio();
                        model.Meses = _combosHelper.GetComboMes();
                        EstadisticasV2ViewModel estadisticasV2ViewModel = new EstadisticasV2ViewModel
                        {
                            vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, user.Cedula, ""),
                        };
                        model.EstadisticasV2ViewModel = estadisticasV2ViewModel;
                    }
                    else if (this.User.IsInRole("Admin") || this.User.IsInRole("Renting"))
                    {
                        model.Clientes = _combosHelper.GetComboClientes();
                        model.Anios = _combosHelper.GetComboAnio();
                        model.Meses = _combosHelper.GetComboMes();
                        EstadisticasV2ViewModel estadisticasV2ViewModel = new EstadisticasV2ViewModel
                        {
                            vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, "", ""),
                        };
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

        public async Task<IActionResult> GetEstadisticasV2(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                UserId = "";
            }
            var resumen = await _datosRepository.GetResumePlacasAsync(UserId, "");

            EstadisticasV2ViewModel estadisticasV2ViewModel = new EstadisticasV2ViewModel
            {
                vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, UserId, ""),
                //Conductores = await _datosRepository.GetConductoresAsync(UserId, ""),
                //ingresosTalleres = await _datosRepository.GetIngresosTallerAsync(UserId, ""),
                //siniestros = await _datosRepository.GetSiniestrosAsync(UserId, ""),

            };
            return PartialView("_EstadisticasV2PartialView", estadisticasV2ViewModel);
        }
        public async Task<IActionResult> GetEstadisticasV2all()
        {
            EstadisticasV2ViewModel estadisticasV2ViewModel = new EstadisticasV2ViewModel
            {
                vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, "", ""),

            };
            return PartialView("_EstadisticasV2PartialView", estadisticasV2ViewModel);
        }

    }
}
