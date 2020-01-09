using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Controllers
{
    public class AnalisisController : BaseController
    {
        private readonly IUserHelper _userHelper;
        private readonly IDatosRepository _datosRepository;
        private readonly IAnalisisRepository _analisisRepository;
        private readonly ICombosHelper _combosHelper;
        private readonly IFileHelper _fileHelper;
        private readonly IMailHelper _mailHelper;
        private readonly ILogRepository _logRepository;
        private readonly DataContext _dataContext;

        public AnalisisController(
            IUserHelper userHelper,
            IDatosRepository datosRepository,
            IAnalisisRepository analisisRepository,
            ICombosHelper combosHelper,     
            IFileHelper fileHelper,
            IMailHelper mailHelper,
            ILogRepository logRepository,
            DataContext dataContext
            )
        {
            _userHelper = userHelper;
            _datosRepository = datosRepository;
            _analisisRepository = analisisRepository;
            _combosHelper = combosHelper;
            _fileHelper = fileHelper;
            _mailHelper = mailHelper;
            _logRepository = logRepository;
            _dataContext = dataContext;
        }
        // GET: TipoAnalisis
        public async Task<IActionResult> Index()
        {
            

            AnalisisViewModel model = new AnalisisViewModel();
            
            var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            if (user != null)
            {
                if (this.User.IsInRole("Cliente"))
                {
                    model = await _analisisRepository.GetAnalisis(user.Cedula);
                }
                else
                {
                   
                }
            }

            ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(user.Cedula);

            await _logRepository.SaveLogs("Get", "Obtiene lista de Análisis", "Análisis", User.Identity.Name);
            return View(model);
        }

        public async Task<IActionResult> IndexLista()
        {
            
            //List<User> clientes = new List<User>();
            var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            if (user != null)
            {
                await _logRepository.SaveLogs("Get", "Obtiene lista de Clientes", "Análisis", User.Identity.Name);
                var clientes = await _userHelper.GetListUsersInRole("Cliente");
                return View(clientes);
            }
            
            return View();
        }
        public async Task<IActionResult> Retorno(string id)
        {
            
            AnalisisViewModel model = new AnalisisViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = await _analisisRepository.GetAnalisis(id);
                ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(id);
            }
            await _logRepository.SaveLogs("Get", "Obtiene lista de Análisis", "Análisis", User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string buscarcliente)
        {

            AnalisisViewModel model = new AnalisisViewModel();
            if (!string.IsNullOrEmpty(buscarcliente))
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                if (user != null)
                {
                    model = await _analisisRepository.GetAnalisis(buscarcliente);
                }
                ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(buscarcliente);
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Renting")]
        // GET: Clientes/Create
        public async Task<IActionResult> Create(string id)
        {
            AnalisisCreateViewModel analisisCreateViewModel = new AnalisisCreateViewModel();
            if(!string.IsNullOrEmpty(id))
            {
                analisisCreateViewModel.cedula = id;
                analisisCreateViewModel.Placas = await _combosHelper.GetComboPlacas(id);
                analisisCreateViewModel.AnalisisTypes = _combosHelper.GetComboTipoAnalisis();
            }
            else
            {

            }

            return View(analisisCreateViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(AnalisisCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<ArchivoAnalisis> archivoAnalises = new List<ArchivoAnalisis>();

                foreach (IFormFile file in model.Files)
                {
                    //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", photo.FileName);
                    //var stream = new FileStream(path, FileMode.Create);
                    //photo.CopyToAsync(stream);
                    //product.Photos.Add(photo.FileName);
                    var path = string.Empty;
                    var extension = string.Empty;

                    path = await _fileHelper.UploadFileAsync(file, "Analisis");
                    extension = Path.GetExtension(file.FileName);

                    var archivoAnalisis = new ArchivoAnalisis
                    {
                        //capacitacion = await _dataContext.capacitaciones.FindAsync(model.Id),
                        ArchivoPath = path,
                        user = await _userHelper.GetUserAsync(this.User.Identity.Name),
                        Fecha = DateTime.Now,
                        tamanio = file.Length,
                        TipoArchivo = extension,
                        //Property = await _dataContext.Properties.FindAsync(model.Id)
                    };
                    archivoAnalises.Add(archivoAnalisis);
                }

                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                
                var analisis = new Analisis
                {
                    Cedula = model.cedula,
                    Fecha = DateTime.Now,
                    Observaciones = model.Observaciones,
                    Placa = model.PlacaId,
                    tipoAnalisisId = model.TipoAnalisisId,
                    ArchivosAnalisis= archivoAnalises,
                    user = user
                };

                _dataContext.Analises.Add(analisis);
                await _dataContext.SaveChangesAsync();

                await _logRepository.SaveLogs("Crear", "Crea Análisis Id: " + analisis.Id.ToString(), "Análisis", User.Identity.Name);
                //enviar correo
                //var datos = await _datosRepository.GetDatosCliente(model.cedula);
                var datos = await _userHelper.GetUserByCedulaAsync(model.cedula);
                var tipoAnalisis = await _dataContext.TiposAnalisis.FindAsync(model.TipoAnalisisId);

                //var emails = "roy_chavez15@hotmail.com";
                var emails = user.Email;

                //TODO: cambiar direccion de correo
                _mailHelper.SendMailAttachment(emails, "Plataforma Clientes Nuevo Analisis Creado", 
                    $"<html xmlns='http://www.w3.org/1999/xhtml'>" +
                    $"<head>"+
                    $"<meta http-equiv=" + "Content-Type" + " content=" + "text/html; charset = UTF-8" + " />" +
                    $"<title>" +
                    $"</title>" +
                    $"</head>" +
                    $"<body>" +
                    $"<h1>Plataforma Clientes Nuevo Analisis</h1>" +
                    $"<table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' style='border-collapse:collapse; max-width:600px!important; width:100%; margin: auto'>" +                                                   
                    $"<tr><td style='font-weight:bold'>Placa</td><td>{model.PlacaId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Tipo</td><td>{tipoAnalisis.Tipo}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Observación</td><td>{model.Observaciones}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Creador por</td><td>{user.FullName}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Fecha</td><td>{analisis.Fecha}</td></tr></table></body></html>", model.Files);

                return Ok(model);
            }
            //Alert("No se pudo agregar el cliente, revise los datos", Enum.Enum.NotificationType.error);
            //return View(model);
            return BadRequest(model);
        }

        public async Task<IActionResult> Crear(AnalisisCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<ArchivoAnalisis> archivoAnalises = new List<ArchivoAnalisis>();
                if (model.Files != null)
                {
                    foreach (IFormFile file in model.Files)
                    {
                        //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", photo.FileName);
                        //var stream = new FileStream(path, FileMode.Create);
                        //photo.CopyToAsync(stream);
                        //product.Photos.Add(photo.FileName);
                        var path = string.Empty;
                        var extension = string.Empty;

                        path = await _fileHelper.UploadFileAsync(file, "Analisis");
                        extension = Path.GetExtension(file.FileName);

                        var archivoAnalisis = new ArchivoAnalisis
                        {
                            //capacitacion = await _dataContext.capacitaciones.FindAsync(model.Id),
                            ArchivoPath = path,
                            user = await _userHelper.GetUserAsync(this.User.Identity.Name),
                            Fecha = DateTime.Now,
                            tamanio = file.Length,
                            TipoArchivo = extension,
                            //Property = await _dataContext.Properties.FindAsync(model.Id)
                        };
                        archivoAnalises.Add(archivoAnalisis);
                    }
                }
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                var analisis = new Analisis
                {
                    Cedula = model.cedula,
                    Fecha = DateTime.Now,
                    Observaciones = model.Observaciones,
                    Placa = model.PlacaId,
                    tipoAnalisisId = model.TipoAnalisisId,
                    ArchivosAnalisis=archivoAnalises,
                    user = user
                };

                _dataContext.Analises.Add(analisis);
                await _dataContext.SaveChangesAsync();
                //enviar correo
                await _logRepository.SaveLogs("Crear", "Crea Análisis Id: " + analisis.Id.ToString(), "Análisis", User.Identity.Name);

                var tipoAnalisis = await _dataContext.TiposAnalisis.FindAsync(model.TipoAnalisisId);

                //var emails = "roy_chavez15@hotmail.com";
                var datos = await _userHelper.GetUserByCedulaAsync(model.cedula);
                var emails = user.Email+','+datos.Email;

                //TODO: cambiar direccion de correo



                _mailHelper.SendMailAttachment(emails, "Plataforma Clientes",
                    $"<html xmlns='http://www.w3.org/1999/xhtml'>" +
                    $"<head>" +
                    $"<meta http-equiv='Content-Type' content='text/html; charset = UTF-8'/>" +
                    $"<meta content = 'text/html; charset=utf-8'/>"+
                    $"<title>" +
                    $"</title>" +
                    $"</head>" +
                    $"<body>" +
                    //$"<h1>Plataforma Clientes Nuevo Análisis</h1>" +
                    $"<hr width=100% align='center' size=30 color='#002D73' style='margin:0px;padding:0px'>" +
                    $"<hr width=100% align='center' size=5 color='#F2AE0B' style='margin:0px;padding:0px'>" +
                    $"<br><br>" +
                    $"<p>Estimado Cliente"+
                    $"<p>Renting Pichincha comunica que se ha cargado en la plataforma de clientes un nuevo Análisis perteneciente al vehículo:"+
                    $"<table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' style='border-collapse:collapse; max-width:600px!important; width:100%; margin: auto'>" +
                    $"<tr><td style='font-weight:bold'>Placa</td><td>{model.PlacaId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Tipo</td><td>{tipoAnalisis.Tipo}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Observación</td><td>{model.Observaciones}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Creador por</td><td>{user.FullName}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Fecha</td><td>{analisis.Fecha}</td></tr></table>" +
                    $"<br><br>" +
                    $"<p>Para poder revisar la información de su plataforma ingrese a su cuenta con su usuario y contraseña."+
                    $"<div align='center'><a href='https://clientes.rentingpichincha.com'><img src='https://clientes.rentingpichincha.com/images/email1.png' align='center'></a></div>" +
                    $"<br><br>" +
                    $"<p>Es un placer estar en contacto.<br>" +
                    $"<p>Saludos cordiales<br>" +
                    $"<br><br>" +
                    $"<p>Consorcio Pichincha S.A CONDELPI<br>" +
                    $"<p>Av.González Suárez N32-346 y Coruña<br>" +
                    $"<p><img src='https://clientes.rentingpichincha.com/images/call.png' width=30px>Call Center: 1-800 RENTING(736846)<br>" +
                    $"<p><img src='https://clientes.rentingpichincha.com/images/email.png' width=25px>E-Mail: inforenting@condelpi.com<br>" +
                    $"<p><img src='https://clientes.rentingpichincha.com/images/whatsapp.jpg' width=25px>WhatsApp: 0997652137" +
                    $"<p>Quito-Ecuador" +
                    $"<br><br>" +
                    $"<img src='https://clientes.rentingpichincha.com/images/email2.png' width=200px>" +
                    $"<hr width=100% align='center' size=30 color='#002D73' style='margin:0px;padding:0px'>" +
                    $"<hr width=100% align='center' size=5 color='#F2AE0B' style='margin:0px;padding:0px'></body></html>"
                    , model.Files) ;

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
            var analisis= await _analisisRepository.GetAnalisisByIdAsync(id);
            
            if (analisis == null)
            {
                
                return NotFound();
            }
            await _logRepository.SaveLogs("Detalles", "Análisis Id: " + analisis.Id.ToString(), "Análisis", User.Identity.Name);
            return View(analisis);
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
            var analisis = await _analisisRepository.GetAnalisisByIdAsync(id);
            if (analisis == null)
            {
                
                return NotFound();
            }
            
            return View(analisis);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int id, Analisis analisis)
        {
            if (id != analisis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(analisis);
                    await _dataContext.SaveChangesAsync();

                    await _logRepository.SaveLogs("Editar", "Análisis Id: " + analisis.Id.ToString(), "Análisis", User.Identity.Name);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnalisisExists(analisis.Id))
                    {
                        
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Retorno), new { id = analisis.Cedula });
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

            var analisis = await _dataContext.Analises
                .Include(a => a.ArchivosAnalisis)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);

            var cedula = analisis.Cedula;

            await _logRepository.SaveLogs("Borrar", "Análisis Id: " + analisis.Id.ToString(), "Análisis", User.Identity.Name);

            _dataContext.Analises.Remove(analisis);
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

            var analisis = await _dataContext.Analises.FindAsync(id.Value);
            if (analisis == null)
            {
                return NotFound();
            }

            var model = new ArchivoAnalisisViewModel
            {
                Id = analisis.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(ArchivoAnalisisViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;
                var extension = string.Empty;

                if (model.Archivo != null)
                {
                    path = await _fileHelper.UploadFileAsync(model.Archivo,"Analisis");
                    extension= Path.GetExtension(model.Archivo.FileName);

                    var archivoAnalisis = new ArchivoAnalisis
                    {
                        analisis = await _dataContext.Analises.FindAsync(model.Id),
                        ArchivoPath = path,
                        user = await _userHelper.GetUserAsync(this.User.Identity.Name),
                        Fecha = DateTime.Now,
                        tamanio = model.Archivo.Length,
                        TipoArchivo = extension,
                        //Property = await _dataContext.Properties.FindAsync(model.Id)
                    };

                    

                    _dataContext.ArchivosAnalisis.Add(archivoAnalisis);
                    await _dataContext.SaveChangesAsync();
                    await _logRepository.SaveLogs("Guarda", "Archivo Análisis Id: " + archivoAnalisis.Id.ToString(), "Análisis", User.Identity.Name);

                    //return RedirectToAction($"{nameof(Retorno)}/{archivoAnalisis.analisis.Cedula}");
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

            var file = await _dataContext.ArchivosAnalisis
                .Include(pi => pi.analisis)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);
            if (file == null)
            {
                return NotFound();
            }

            await _logRepository.SaveLogs("Borrar", "Archivo Análisis Id: " + file.Id.ToString(), "Análisis", User.Identity.Name);

            _dataContext.ArchivosAnalisis.Remove(file);
            await _dataContext.SaveChangesAsync();

            
            //return RedirectToAction($"{nameof(Retorno)}/{file.analisis.Cedula}");
            return RedirectToAction($"{nameof(Edit)}/{file.analisis.Id}");
        }

        public async Task<IActionResult> DownloadFile(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var file = await _dataContext.ArchivosAnalisis
                .Include(pi => pi.analisis)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);

            if (file == null)
            {
                return NotFound();
            }

            await _logRepository.SaveLogs("Descargar", "Archivo Análisis Id: " + file.Id.ToString(), "Análisis", User.Identity.Name);
            //return _fileHelper.GetFileAsStream(file.ArchivoPath.Substring(1)) ?? (IActionResult)NotFound();
            return _fileHelper.GetFile(file.ArchivoPath.Substring(1)) ?? (IActionResult)NotFound();
        }

    }
}
