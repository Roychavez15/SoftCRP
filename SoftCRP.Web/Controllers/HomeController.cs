﻿using System;
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
        private readonly IDatosRepository _datosRepository;

        public HomeController(
            IUserHelper userHelper,
            IDatosRepository datosRepository)
        {
            _userHelper = userHelper;
            _datosRepository = datosRepository;
        }
        public async Task<IActionResult> Index()
        {

            List<VehiculosClientesViewModel> Vehiculos = new List<VehiculosClientesViewModel>();

            if (!string.IsNullOrEmpty(this.User.Identity.Name))
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                if (user != null)
                {
                    if (this.User.IsInRole("Cliente"))
                    {
                        Vehiculos = await _datosRepository.GetVehiculosClienteAsync(user.Cedula);
                    }
                }
            }
            return View(Vehiculos);
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
    }
}
