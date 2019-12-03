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
    public class CapacitacionesController : BaseController
    {
        private readonly IUserHelper _userHelper;
        private readonly IDatosRepository _datosRepository;
        private readonly ICapacitacionesRepository _capacitacionesRepository;
        private readonly ICombosHelper _combosHelper;
        private readonly IFileHelper _fileHelper;
        private readonly IMailHelper _mailHelper;
        private readonly DataContext _dataContext;

        public CapacitacionesController(
            IUserHelper userHelper,
            IDatosRepository datosRepository,
            ICapacitacionesRepository capacitacionesRepository,
            ICombosHelper combosHelper,
            IFileHelper fileHelper,
            IMailHelper mailHelper,
            DataContext dataContext)
        {
            _userHelper = userHelper;
            _datosRepository = datosRepository;
            _capacitacionesRepository = capacitacionesRepository;
            _combosHelper = combosHelper;
            _fileHelper = fileHelper;
            _mailHelper = mailHelper;
            _dataContext = dataContext;
        }
        public async Task<IActionResult> Index()
        {

            CapacitacionesViewModel model = new CapacitacionesViewModel();

            var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            if (user != null)
            {
                //if (this.User.IsInRole("Cliente"))
                //{
                //    model = await _capacitacionesRepository.GetAnalisis(user.Cedula);
                //}
                //else
                //{

                //}
                model = await _capacitacionesRepository.GetCapacitacionesAsync();
            }

            //ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(user.Cedula);

            return View(model);
        }
        //public async Task<IActionResult> Retorno(string id)
        //{

        //    AnalisisViewModel model = new AnalisisViewModel();
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        model = await _analisisRepository.GetAnalisis(id);
        //        ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(id);
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Index(string buscarcliente)
        //{

        //    AnalisisViewModel model = new AnalisisViewModel();
        //    if (!string.IsNullOrEmpty(buscarcliente))
        //    {
        //        var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
        //        if (user != null)
        //        {
        //            model = await _analisisRepository.GetAnalisis(buscarcliente);
        //        }
        //        ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(buscarcliente);
        //    }
        //    return View(model);
        //}

        [HttpGet]
        [Authorize(Roles = "Admin,Renting")]
        // GET: Clientes/Create
        public async Task<IActionResult> Create()
        {
            CapacitacionesCreateViewModel capacitacionesCreateViewModel = new CapacitacionesCreateViewModel();
            //if (!string.IsNullOrEmpty(id))
            //{
            //    analisisCreateViewModel.cedula = id;
            //    analisisCreateViewModel.Placas = await _combosHelper.GetComboPlacas(id);
            //    analisisCreateViewModel.AnalisisTypes = _combosHelper.GetComboTipoAnalisis();
            //}
            //else
            //{

            //}
            capacitacionesCreateViewModel.CapacitacionesTypes = _combosHelper.GetComboTipoCapacitaciones();

            return View(capacitacionesCreateViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CapacitacionesCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var files = HttpContext.Request.Form.Files;
                List<ArchivoCapacitaciones> archivoCapacitacionesList = new List<ArchivoCapacitaciones>();

                foreach (IFormFile file in model.Files)
                {
                    //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", photo.FileName);
                    //var stream = new FileStream(path, FileMode.Create);
                    //photo.CopyToAsync(stream);
                    //product.Photos.Add(photo.FileName);
                    var path = string.Empty;
                    var extension = string.Empty;

                    path = await _fileHelper.UploadFileAsync(file, "Capacitaciones");
                    extension = Path.GetExtension(file.FileName);

                    var archivoCapacitaciones = new ArchivoCapacitaciones
                    {
                        //capacitacion = await _dataContext.capacitaciones.FindAsync(model.Id),
                        ArchivoPath = path,
                        user = await _userHelper.GetUserAsync(this.User.Identity.Name),
                        Fecha = DateTime.Now,
                        tamanio = file.Length,
                        TipoArchivo = extension,
                        //Property = await _dataContext.Properties.FindAsync(model.Id)
                    };
                    archivoCapacitacionesList.Add(archivoCapacitaciones);
                }



                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);



                var capacitacion = new Capacitacion
                {
                    Fecha = DateTime.Now,
                    Test = model.Test,
                    tipoCapacitacionId=model.tipoCapacitacionId,
                    archivoCapacitaciones=archivoCapacitacionesList,
                    user = user
                };

                _dataContext.capacitaciones.Add(capacitacion);
                await _dataContext.SaveChangesAsync();
                //enviar correo
                //var datos = await _datosRepository.GetDatosCliente(model.cedula);
                var tipoCapacitacion = await _dataContext.tipoCapacitaciones.FindAsync(model.tipoCapacitacionId);

                //var emails = "roy_chavez15@hotmail.com";
                //var datos = await _userHelper.GetUserByCedulaAsync(model.cedula);
                var emails = user.Email;

                //TODO: cambiar direccion de correo
                _mailHelper.SendMailAttachment(emails, "Plataforma Clientes",
                    $"<html xmlns='http://www.w3.org/1999/xhtml'>" +
                    $"<head>" +
                    $"<title>" +
                    $"</title>" +
                    $"</head>" +
                    $"<body>" +
                    //$"<h1>Plataforma Clientes Nueva Capacitación</h1>" +
                    $"<hr width=100% align='center' size=30 color='#002D73' style='margin:0px;padding:0px'>" +
                    $"<hr width=100% align='center' size=5 color='#F2AE0B' style='margin:0px;padding:0px'>" +
                    $"<br><br>" +
                    $"<p>Estimado Cliente" +
                    $"<p>Renting Pichincha comunica que se ha cargado en la plataforma de clientes una nueva Capacitación:" +

                    $"<table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' style='border-collapse:collapse; max-width:600px!important; width:100%; margin: auto'>" +                    
                    $"<tr><td style='font-weight:bold'>Tipo</td><td>{tipoCapacitacion.Tipo}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Observación</td><td>{model.Test}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Creador por</td><td>{user.FullName}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Fecha</td><td>{capacitacion.Fecha}</td></tr></table>"+
                    $"<br><br>" +
                    $"<p>Para poder revisar la información de su plataforma ingrese a su cuenta con su usuario y contraseña." +
                    $"<div align='center'><a href='http://181.112.216.3/softcrpweb'><img src='http://181.112.216.3/softcrpweb/images/email1.png' align='center'></a></div>" +
                    $"<br><br>" +
                    $"<p>Es un placer estar en contacto.<br>" +
                    $"<p>Saludos cordiales<br>" +
                    $"<br><br>" +
                    $"<p>Consorcio Pichincha S.A CONDELPI<br>" +
                    $"<p>Av.González Suárez N32-346 y Coruña<br>" +
                    $"<p>Call Center: 1-800 RENTING(736846)<br>" +
                    $"<p>E-Mail: inforenting@condelpi.com<br>" +
                    $"<p>Quito-Ecuador" +
                    $"<br><br>" +
                    $"<img src='http://181.112.216.3/softcrpweb/images/email2.png' width=200px>" +
                    $"<hr width=100% align='center' size=30 color='#002D73' style='margin:0px;padding:0px'>" +
                    $"<hr width=100% align='center' size=5 color='#F2AE0B' style='margin:0px;padding:0px'></body></html>"
                    , model.Files);

                return Ok(model);
            }
            //Alert("No se pudo agregar el cliente, revise los datos", Enum.Enum.NotificationType.error);
            //return View(model);
            return BadRequest(model);
        }

        public async Task<IActionResult> Crear(CapacitacionesCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<ArchivoCapacitaciones> archivoCapacitacionesList = new List<ArchivoCapacitaciones>();
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

                        path = await _fileHelper.UploadFileAsync(file, "Capacitaciones");
                        extension = Path.GetExtension(file.FileName);

                        var archivoCapacitaciones = new ArchivoCapacitaciones
                        {
                            //capacitacion = await _dataContext.capacitaciones.FindAsync(model.Id),
                            ArchivoPath = path,
                            user = await _userHelper.GetUserAsync(this.User.Identity.Name),
                            Fecha = DateTime.Now,
                            tamanio = file.Length,
                            TipoArchivo = extension,
                            //Property = await _dataContext.Properties.FindAsync(model.Id)
                        };
                        archivoCapacitacionesList.Add(archivoCapacitaciones);
                    }
                }

                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                var capacitacion = new Capacitacion
                {
                    Fecha = DateTime.Now,
                    Test = model.Test,
                    tipoCapacitacionId = model.tipoCapacitacionId,
                    archivoCapacitaciones = archivoCapacitacionesList,
                    user = user
                };

                _dataContext.capacitaciones.Add(capacitacion);
                await _dataContext.SaveChangesAsync();
                //enviar correo
                //var datos = await _datosRepository.GetDatosCliente(model.cedula);
                var tipoCapacitacion = await _dataContext.tipoCapacitaciones.FindAsync(model.tipoCapacitacionId);

                //var emails = "roy_chavez15@hotmail.com";
                //var datos = await _userHelper.GetUserByCedulaAsync(model.cedula);
                var emails = user.Email;

                //TODO: cambiar direccion de correo
                _mailHelper.SendMailAttachment(emails, "Plataforma Clientes",
                    $"<html xmlns='http://www.w3.org/1999/xhtml'>" +
                    $"<head>" +
                    $"<title>" +
                    $"</title>" +
                    $"</head>" +
                    $"<body>" +
                    //$"<h1>Plataforma Clientes Nueva Capacitación</h1>" +
                    $"<hr width=100% align='center' size=30 color='#002D73' style='margin:0px;padding:0px'>" +
                    $"<hr width=100% align='center' size=5 color='#F2AE0B' style='margin:0px;padding:0px'>" +
                    $"<br><br>" +

                    $"<table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' style='border-collapse:collapse; max-width:600px!important; width:100%; margin: auto'>" +
                    $"<tr><td style='font-weight:bold'>Tipo</td><td>{tipoCapacitacion.Tipo}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Observación</td><td>{model.Test}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Creador por</td><td>{user.FullName}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Fecha</td><td>{capacitacion.Fecha}</td></tr></table>"+
                    $"<br><br>" +
                    $"<p>Para poder revisar la información de su plataforma ingrese a su cuenta con su usuario y contraseña." +
                    $"<div align='center'><a href='http://181.112.216.3/softcrpweb'><img src='http://181.112.216.3/softcrpweb/images/email1.png' align='center'></a></div>" +
                    $"<br><br>" +
                    $"<p>Es un placer estar en contacto.<br>" +
                    $"<p>Saludos cordiales<br>" +
                    $"<br><br>" +
                    $"<p>Consorcio Pichincha S.A CONDELPI<br>" +
                    $"<p>Av.González Suárez N32-346 y Coruña<br>" +
                    $"<p><img src='http://181.112.216.3/softcrpweb/images/call.png' width=30px>Call Center: 1-800 RENTING(736846)<br>" +
                    $"<p><img src='http://181.112.216.3/softcrpweb/images/email.png' width=25px>E-Mail: inforenting@condelpi.com<br>" +
                    $"<p><img src='http://181.112.216.3/softcrpweb/images/whatsapp.jpg' width=25px>WhatsApp: 0997652137" +
                    $"<p>Quito-Ecuador" +
                    $"<br><br>" +
                    $"<img src='http://181.112.216.3/softcrpweb/images/email2.png' width=200px>" +
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
            var capacitacion = await _capacitacionesRepository.GetCapacitacionByIdAsync(id);

            if (capacitacion == null)
            {
                return NotFound();
            }

            return View(capacitacion);
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
            var analisis = await _capacitacionesRepository.GetCapacitacionByIdAsync(id);
            if (analisis == null)
            {
                return NotFound();
            }
            return View(analisis);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int id, Capacitacion capacitacion)
        {
            if (id != capacitacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(capacitacion);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnalisisExists(capacitacion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
                
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

            var capacitacion = await _dataContext.capacitaciones
                .Include(a => a.archivoCapacitaciones)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);

           
            _dataContext.capacitaciones.Remove(capacitacion);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool AnalisisExists(int id)
        {
            return _dataContext.Analises.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Admin,Renting")]
        public async Task<IActionResult> AddFile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capacitacion = await _dataContext.capacitaciones.FindAsync(id.Value);
            if (capacitacion == null)
            {
                return NotFound();
            }

            var model = new ArchivoCapacitacionesViewModel
            {
                Id = capacitacion.Id
            };

            return View(model);
        }
        public async Task<IActionResult> AddFile2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capacitacion = await _dataContext.capacitaciones.FindAsync(id.Value);
            if (capacitacion == null)
            {
                return NotFound();
            }

            var model = new ArchivoCapacitacionesViewModel
            {
                Id = capacitacion.Id
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddFile(ArchivoCapacitacionesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;
                var extension = string.Empty;

                if (model.Archivo != null)
                {
                    path = await _fileHelper.UploadFileAsync(model.Archivo, "Capacitaciones");
                    extension = Path.GetExtension(model.Archivo.FileName);

                    var archivoCapacitaciones = new ArchivoCapacitaciones
                    {
                        capacitacion = await _dataContext.capacitaciones.FindAsync(model.Id),
                        ArchivoPath = path,
                        user = await _userHelper.GetUserAsync(this.User.Identity.Name),
                        Fecha = DateTime.Now,
                        tamanio = model.Archivo.Length,
                        TipoArchivo = extension,
                        //Property = await _dataContext.Properties.FindAsync(model.Id)
                    };

                    _dataContext.archivoCapacitaciones.Add(archivoCapacitaciones);
                    await _dataContext.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                    return RedirectToAction(nameof(Edit), new { id = model.Id });
                }

            }

            return View(model);
        }


        public async Task<IActionResult> AddFile1(ArchivoCapacitacionesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;
                var extension = string.Empty;

                if (model.Archivo != null)
                {
                    path = await _fileHelper.UploadFileAsync(model.Archivo, "Capacitaciones");
                    extension = Path.GetExtension(model.Archivo.FileName);
                }

                var archivoCapacitaciones = new ArchivoCapacitaciones
                {
                    capacitacion = await _dataContext.capacitaciones.FindAsync(model.Id),
                    ArchivoPath = path,
                    user = await _userHelper.GetUserAsync(this.User.Identity.Name),
                    Fecha = DateTime.Now,
                    tamanio = model.Archivo.Length,
                    TipoArchivo = extension,
                    //Property = await _dataContext.Properties.FindAsync(model.Id)
                };

                _dataContext.archivoCapacitaciones.Add(archivoCapacitaciones);
                await _dataContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
                //return RedirectToAction(nameof(AddFile), new { id = model.Id });
                
            }

            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile3(ArchivoCapacitacionesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;
                var extension = string.Empty;

                if (model.Archivo != null)
                {
                    path = await _fileHelper.UploadFileAsync(model.Archivo, "Capacitaciones");
                    extension = Path.GetExtension(model.Archivo.FileName);
                }

                var archivoCapacitaciones = new ArchivoCapacitaciones
                {
                    capacitacion = await _dataContext.capacitaciones.FindAsync(model.Id),
                    ArchivoPath = path,
                    user = await _userHelper.GetUserAsync(this.User.Identity.Name),
                    Fecha = DateTime.Now,
                    tamanio = model.Archivo.Length,
                    TipoArchivo = extension,
                    //Property = await _dataContext.Properties.FindAsync(model.Id)
                };

                _dataContext.archivoCapacitaciones.Add(archivoCapacitaciones);
                await _dataContext.SaveChangesAsync();

                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Edit), new { id = model.Id });
                
            }
            return View(model);
            //return Json(new { state = 0, message = string.Empty });
        }

        [Authorize(Roles = "Admin,Renting")]
        public async Task<IActionResult> DeleteFile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _dataContext.archivoCapacitaciones
                .Include(pi => pi.capacitacion)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);
            if (file == null)
            {
                return NotFound();
            }

            _dataContext.archivoCapacitaciones.Remove(file);
            await _dataContext.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction($"{nameof(Edit)}/{file.capacitacion.Id}");
        }

        public async Task<IActionResult> DownloadFile(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var file = await _dataContext.archivoCapacitaciones
                .Include(pi => pi.capacitacion)
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