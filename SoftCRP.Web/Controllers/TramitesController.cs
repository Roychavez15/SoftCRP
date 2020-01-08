using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;

namespace SoftCRP.Web.Controllers
{
    public class TramitesController : BaseController
    {
        private readonly IUserHelper _userHelper;
        private readonly IDatosRepository _datosRepository;
        private readonly ITramitesRepository _tramitesRepository;
        private readonly ICombosHelper _combosHelper;
        private readonly IFileHelper _fileHelper;
        private readonly IMailHelper _mailHelper;
        private readonly ILogRepository _logRepository;
        private readonly DataContext _dataContext;

        public TramitesController(
            IUserHelper userHelper,
            IDatosRepository datosRepository,
            ITramitesRepository tramitesRepository,
            ICombosHelper combosHelper,
            IFileHelper fileHelper,
            IMailHelper mailHelper,
            ILogRepository logRepository,
            DataContext dataContext)
        {
            _userHelper = userHelper;
            _datosRepository = datosRepository;
            _tramitesRepository = tramitesRepository;
            _combosHelper = combosHelper;
            _fileHelper = fileHelper;
            _mailHelper = mailHelper;
            _logRepository = logRepository;
            _dataContext = dataContext;
        }
        public async Task<IActionResult> Index()
        {

            TramitesViewModel model = new TramitesViewModel();

            var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            if (user != null)
            {
                if (this.User.IsInRole("Cliente"))
                {
                    model = await _tramitesRepository.GetTramiteAsync (user.Cedula);
                }
                else
                {

                }
            }

            ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(user.Cedula);
            await _logRepository.SaveLogs("Get", "Obtiene lista de Trámites", "Trámites", User.Identity.Name);
            return View(model);
        }
        public async Task<IActionResult> IndexLista()
        {

            //List<User> clientes = new List<User>();
            var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            if (user != null)
            {
                var clientes = await _userHelper.GetListUsersInRole("Cliente");
                await _logRepository.SaveLogs("Get", "Obtiene lista de Clientes", "Trámites", User.Identity.Name);
                return View(clientes);
            }

            return View();
        }
        public async Task<IActionResult> Retorno(string id)
        {

            TramitesViewModel model = new TramitesViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = await _tramitesRepository.GetTramiteAsync(id);
                ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(id);
            }
            await _logRepository.SaveLogs("Get", "Obtiene lista de Trámites", "Trámites", User.Identity.Name);
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Index(string buscarcliente)
        {

            TramitesViewModel model = new TramitesViewModel();
            if (!string.IsNullOrEmpty(buscarcliente))
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                if (user != null)
                {
                    model = await _tramitesRepository.GetTramiteAsync(buscarcliente);
                }
                ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(buscarcliente);
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Renting")]
        public async Task<IActionResult> Create(string id)
        {
            TramitesCreateViewModel tramitesCreateViewModel = new TramitesCreateViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                tramitesCreateViewModel.cedula = id;
                tramitesCreateViewModel.Placas = await _combosHelper.GetComboPlacas(id);
                tramitesCreateViewModel.TramitesTypes = _combosHelper.GetComboTipoTramites();
                tramitesCreateViewModel.Meses = _combosHelper.GetComboMes();
                tramitesCreateViewModel.Anios = _combosHelper.GetComboAnio();
            }
            else
            {

            }

            return View(tramitesCreateViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TramitesCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                List<ArchivoTramites> archivoTramitesList = new List<ArchivoTramites>();

                foreach (IFormFile file in model.Files)
                {
                    var path = string.Empty;
                    var extension = string.Empty;

                    path = await _fileHelper.UploadFileAsync(file, "Tramites");
                    extension = Path.GetExtension(file.FileName);

                    var archivoTramites = new ArchivoTramites
                    {
                        //capacitacion = await _dataContext.capacitaciones.FindAsync(model.Id),
                        ArchivoPath = path,
                        user = await _userHelper.GetUserAsync(this.User.Identity.Name),
                        Fecha = DateTime.Now,
                        tamanio = file.Length,
                        TipoArchivo = extension,
                        //Property = await _dataContext.Properties.FindAsync(model.Id)
                    };
                    archivoTramitesList.Add(archivoTramites);
                }

                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                var tramite = new Tramite
                {
                    Cedula = model.cedula,
                    Fecha = DateTime.Now,
                    Observaciones = model.Observaciones,
                    Placa = model.PlacaId,
                    tipoTramiteId = model.TipoTramiteId,
                    Anio=model.AnioId,
                    Mes=model.MesId,  
                    archivoTramites=archivoTramitesList,
                    user = user
                };

                _dataContext.tramites.Add(tramite);
                await _dataContext.SaveChangesAsync();

                await _logRepository.SaveLogs("Crear", "Crea Trámites Id: "+tramite.Id.ToString(), "Trámites", User.Identity.Name);
                //enviar correo
                //var datos = await _datosRepository.GetDatosCliente(model.cedula);
                var tipoTramite = await _dataContext.tipoTramites.FindAsync(model.TipoTramiteId);

                //var emails = "roy_chavez15@hotmail.com";
                var datos = await _userHelper.GetUserByCedulaAsync(model.cedula);
                var emails = user.Email + ',' + datos.Email;

                //TODO: cambiar direccion de correo
                _mailHelper.SendMailAttachment(emails, "Plataforma Clientes Nuevo Tramite Creado",
                    $"<html xmlns='http://www.w3.org/1999/xhtml'>" +
                    $"<head>" +
                    $"<meta http-equiv=" + "Content-Type" + " content=" + "text/html; charset = UTF-8" + " />" +
                    $"<title>" +
                    $"</title>" +
                    $"</head>" +
                    $"<body>" +
                    $"<h1>Plataforma Clientes Nuevo Trámite</h1>" +
                    $"<table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' style='border-collapse:collapse; max-width:600px!important; width:100%; margin: auto'>" +
                    $"<tr><td style='font-weight:bold'>Placa</td><td>{model.PlacaId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Tipo</td><td>{tipoTramite.Tipo}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Año</td><td>{model.AnioId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Mes</td><td>{model.MesId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Observación</td><td>{model.Observaciones}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Creador por</td><td>{user.FullName}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Fecha</td><td>{tramite.Fecha}</td></tr></table></body></html>",model.Files);

                return Ok(model);
            }
            //Alert("No se pudo agregar el cliente, revise los datos", Enum.Enum.NotificationType.error);
            //return View(model);
            return BadRequest(model);
        }

        public async Task<IActionResult> Crear(TramitesCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<ArchivoTramites> archivoTramitesList = new List<ArchivoTramites>();
                if (model.Files != null)
                {
                    foreach (IFormFile file in model.Files)
                    {
                        var path = string.Empty;
                        var extension = string.Empty;

                        path = await _fileHelper.UploadFileAsync(file, "Tramites");
                        extension = Path.GetExtension(file.FileName);

                        var archivoTramites = new ArchivoTramites
                        {
                            //capacitacion = await _dataContext.capacitaciones.FindAsync(model.Id),
                            ArchivoPath = path,
                            user = await _userHelper.GetUserAsync(this.User.Identity.Name),
                            Fecha = DateTime.Now,
                            tamanio = file.Length,
                            TipoArchivo = extension,
                            //Property = await _dataContext.Properties.FindAsync(model.Id)
                        };
                        archivoTramitesList.Add(archivoTramites);
                    }
                }

                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                var tramite = new Tramite
                {
                    Cedula = model.cedula,
                    Fecha = DateTime.Now,
                    Observaciones = model.Observaciones,
                    Placa = model.PlacaId,
                    tipoTramiteId = model.TipoTramiteId,
                    Anio = model.AnioId,
                    Mes = model.MesId,
                    archivoTramites=archivoTramitesList,
                    user = user
                };

                _dataContext.tramites.Add(tramite);
                await _dataContext.SaveChangesAsync();
                await _logRepository.SaveLogs("Crear", "Crea Trámites Id: " + tramite.Id.ToString(), "Trámites", User.Identity.Name);

                //enviar correo
                //var datos = await _datosRepository.GetDatosCliente(model.cedula);
                var tipoTramite = await _dataContext.tipoTramites.FindAsync(model.TipoTramiteId);

                //var emails = "roy_chavez15@hotmail.com";
                var datos = await _userHelper.GetUserByCedulaAsync(model.cedula);
                var emails = user.Email + ',' + datos.Email;

                //TODO: cambiar direccion de correo
                _mailHelper.SendMailAttachment(emails, "Plataforma Clientes",
                    $"<html xmlns='http://www.w3.org/1999/xhtml'>" +
                    $"<head>" +
                    $"<meta http-equiv=" + "Content-Type" + " content=" + "text/html; charset = UTF-8" + " />" +
                    $"<title>" +
                    $"</title>" +
                    $"</head>" +
                    $"<body>" +
                    //$"<h1>Plataforma Clientes Nuevo Trámite</h1>" +
                    $"<hr width=100% align='center' size=30 color='#002D73' style='margin:0px;padding:0px'>" +
                    $"<hr width=100% align='center' size=5 color='#F2AE0B' style='margin:0px;padding:0px'>" +
                    $"<br><br>" +
                    $"<p>Estimado Cliente" +
                    $"<p>Renting Pichincha comunica que se ha cargado en la plataforma de clientes un nuevo Trámite perteneciente al vehículo:" +

                    $"<table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' style='border-collapse:collapse; max-width:600px!important; width:100%; margin: auto'>" +
                    $"<tr><td style='font-weight:bold'>Placa</td><td>{model.PlacaId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Tipo</td><td>{tipoTramite.Tipo}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Año</td><td>{model.AnioId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Mes</td><td>{model.MesId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Observación</td><td>{model.Observaciones}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Creador por</td><td>{user.FullName}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Fecha</td><td>{tramite.Fecha}</td></tr></table>"+
                    $"<br><br>" +
                    $"<p>Para poder revisar la información de su plataforma ingrese a su cuenta con su usuario y contraseña." +
                    $"<div align='center'><a href='http://181.112.216.3:8084'><img src='http://181.112.216.3:8084/images/email1.png' align='center'></a></div>" +
                    $"<br><br>" +
                    $"<p>Es un placer estar en contacto.<br>" +
                    $"<p>Saludos cordiales<br>" +
                    $"<br><br>" +
                    $"<p>Consorcio Pichincha S.A CONDELPI<br>" +
                    $"<p>Av.González Suárez N32-346 y Coruña<br>" +
                    $"<p><img src='http://181.112.216.3:8084/images/call.png' width=30px>Call Center: 1-800 RENTING(736846)<br>" +
                    $"<p><img src='http://181.112.216.3:8084/images/email.png' width=25px>E-Mail: inforenting@condelpi.com<br>" +
                    $"<p><img src='http://181.112.216.3:8084/images/whatsapp.jpg' width=25px>WhatsApp: 0997652137" +
                    $"<p>Quito-Ecuador" +
                    $"<br><br>" +
                    $"<img src='http://181.112.216.3:8084/images/email2.png' width=200px>" +
                    $"<hr width=100% align='center' size=30 color='#002D73' style='margin:0px;padding:0px'>" +
                    $"<hr width=100% align='center' size=5 color='#F2AE0B' style='margin:0px;padding:0px'></body></html>"
                    , model.Files);

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
            var tramite = await _tramitesRepository.GetTramiteByIdAsync(id);

            if (tramite == null)
            {
                return NotFound();
            }
            await _logRepository.SaveLogs("Detalles", "Trámites Id: " + tramite.Id.ToString(), "Trámites", User.Identity.Name);
            return View(tramite);
        }
        // GET: Clientes/Edit/5
        [Authorize(Roles = "Admin,Renting")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var analisis = await _dataContext.Analises.FindAsync(id);
            var analisis = await _tramitesRepository.GetTramiteByIdAsync(id);
            if (analisis == null)
            {
                return NotFound();
            }
            return View(analisis);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int id, Tramite tramite)
        {
            if (id != tramite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(tramite);
                    await _dataContext.SaveChangesAsync();
                    await _logRepository.SaveLogs("Editar", "Trámites Id: " + tramite.Id.ToString(), "Trámites", User.Identity.Name);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnalisisExists(tramite.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Retorno), new { id = tramite.Cedula });
            }
            return View(_dataContext);
        }

        [Authorize(Roles = "Admin,Renting")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tramite = await _dataContext.tramites
                .Include(a => a.archivoTramites)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);

            var cedula = tramite.Cedula;

            await _logRepository.SaveLogs("Borrar", "Borra Trámites Id: " + tramite.Id.ToString(), "Trámites", User.Identity.Name);
            _dataContext.tramites.Remove(tramite);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction($"{nameof(Retorno)}/{cedula}");
        }

        private bool AnalisisExists(int id)
        {
            return _dataContext.Analises.Any(e => e.Id == id);
        }


        public async Task<IActionResult> AddFile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tramite = await _dataContext.tramites.FindAsync(id.Value);
            if (tramite == null)
            {
                return NotFound();
            }

            var model = new ArchivoTramitesViewModel
            {
                Id = tramite.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(ArchivoTramitesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;
                var extension = string.Empty;

                if (model.Archivo != null)
                {
                    path = await _fileHelper.UploadFileAsync(model.Archivo, "Tramites");
                    extension = Path.GetExtension(model.Archivo.FileName);

                    var archivoTramites = new ArchivoTramites
                    {
                        tramite = await _dataContext.tramites.FindAsync(model.Id),
                        ArchivoPath = path,
                        user = await _userHelper.GetUserAsync(this.User.Identity.Name),
                        Fecha = DateTime.Now,
                        tamanio = model.Archivo.Length,
                        TipoArchivo = extension,
                        //Property = await _dataContext.Properties.FindAsync(model.Id)
                    };

                    _dataContext.archivoTramites.Add(archivoTramites);
                    await _dataContext.SaveChangesAsync();

                    await _logRepository.SaveLogs("Guarda", "Guarda Archivo Trámites Id: " + archivoTramites.Id.ToString(), "Trámites", User.Identity.Name);
                    //return RedirectToAction($"{nameof(Retorno)}/{archivoTramites.tramite.Cedula}");
                    return RedirectToAction(nameof(Edit), new { id = model.Id });
                }


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

            var file = await _dataContext.archivoTramites
                .Include(pi => pi.tramite)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);
            if (file == null)
            {
                return NotFound();
            }

            await _logRepository.SaveLogs("Borrar", "Borrar Archivo Trámites Id: " + file.Id.ToString(), "Trámites", User.Identity.Name);
            _dataContext.archivoTramites.Remove(file);
            await _dataContext.SaveChangesAsync();
            //return RedirectToAction($"{nameof(Retorno)}/{file.tramite.Cedula}");
            return RedirectToAction($"{nameof(Edit)}/{file.tramite.Id}");
        }

        public async Task<IActionResult> DownloadFile(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var file = await _dataContext.archivoTramites
                .Include(pi => pi.tramite)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);
            if (file == null)
            {
                return NotFound();
            }

            await _logRepository.SaveLogs("Descargar", "Descargar Archivo Trámites Id: " + file.Id.ToString(), "Trámites", User.Identity.Name);
            //return _fileHelper.GetFileAsStream(file.ArchivoPath.Substring(1)) ?? (IActionResult)NotFound();
            return _fileHelper.GetFile(file.ArchivoPath.Substring(1)) ?? (IActionResult)NotFound();
        }

    }
}