using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SoftCRP.Web.Data;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;

namespace SoftCRP.Web.Controllers
{
    public class InformacionController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IDatosRepository _datosRepository;

        public InformacionController(
            DataContext context,
            IUserHelper userHelper,
            IDatosRepository datosRepository)
        {
            _context = context;
            _userHelper = userHelper;
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
                    Vehiculos = await _datosRepository.GetVehiculosClienteAsync(user.Cedula);
                }
            }

            return View(Vehiculos);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string buscarcliente)
        {
            
            List<VehiculosClientesViewModel> Vehiculos = new List<VehiculosClientesViewModel>();

            //var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            //if (user != null)
            //{
            //    if (this.User.IsInRole("Cliente"))
            //    {
            //        Vehiculos = await _datosRepository.GetVehiculosClienteAsync(user.Cedula);
            //    }
            //}

            Vehiculos = await _datosRepository.GetVehiculosClienteAsync(buscarcliente);

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
            return View(vehiculo);

        }
    }
}