using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
            ICombosHelper combosHelper
            )
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
            return View();
        }
        public async Task<IActionResult> Index1()
        {
            return View();
        }
        public async Task<IActionResult> Index2()
        {
            return View();
        }
        public JsonResult GetSubProcess(string proceso)
        {

            var sub = _combosHelper.GetComboSubProcesos(proceso);
            return Json(sub.ToList());
        }
        public JsonResult GetClientes(string proceso)
        {

            var sub = _combosHelper.GetComboClientes();
            return Json(sub.ToList());
        }
        public async Task<JsonResult> GetPlacas(string nit)
        {

            var sub = await _combosHelper.GetComboPlacasSN(nit);
            return Json(sub.ToList());
        }

        [HttpPost]
        public async Task<JsonResult> Chart(GrafViewModel model)
        {
            var proceso = model.Proceso;
            var subproceso = model.SubProceso;
            var cliente = model.Cliente;
            var placa = model.Placa;
            var fecha = model.Fecha;
            var usuario = model.Usuario;

            if (model.Cliente=="null" || string.IsNullOrEmpty(model.Cliente) || model.Cliente=="0")
            {
                cliente = "";
            }
            if (model.Proceso == "null")
            {
                proceso = "";
            }
            if (model.SubProceso == "null")
            {
                subproceso = "";
            }
            if (model.Placa == "null" || string.IsNullOrEmpty(model.Placa))
            {
                placa = "";
            }
            if (model.Fecha == "null")
            {
                fecha = "";
            }
            if (model.Usuario == "null")
            {
                usuario = "";
            }
            List<object> iData = new List<object>();
            


            if (!string.IsNullOrEmpty(this.User.Identity.Name))
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                if (user != null)
                {
                    if (proceso == "Conducción")
                    {
                        List<VehiculoGps> vehiculosGps = new List<VehiculoGps>();
                        if (this.User.IsInRole("Cliente"))
                        {

                            vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, user.Cedula, placa).ToList();

                        }
                        else if (this.User.IsInRole("Admin") || this.User.IsInRole("Renting"))
                        {

                            vehiculosGps = _vehiculoGpsRepository.GetVehiculosGPSAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, cliente, placa).ToList();

                        }
                        return Json(vehiculosGps);
                    }
                    else if(proceso=="Mantenimientos")
                    {
                        
                        IEnumerable<ResumenPlacasViewModel> mantenimiento = new List<ResumenPlacasViewModel>();
                        if (this.User.IsInRole("Cliente"))
                        {

                            mantenimiento = await _datosRepository.GetResumePlacasAsync(user.Cedula, placa);

                        }
                        else if (this.User.IsInRole("Admin") || this.User.IsInRole("Renting"))
                        {

                            mantenimiento = await _datosRepository.GetResumePlacasAsync(cliente, placa);

                        }
                        return Json(mantenimiento.ToList());
                    }
                    else if (proceso == "Sustitutos")
                    {

                    }
                    else if (proceso == "Siniestros")
                    {

                    }
                }
            }


            //ViewBag.ChartData = vehiculosGps;
            //Source data returned as JSON    
            return Json(null);
        }
    }
}
