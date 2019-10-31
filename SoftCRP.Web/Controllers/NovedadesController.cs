using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;

namespace SoftCRP.Web.Controllers
{
    public class NovedadesController : BaseController
    {
        private readonly IUserHelper _userHelper;
        private readonly IDatosRepository _datosRepository;
        
        private readonly ICombosHelper _combosHelper;
        private readonly IFileHelper _fileHelper;
        private readonly IMailHelper _mailHelper;
        private readonly INovedadesRepository _novedadesRepository;
        private readonly DataContext _dataContext;

        public NovedadesController(
            IUserHelper userHelper,
            IDatosRepository datosRepository,
            INovedadesRepository novedadesRepository,
            ICombosHelper combosHelper,
            IFileHelper fileHelper,
            IMailHelper mailHelper,
            DataContext dataContext
            )
        {
            _userHelper = userHelper;
            _datosRepository = datosRepository;            
            _combosHelper = combosHelper;
            _fileHelper = fileHelper;
            _mailHelper = mailHelper;
            _novedadesRepository = novedadesRepository;
            _dataContext = dataContext;
        }
        public async Task<IActionResult> Index()
        {

            NovedadesViewModel model = new NovedadesViewModel();

            var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            if (user != null)
            {
                if (this.User.IsInRole("Cliente"))
                {
                    model = await _novedadesRepository.GetNovedadAsync(user.Cedula);
                }
                else
                {

                }
            }

            ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(user.Cedula);

            return View(model);
        }

        public async Task<IActionResult> Retorno(string id)
        {

            NovedadesViewModel model = new NovedadesViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = await _novedadesRepository.GetNovedadAsync(id);
                ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(id);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string buscarcliente)
        {

            NovedadesViewModel model = new NovedadesViewModel();
            if (!string.IsNullOrEmpty(buscarcliente))
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                if (user != null)
                {
                    model = await _novedadesRepository.GetNovedadAsync(buscarcliente);
                }
                ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(buscarcliente);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            NovedadesCreateViewModel novedadesCreateViewModel = new NovedadesCreateViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                novedadesCreateViewModel.cedula = id;
                novedadesCreateViewModel.Placas = await _combosHelper.GetComboPlacas(id);
                novedadesCreateViewModel.MotivoTypes = await _combosHelper.GetComboTipoNovedades();
                novedadesCreateViewModel.SubMotivoTypes = await _combosHelper.GetComboSubMotivos();
                novedadesCreateViewModel.ViaIngresoTypes = await _combosHelper.GetComboViaIngreso();

            }
            else
            {

            }

            return View(novedadesCreateViewModel);
        }


        [HttpPost]

        public async Task<IActionResult> Create(NovedadesCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                var novedad = new Novedad
                {
                    Cedula = model.cedula,
                    Fecha = DateTime.Now,
                    Observaciones = model.Observaciones,
                    Placa = model.PlacaId,
                    Motivo = model.MotivoId,
                    SubMotivo=model.SubMotivoId,
                    ViaIngreso=model.ViaIngresoId,
                    user = user
                };

                _dataContext.novedades.Add(novedad);
                await _dataContext.SaveChangesAsync();
                //enviar correo
                var datos = await _datosRepository.GetDatosCliente(model.cedula);
                //var tipoAnalisis = await _dataContext.TiposAnalisis.FindAsync(model.TipoAnalisisId);

                var emails = "roy_chavez15@hotmail.com";

                //TODO: cambiar direccion de correo
                _mailHelper.SendMail(emails, "SoftCRP Nueva Novedad Creado",
                    $"<html xmlns='http://www.w3.org/1999/xhtml'>" +
                    $"<head>" +
                    $"<title>" +
                    $"</title>" +
                    $"</head>" +
                    $"<body>" +
                    $"<h1>SoftCRP Nuevo Novedad</h1>" +
                    $"<table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' style='border-collapse:collapse; max-width:600px!important; width:100%; margin: auto'>" +
                    $"<tr><td style='font-weight:bold'>Placa</td><td>{model.PlacaId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Motivo</td><td>{model.MotivoId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>SubMotivo</td><td>{model.SubMotivoId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Vía Ingreso</td><td>{model.ViaIngresoId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Observación</td><td>{model.Observaciones}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Creador por</td><td>{user.FullName}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Fecha</td><td>{novedad.Fecha}</td></tr></table></body></html>");

                return Ok(model);
            }
            //Alert("No se pudo agregar el cliente, revise los datos", Enum.Enum.NotificationType.error);
            //return View(model);
            return BadRequest(model);
        }


        public async Task<IActionResult> Crear(NovedadesCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                var novedad = new Novedad
                {
                    Cedula = model.cedula,
                    Fecha = DateTime.Now,
                    Observaciones = model.Observaciones,
                    Placa = model.PlacaId,
                    Motivo = model.MotivoId,
                    SubMotivo = model.SubMotivoId,
                    ViaIngreso = model.ViaIngresoId,
                    user = user
                };

                _dataContext.novedades.Add(novedad);
                await _dataContext.SaveChangesAsync();
                //enviar correo
                var datos = await _datosRepository.GetDatosCliente(model.cedula);
                //var tipoAnalisis = await _dataContext.TiposAnalisis.FindAsync(model.TipoAnalisisId);

                var emails = "roy_chavez15@hotmail.com";

                //TODO: cambiar direccion de correo
                _mailHelper.SendMail(emails, "SoftCRP Nueva Novedad Creado",
                    $"<html xmlns='http://www.w3.org/1999/xhtml'>" +
                    $"<head>" +
                    $"<title>" +
                    $"</title>" +
                    $"</head>" +
                    $"<body>" +
                    $"<h1>SoftCRP Nuevo Novedad</h1>" +
                    $"<table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' style='border-collapse:collapse; max-width:600px!important; width:100%; margin: auto'>" +
                    $"<tr><td style='font-weight:bold'>Placa</td><td>{model.PlacaId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Motivo</td><td>{model.MotivoId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>SubMotivo</td><td>{model.SubMotivoId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Vía Ingreso</td><td>{model.ViaIngresoId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Observación</td><td>{model.Observaciones}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Creador por</td><td>{user.FullName}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Fecha</td><td>{novedad.Fecha}</td></tr></table></body></html>");

                return Ok(model);
            }
            //Alert("No se pudo agregar el cliente, revise los datos", Enum.Enum.NotificationType.error);
            //return View(model);
            return BadRequest(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var cliente = await _dataContext.Analises
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var novedad = await _novedadesRepository.GetNovedadByIdAsync(id);

            if (novedad == null)
            {
                return NotFound();
            }

            return View(novedad);
        }
        // GET: Clientes/Edit/5
        [Authorize(Roles = "Admin,Renting")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novedad = await _novedadesRepository.GetNovedadByIdAsync(id);
            if (novedad == null)
            {
                return NotFound();
            }

            var novedadesEditViewModel = new NovedadesEditViewModel
            {
                Id = novedad.Id,
                Cedula = novedad.Cedula,
                archivoNovedades = novedad.archivoNovedades,
                Motivo = novedad.Motivo,
                Novedad = novedad.Observaciones,
                Placa = novedad.Placa,
                SubMotivo = novedad.SubMotivo,
                Via = novedad.ViaIngreso,
                Solucion = novedad.Solucion,
                Estados = await _combosHelper.GetComboEstadoNovedad(),
                //EstadoId=novedad.EstadoSolucion
            };

            return View(novedadesEditViewModel);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int id, NovedadesEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //var novedad = _dataContext.novedades.Find(model.Id);
                var novedad = await _novedadesRepository.GetNovedadByIdAsync(model.Id);

                novedad.Solucion = model.Solucion;
                novedad.userSolucion=await _userHelper.GetUserAsync(this.User.Identity.Name);
                novedad.FechaSolucion = DateTime.Now;
                novedad.EstadoSolucion = model.EstadoId;

                try
                {
                    _dataContext.Update(novedad);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnalisisExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Retorno), new { id = novedad.Cedula });
            }
            return View(_dataContext);
        }

        //[HttpPost]
        [Authorize(Roles = "Admin,Renting")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novedades = await _dataContext.novedades
                .Include(a => a.archivoNovedades)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);

            var cedula = novedades.Cedula;

            _dataContext.novedades.Remove(novedades);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction($"{nameof(Retorno)}/{cedula}");
        }

        private bool AnalisisExists(int id)
        {
            return _dataContext.novedades.Any(e => e.Id == id);
        }


        public async Task<IActionResult> AddFile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novedades = await _dataContext.novedades.FindAsync(id.Value);
            if (novedades == null)
            {
                return NotFound();
            }

            var model = new ArchivoNovedadesViewModel
            {
                Id = novedades.Id
            };

            return View(model);
        }

        [Authorize(Roles = "Admin,Renting")]
        [HttpPost]
        public async Task<IActionResult> AddFile(ArchivoNovedadesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;
                var extension = string.Empty;

                if (model.Archivo != null)
                {
                    path = await _fileHelper.UploadFileAsync(model.Archivo, "Novedades");
                    extension = Path.GetExtension(model.Archivo.FileName);
                }

                var archivoNovedades = new ArchivoNovedades
                {
                    novedad = await _dataContext.novedades.FindAsync(model.Id),
                    ArchivoPath = path,
                    user = await _userHelper.GetUserAsync(this.User.Identity.Name),
                    Fecha = DateTime.Now,
                    tamanio = model.Archivo.Length,
                    TipoArchivo = extension,
                    //Property = await _dataContext.Properties.FindAsync(model.Id)
                };

                _dataContext.archivoNovedades.Add(archivoNovedades);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"{nameof(Retorno)}/{archivoNovedades.novedad.Cedula}");
            }

            return View(model);
        }

        [Authorize(Roles = "Admin,Renting")]
        public async Task<IActionResult> DeleteFile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _dataContext.archivoNovedades
                .Include(pi => pi.novedad)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);
            if (file == null)
            {
                return NotFound();
            }

            _dataContext.archivoNovedades.Remove(file);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction($"{nameof(Retorno)}/{file.novedad.Cedula}");
        }

        public async Task<IActionResult> DownloadFile(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var file = await _dataContext.archivoNovedades
                .Include(pi => pi.novedad)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);
            if (file == null)
            {
                return NotFound();
            }


            //return _fileHelper.GetFileAsStream(file.ArchivoPath.Substring(1)) ?? (IActionResult)NotFound();
            return _fileHelper.GetFile(file.ArchivoPath.Substring(1)) ?? (IActionResult)NotFound();
        }
    }
}