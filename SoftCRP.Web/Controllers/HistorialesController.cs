using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [Authorize(Roles = "Admin,Renting")]
    public class HistorialesController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IDatosRepository _datosRepository;
        private readonly IUserHelper _userHelper;
        private readonly ILogRepository _logRepository;

        public HistorialesController(
            DataContext dataContext,
            IDatosRepository datosRepository,
            IUserHelper userHelper,
            ILogRepository logRepository)
        {
            _dataContext = dataContext;
            _datosRepository = datosRepository;
            _userHelper = userHelper;
            _logRepository = logRepository;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            if (user != null)
            {

                var clientes = await _userHelper.GetListUsersInRole("Cliente");

                foreach (var cliente in clientes)
                {
                    var Vehiculos = await _datosRepository.GetVehiculosClienteAsync(cliente.Cedula);
                    foreach (var vh in Vehiculos)
                    {
                        var hist = _dataContext.Histories
                            .Where(p => p.Placa == vh.placa && p.User == cliente)
                            .FirstOrDefault();

                        if (hist == null)
                        {
                            History historial = new History
                            {
                                isActive = false,
                                Desde = DateTime.Now,
                                Hasta = DateTime.Now,
                                Placa = vh.placa,
                                User = cliente
                            };

                            await _dataContext.Histories.AddAsync(historial);
                            await _dataContext.SaveChangesAsync();
                        }

                        var hist1 = _dataContext.Histories
                            .Where(p => p.Placa == "" && p.User == cliente)
                            .FirstOrDefault();
                        if (hist1 == null)
                        {
                            History historial1 = new History
                            {
                                isActive = false,
                                Desde = DateTime.Now,
                                Hasta = DateTime.Now,
                                Placa = "",
                                User = cliente
                            };

                            await _dataContext.Histories.AddAsync(historial1);
                            await _dataContext.SaveChangesAsync();
                        }
                        var hist2 = _dataContext.Histories
                            .Where(p => p.Placa.ToUpper() == "FLOTA" && p.User == cliente)
                            .FirstOrDefault();
                        if (hist2 == null)
                        {
                            History historial2 = new History
                            {
                                isActive = false,
                                Desde = DateTime.Now,
                                Hasta = DateTime.Now,
                                Placa = "FLOTA",
                                User = cliente
                            };

                            await _dataContext.Histories.AddAsync(historial2);
                            await _dataContext.SaveChangesAsync();
                        }
                    }
                }
                //var Vehiculos = await _datosRepository.GetVehiculosClienteAsync("");
                //foreach(var vh in Vehiculos)
                //{
                //    var hist = _dataContext.Histories
                //        .Where(p => p.Placa == vh.placa && p.User.Cedula == vh.nit_cliente)
                //        .FirstOrDefault();
                //    if (hist == null)
                //    {
                //        History historial = new History
                //        {
                //            isActive = false,
                //            Desde = DateTime.Now,
                //            Hasta = DateTime.Now,
                //            Placa = vh.placa,
                //            User = cliente
                //        };

                //        await _dataContext.Histories.AddAsync(historial);
                //        await _dataContext.SaveChangesAsync();
                //    }

                //    var hist1 = _dataContext.Histories
                //        .Where(p => p.Placa == "" && p.User == cliente)
                //        .FirstOrDefault();
                //    if (hist1 == null)
                //    {
                //        History historial1 = new History
                //        {
                //            isActive = false,
                //            Desde = DateTime.Now,
                //            Hasta = DateTime.Now,
                //            Placa = "",
                //            User = cliente
                //        };

                //        await _dataContext.Histories.AddAsync(historial1);
                //        await _dataContext.SaveChangesAsync();
                //    }
                //}
                await _logRepository.SaveLogs("Get", "Obtiene Lista de Clientes", "Historial", User.Identity.Name);
                return View(clientes);
            }
            return View();
        }
        public async Task<IActionResult> Retorno(string id)
        {
            List<VehiculosClientesViewModel> Vehiculos = new List<VehiculosClientesViewModel>();
            HistorialDataViewModel historialDataViewModel = new HistorialDataViewModel();
            var cliente = await _userHelper.GetUserByCedulaAsync(id);
            var datosc = await _datosRepository.GetDatosCliente(id);

            if (datosc != null && cliente != null)
            {
                ViewBag.ClienteViewModel = datosc;
                Vehiculos = await _datosRepository.GetVehiculosClienteAsync(id);
                foreach(var placas in Vehiculos)
                {
                    var hist = _dataContext.Histories
                        .Where(p => p.Placa == placas.placa && p.User == cliente)
                        .FirstOrDefault();
                    if(hist==null)
                    {
                        History historial = new History
                        {
                            isActive=false,
                            Desde= DateTime.Now,
                            Hasta=DateTime.Now,
                            Placa=placas.placa,
                            User=cliente
                        };

                        await _dataContext.Histories.AddAsync(historial);
                        await _dataContext.SaveChangesAsync();
                    }

                    var hist1 = _dataContext.Histories
                        .Where(p => p.Placa == "" && p.User == cliente)
                        .FirstOrDefault();
                    if (hist1 == null)
                    {
                        History historial1 = new History
                        {
                            isActive = false,
                            Desde = DateTime.Now,
                            Hasta = DateTime.Now,
                            Placa = "",
                            User = cliente
                        };

                        await _dataContext.Histories.AddAsync(historial1);
                        await _dataContext.SaveChangesAsync();
                    }

                    var hist2 = _dataContext.Histories
                        .Where(p => p.Placa.ToUpper() == "FLOTA" && p.User == cliente)
                        .FirstOrDefault();
                    if (hist2 == null)
                    {
                        History historial2 = new History
                        {
                            isActive = false,
                            Desde = DateTime.Now,
                            Hasta = DateTime.Now,
                            Placa = "FLOTA",
                            User = cliente
                        };

                        await _dataContext.Histories.AddAsync(historial2);
                        await _dataContext.SaveChangesAsync();
                    }
                }
            }


            var historialcliente = _dataContext.Histories
                .Where(i => i.User == cliente)
                .ToList();
            var historialcliente1 = _dataContext.Histories
                .Where(i => i.User == cliente && i.Placa=="")
                .FirstOrDefault();
            if(historialcliente1!=null)
            {
                ViewBag.desdet = historialcliente1.Desde.ToString("dd/MM/yyyy");
                ViewBag.hastat = historialcliente1.Hasta.ToString("dd/MM/yyyy");
                ViewBag.isActivet = historialcliente1.isActive;

                historialDataViewModel.desde= historialcliente1.Desde;
                historialDataViewModel.hasta = historialcliente1.Hasta;
                historialDataViewModel.isActive= historialcliente1.isActive;
            }
            historialDataViewModel.Histories = historialcliente;

            await _logRepository.SaveLogs("Get", "Obtiene Lista de Flota", "Información", User.Identity.Name);
            return View(historialDataViewModel);
        }

        public IActionResult ActualizaHistorial(int id)
        {
            var historial = _dataContext.Histories
                .Include(u=>u.User)
                .Where(i => i.Id == id)
                .FirstOrDefault();

            return PartialView("_ActualizaHistorialPartialView", historial);
        }

        [HttpPost]
        public async Task<IActionResult> GuardaHistorial(History model)
        {
            var response = new ResponseViewModel();
            var historial = _dataContext.Histories
                .Include(u => u.User)
                .Where(i => i.Id == model.Id)
                .FirstOrDefault();

            if(historial!=null)
            {
                historial.Desde = model.Desde;
                historial.Hasta = model.Hasta;
                historial.isActive = model.isActive;

                try
                {
                    _dataContext.Histories.Update(historial);
                    await _dataContext.SaveChangesAsync();
                    response.status = "OK";
                    response.description = historial.User.Cedula;
                }
                catch (Exception ex)
                {
                    response.status = "ERROR";
                    response.description = ex.Message;
                }




                return Ok(response);

            }
            response.status = "ERROR";
            response.description = "NO EXISTE HISTORIAL";


            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GuardaHistorialt(History model)
        {
            var response = new ResponseViewModel();
            var historial = _dataContext.Histories
                .Include(u => u.User)
                .Where(i => i.User.Cedula == model.Placa)
                .ToList();

            if (historial != null)
            {

                foreach(var reg in historial)
                {
                    if(model.isActive)
                    {
                        reg.Desde = model.Desde;
                        reg.Hasta = model.Hasta;
                        reg.isActive = model.isActive;
                    }
                    else
                    {
                        reg.isActive = model.isActive;
                    }
                    try
                    {
                        _dataContext.Histories.Update(reg);
                        await _dataContext.SaveChangesAsync();
                        response.status = "OK";
                        response.description = reg.User.Cedula;
                    }
                    catch (Exception ex)
                    {
                        response.status = "ERROR";
                        response.description = ex.Message;
                    }
                }


                return Ok(response);

            }
            response.status = "ERROR";
            response.description = "NO EXISTE HISTORIAL";


            return Ok(response);
        }
    }
}
