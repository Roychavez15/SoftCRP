using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class SiniestrosController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IVehiculoProvGpsRepository _vehiculoProvGpsRepository;
        private readonly IVehiculoGpsRepository _vehiculoGpsRepository;
        private readonly ILogger<SiniestrosController> _logger;
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IDatosRepository _datosRepository;
        public SiniestrosController(
        IUserHelper userHelper,
        IVehiculoProvGpsRepository vehiculoProvGpsRepository,
        IVehiculoGpsRepository vehiculoGpsRepository,
        ILogRepository logRepository,
        IDatosRepository datosRepository,
        ILogger<SiniestrosController> logger,
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
            SiniestrosDataViewModel modelView = new SiniestrosDataViewModel();
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
                        var cuantos = await _datosRepository.GetSiniestrosAsync(user.Cedula, "");
                        modelView.SiniestrosViewModel = getCuantos(cuantos);
                        modelView.SiniestrosDetalleViewModel = await _datosRepository.GetSiniestrosDetalleAsync(user.Cedula, "");
                    }
                    else if (this.User.IsInRole("Admin") || this.User.IsInRole("Renting"))
                    {
                        model.Clientes = _combosHelper.GetComboClientes();
                        model.Anios = _combosHelper.GetComboAnio();
                        model.Meses = _combosHelper.GetComboMes();
                        var cuantos = await _datosRepository.GetSiniestrosAsync("", "");
                        modelView.SiniestrosViewModel = getCuantos(cuantos);
                        modelView.SiniestrosDetalleViewModel = await _datosRepository.GetSiniestrosDetalleAsync("", "");
                    }
                    modelView.DashBoardV2ViewModel = model;
                }
            }
            return View(modelView);
        }
        public SiniestrosViewModel getCuantos(IEnumerable<SiniestrosViewModel> data)
        {
            SiniestrosViewModel cuantos = new SiniestrosViewModel();
            foreach (var dato in data)
            {
                cuantos.Total_Siniestros += dato.Total_Siniestros;
                cuantos.Eventos_Siniestros += dato.Eventos_Siniestros;
                cuantos.Eventos_Siniestros1 += dato.Eventos_Siniestros1;
            }
            return cuantos;
        }
        public async Task<IActionResult> GetEstadisticasV2(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                UserId = "";
            }

            var resultado = await _datosRepository.GetSiniestrosDetalleAsync(UserId, "");
            return PartialView("_ResumenPartialView", resultado);
        }

        public async Task<IActionResult> GetEstadisticasV2all()
        {
            var resultado = await _datosRepository.GetSiniestrosDetalleAsync("", "");
            return PartialView("_ResumenPartialView", resultado);
        }


        public async Task<IActionResult> getDashboard(string UserId)
        {
            var cuantos = await _datosRepository.GetSiniestrosAsync(UserId, "");
            var result = getCuantos(cuantos);
            return PartialView("_DashboardSiniPartialView", result);
        }

        public async Task<IActionResult> getDashboardDate(string UserId, string mes, string anio)
        {
            var cuantos = await _datosRepository.GetSiniestrosAsync(UserId, "");
            if (mes != null)
            {
                cuantos = cuantos.Where<SiniestrosViewModel>(u => u.mes == getMes(mes));
            }
            if (anio != null)
            {
                cuantos = cuantos.Where<SiniestrosViewModel>(u => u.anio == int.Parse(anio));
            }
            var result = getCuantos(cuantos);
            return PartialView("_DashboardSiniPartialView", result);
        }
        public int getMes(string mes)
        {
            int messalida = 0;

            if (mes.Equals("Enero"))
            {
                messalida = 1;
            }
            else if (mes.Equals("Febrero"))
            {
                messalida = 2;
            }
            else if (mes.Equals("Febrero"))
            {
                messalida = 3;
            }
            else if (mes.Equals("Febrero"))
            {
                messalida = 4;
            }
            else if (mes.Equals("Febrero"))
            {
                messalida = 5;
            }
            else if (mes.Equals("Febrero"))
            {
                messalida = 6;
            }
            else if (mes.Equals("Febrero"))
            {
                messalida = 7;
            }
            else if (mes.Equals("Febrero"))
            {
                messalida = 8;
            }
            else if (mes.Equals("Febrero"))
            {
                messalida = 9;
            }
            else if (mes.Equals("Febrero"))
            {
                messalida = 10;
            }
            else if (mes.Equals("Febrero"))
            {
                messalida = 11;
            }
            else if (mes.Equals("Febrero"))
            {
                messalida = 12;
            }
            return messalida;
        }
    }
}
