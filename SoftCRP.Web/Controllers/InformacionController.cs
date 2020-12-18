using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SoftCRP.Web.Data;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;

namespace SoftCRP.Web.Controllers
{
    public class InformacionController : BaseController
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ILogRepository _logRepository;
        private readonly ILogger<InformacionController> _logger;
        private readonly IDatosRepository _datosRepository;

        public InformacionController(
            DataContext context,
            IUserHelper userHelper,
            ILogRepository logRepository,
            ILogger<InformacionController> logger,
            IDatosRepository datosRepository)
        {
            _context = context;
            _userHelper = userHelper;
            _logRepository = logRepository;
            _logger = logger;
            _datosRepository = datosRepository;
        }
        public async Task<IActionResult> Index()
        {
            

            List<VehiculosClientesViewModel> Vehiculos = new List<VehiculosClientesViewModel>();

            var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            if (user != null)
            {
                if (this.User.IsInRole("Cliente"))
                {
                    var datosc = await _datosRepository.GetDatosCliente(user.Cedula);

                    if (datosc != null)
                    {
                        ViewBag.ClienteViewModel = datosc;
                        Vehiculos = await _datosRepository.GetVehiculosClienteAsync(user.Cedula);
                    }
                }
            }
            await _logRepository.SaveLogs("Get", "Obtiene Lista de Flota", "Información", User.Identity.Name);

            return View(Vehiculos);
        }

        public async Task<IActionResult> IndexLista()
        {

            //List<User> clientes = new List<User>();
            var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            if (user != null)
            {
                var clientes = await _userHelper.GetListUsersInRole("Cliente");
                await _logRepository.SaveLogs("Get", "Obtiene Lista de Clientes", "Información", User.Identity.Name);
                return View(clientes);
            }

            return View();
        }
        public async Task<IActionResult> Retorno(string id)
        {
            List<VehiculosClientesViewModel> Vehiculos = new List<VehiculosClientesViewModel>();

            var datosc = await _datosRepository.GetDatosCliente(id);

            if (datosc != null)
            {
                ViewBag.ClienteViewModel = datosc;
                Vehiculos = await _datosRepository.GetVehiculosClienteAsync(id);
            }

            await _logRepository.SaveLogs("Get", "Obtiene Lista de Flota", "Información", User.Identity.Name);
            return View(Vehiculos);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string buscarcliente)
        {
            
            List<VehiculosClientesViewModel> Vehiculos = new List<VehiculosClientesViewModel>();

            var datosc = await _datosRepository.GetDatosCliente(buscarcliente);

            if(datosc!=null)
            {
                ViewBag.ClienteViewModel = datosc;
                Vehiculos = await _datosRepository.GetVehiculosClienteAsync(buscarcliente);
            }
            

            return View(Vehiculos);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var vehiculo = await _datosRepository.GetDatosAutoAsync(id);


            if (vehiculo == null)
            {
                return NotFound();
            }

            await _logRepository.SaveLogs("Get", "Obtiene Información de Vehículo: "+vehiculo.Placa, "Información", User.Identity.Name);
            return View(vehiculo);

        }
    }
}