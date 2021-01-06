using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftCRP.Web.Data;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Controllers
{
    public class MantenimientoController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IVehiculoProvGpsRepository _vehiculoProvGpsRepository;
        private readonly IVehiculoGpsRepository _vehiculoGpsRepository;
        private readonly ILogger<MantenimientoController> _logger;
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IDatosRepository _datosRepository;
        public MantenimientoController(
            IUserHelper userHelper,
            IVehiculoProvGpsRepository vehiculoProvGpsRepository,
            IVehiculoGpsRepository vehiculoGpsRepository,
            ILogRepository logRepository,
            IDatosRepository datosRepository,
            ILogger<MantenimientoController> logger,
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
            MantenimientoViewModel modelView = new MantenimientoViewModel();
            DashBoardV2ViewModel model = new DashBoardV2ViewModel();
            modelView.DashboardMantViewModel = new DashboardMantViewModel();

            if (!string.IsNullOrEmpty(this.User.Identity.Name))
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);

                if (user != null)
                {
                    model.Anios = _combosHelper.GetComboAnio();
                    model.Meses = _combosHelper.GetComboMes();
                    if (this.User.IsInRole("Cliente"))
                    {
                        modelView.ResumenPlacasViewModel = await _datosRepository.GetResumePlacasAsync(user.Cedula, "");
                        var cuantos = await _datosRepository.GetMantenimientoEstadoCuantos(user.Cedula);
                        modelView.DashboardMantViewModel = getCuantos(cuantos);
                    }
                    else if (this.User.IsInRole("Admin") || this.User.IsInRole("Renting"))
                    {
                        model.Clientes = _combosHelper.GetComboClientes();
                        model.Anios = _combosHelper.GetComboAnio();
                        model.Meses = _combosHelper.GetComboMes();
                        modelView.ResumenPlacasViewModel = await _datosRepository.GetResumePlacasAsync(user.Cedula, "");
                        var cuantos = await _datosRepository.GetMantenimientoEstadoCuantos("");
                        modelView.DashboardMantViewModel = getCuantos(cuantos);
                    }
                }
            }
            modelView.DashBoardV2ViewModel = model;
            return View(modelView);
        }
        public DashboardMantViewModel getCuantos(IEnumerable<MantEstadosCuantosViewModel> cuantos)
        {
            DashboardMantViewModel dashboardMant = new DashboardMantViewModel();
            foreach (var dato in cuantos)
            {
                switch (dato.estado)
                {
                    case "VEHICULO FUERA DE TALLER":
                        dashboardMant.vehiculo_fuera_taller += 1;
                        break;
                    case "CITA CONFIRMADA":
                        dashboardMant.cita_confirmada += 1;
                        break;
                    case "VEHICULO INGRESADO":
                        dashboardMant.vehiculo_ingresado += 1;
                        break;
                    case "REGISTRADO":
                        dashboardMant.vehiculo_registrado_ingreso += 1;
                        break;
                }
            }
            return dashboardMant;
        }
        public async Task<IActionResult> GetEstadisticasV2(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                UserId = "";
            }

            var resultado = await _datosRepository.GetResumePlacasAsync(UserId, "");
            return PartialView("_ResumenPartialView", resultado);
        }

        public async Task<IActionResult> GetEstadisticasV2all()
        {
            var resultado = await _datosRepository.GetResumePlacasAsync("", "");
            return PartialView("_ResumenPartialView", resultado);
        }


        public async Task<IActionResult> getDashboard(string UserId)
        {
            DashboardMantViewModel dashboardMant = new DashboardMantViewModel();
            var cuantos = await _datosRepository.GetMantenimientoEstadoCuantos(UserId);
            var result = getCuantos(cuantos);
            return PartialView("_DashboardMantPartialView", result);
        }


        public async Task<IActionResult> getDashboardAll()
        {
            DashboardMantViewModel dashboardMant = new DashboardMantViewModel();
            var cuantos = await _datosRepository.GetMantenimientoEstadoCuantos("");
            var result = getCuantos(cuantos);
            return PartialView("_DashboardMantPartialView", result);
        }
    }
}
