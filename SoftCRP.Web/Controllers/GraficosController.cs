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
        public JsonResult GetUsuarios(string proceso)
        {
            if(proceso=="Conducción")
            {
                return Json(_combosHelper.UsuariosConduccion().ToList());
            }
            else if (proceso == "Mantenimientos")
            {
                return Json(_combosHelper.UsuariosConduccion().ToList());
            }
            else if (proceso == "Siniestros")
            {
                return Json(_combosHelper.UsuariosConduccion().ToList());
            }
            return null;
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
            var validez = model.Validez;

            string[] fechas = model.Fecha.Split('-');
            var fechainicio = Convert.ToDateTime(fechas[0]);
            var fechafin = Convert.ToDateTime(fechas[1]);

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
                        if (validez == "1")
                        {
                            //return Json(vehiculosGps.Where(f => Convert.ToDateTime(f.f) >= fechainicio && Convert.ToDateTime(f.Fecha_asignacion) <= fechafin).ToList());
                            vehiculosGps = vehiculosGps.Where(f => Convert.ToDateTime(f.anio + "-" + f.mes + "-" + f.dia) >= fechainicio && Convert.ToDateTime(f.anio + "-" + f.mes + "-" + f.dia) <= fechafin).ToList();
                        }
                        if(usuario!="" && usuario!="0")
                        {
                            vehiculosGps = vehiculosGps.Where(u => u.usuario.ToUpper() == usuario).ToList();
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
                        if (validez == "1")
                        {
                            mantenimiento = mantenimiento.Where(f => Convert.ToDateTime(f.fecha_mmto) >= fechainicio && Convert.ToDateTime(f.fecha_mmto) <= fechafin).ToList();
                            //return Json(mantenimiento.Where(f => Convert.ToDateTime(f.fecha_mmto) >= fechainicio && Convert.ToDateTime(f.fecha_mmto) <= fechafin).ToList());
                        }
                        if (usuario != "" && usuario != "0")
                        {
                            mantenimiento = mantenimiento.Where(u => u.usuario.ToUpper() == usuario).ToList();
                        }
                        return Json(mantenimiento.ToList());
                    }
                    else if (proceso == "Sustitutos")
                    {
                        IEnumerable<DiasSustitutosViewModel> sustitutos = new List<DiasSustitutosViewModel>();
                        if (this.User.IsInRole("Cliente"))
                        {

                            sustitutos = await _datosRepository.GetDiasSustitutosAsync(user.Cedula, placa);
                        }
                        else if (this.User.IsInRole("Admin") || this.User.IsInRole("Renting"))
                        {

                            sustitutos = await _datosRepository.GetDiasSustitutosAsync(cliente, placa);
                        }
                        if(validez=="1")
                        {
                            //return Json(sustitutos.Where(f=> Convert.ToDateTime(f.Fecha_asignacion)>=fechainicio && Convert.ToDateTime(f.Fecha_asignacion) <= fechafin).ToList());
                            sustitutos = sustitutos.Where(f => Convert.ToDateTime(f.Fecha_asignacion) >= fechainicio && Convert.ToDateTime(f.Fecha_asignacion) <= fechafin).ToList();
                        }
                        return Json(sustitutos.ToList());
                    }
                    else if (proceso == "Siniestros")
                    {
                        IEnumerable<SiniestrosDetalleViewModel> siniestros = new List<SiniestrosDetalleViewModel>();
                        if (this.User.IsInRole("Cliente"))
                        {
                            siniestros = await _datosRepository.GetSiniestrosDetalleAsync(user.Cedula, placa);
                        }
                        else if (this.User.IsInRole("Admin") || this.User.IsInRole("Renting"))
                        {
                            siniestros = await _datosRepository.GetSiniestrosDetalleAsync(cliente, placa);
                        }
                        if (validez == "1")
                        {
                            //return Json(siniestros.Where(f => Convert.ToDateTime(f.fecha_siniestro) >= fechainicio && Convert.ToDateTime(f.fecha_siniestro) <= fechafin).ToList());
                            siniestros = siniestros.Where(f => Convert.ToDateTime(f.fecha_siniestro) >= fechainicio && Convert.ToDateTime(f.fecha_siniestro) <= fechafin).ToList();
                        }
                        if (usuario != "" && usuario != "0")
                        {
                            siniestros = siniestros.Where(u => u.usuario.ToUpper() == usuario).ToList();
                        }
                        return Json(siniestros.ToList());
                    }
                }
            }


            //ViewBag.ChartData = vehiculosGps;
            //Source data returned as JSON    
            return Json(null);
        }
    }
}
