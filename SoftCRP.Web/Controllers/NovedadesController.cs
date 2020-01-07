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
    public class NovedadesController : BaseController
    {
        private readonly IUserHelper _userHelper;
        private readonly IDatosRepository _datosRepository;
        
        private readonly ICombosHelper _combosHelper;
        private readonly IFileHelper _fileHelper;
        private readonly IMailHelper _mailHelper;
        private readonly ILogRepository _logRepository;
        private readonly INovedadesRepository _novedadesRepository;
        private readonly DataContext _dataContext;

        public NovedadesController(
            IUserHelper userHelper,
            IDatosRepository datosRepository,
            INovedadesRepository novedadesRepository,
            ICombosHelper combosHelper,
            IFileHelper fileHelper,
            IMailHelper mailHelper,
            ILogRepository logRepository,
            DataContext dataContext
            )
        {
            _userHelper = userHelper;
            _datosRepository = datosRepository;            
            _combosHelper = combosHelper;
            _fileHelper = fileHelper;
            _mailHelper = mailHelper;
            _logRepository = logRepository;
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
            await _logRepository.SaveLogs("Get", "Obtiene lista de Novedades", "Novedades", User.Identity.Name);
            return View(model);
        }
        public async Task<IActionResult> IndexLista()
        {

            //List<User> clientes = new List<User>();
            var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            if (user != null)
            {
                var clientes = await _userHelper.GetListUsersInRole("Cliente");
                await _logRepository.SaveLogs("Get", "Obtiene lista de Clientes", "Novedades", User.Identity.Name);
                return View(clientes);
            }

            return View();
        }
        public async Task<IActionResult> Retorno(string id)
        {

            NovedadesViewModel model = new NovedadesViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = await _novedadesRepository.GetNovedadAsync(id);
                ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(id);
            }
            await _logRepository.SaveLogs("Get", "Obtiene lista de Novedades", "Novedades", User.Identity.Name);
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
                novedadesCreateViewModel.Placas = await _combosHelper.GetComboPlacasSN(id);
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
                List<ArchivoNovedades> archivoNovedadesList = new List<ArchivoNovedades>();

                foreach (IFormFile file in model.Files)
                {
                    var path = string.Empty;
                    var extension = string.Empty;

                    path = await _fileHelper.UploadFileAsync(file, "Novedades");
                    extension = Path.GetExtension(file.FileName);

                    var archivoNovedades = new ArchivoNovedades
                    {
                        //capacitacion = await _dataContext.capacitaciones.FindAsync(model.Id),
                        ArchivoPath = path,
                        user = await _userHelper.GetUserAsync(this.User.Identity.Name),
                        Fecha = DateTime.Now,
                        tamanio = file.Length,
                        TipoArchivo = extension,
                        //Property = await _dataContext.Properties.FindAsync(model.Id)
                    };
                    archivoNovedadesList.Add(archivoNovedades);
                }
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                var novedad = new Novedad
                {
                    Cedula = model.cedula,
                    EstadoSolucion = "PENDIENTE",
                    Fecha = DateTime.Now,
                    Observaciones = model.Observaciones,
                    Placa = model.PlacaId,
                    Motivo = model.MotivoId,
                    SubMotivo=model.SubMotivoId,
                    ViaIngreso=model.ViaIngresoId,
                    archivoNovedades=archivoNovedadesList,
                    user = user
                };

                _dataContext.novedades.Add(novedad);
                await _dataContext.SaveChangesAsync();

                await _logRepository.SaveLogs("Crear", "Crea Novedades id: " + novedad.Id.ToString(), "Novedades", User.Identity.Name);

                var lognovedad = new LogNovedad
                {
                    Estado = "PENDIENTE",
                    Fecha = DateTime.Now,
                    Usuario = user,
                    novedad = novedad,
                    Observaciones = model.Observaciones,
                };
                _dataContext.logNovedades.Add(lognovedad);
                await _dataContext.SaveChangesAsync();
                //enviar correo
                //var datos = await _datosRepository.GetDatosCliente(model.cedula);
                //var tipoAnalisis = await _dataContext.TiposAnalisis.FindAsync(model.TipoAnalisisId);

                //var emails = "roy_chavez15@hotmail.com";
                var datos = await _userHelper.GetUserByCedulaAsync(model.cedula);
                var emails = user.Email + ',' + datos.Email;

                //TODO: cambiar direccion de correo
                _mailHelper.SendMailAttachment(emails, "SoftCRP Nueva Novedad Creado",
                    $"<html xmlns='http://www.w3.org/1999/xhtml'>" +
                    $"<head>" +
                    $"<meta http-equiv=" + "Content-Type" + " content=" + "text/html; charset = UTF-8" + " />" +
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
                    $"<tr><td style='font-weight:bold'>Fecha</td><td>{novedad.Fecha}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Creador por</td><td>{user.FullName}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Observación</td><td>{model.Observaciones}</td></tr></table></body></html>", model.Files);

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
                List<ArchivoNovedades> archivoNovedadesList = new List<ArchivoNovedades>();
                if (model.Files != null)
                {
                    foreach (IFormFile file in model.Files)
                    {
                        var path = string.Empty;
                        var extension = string.Empty;

                        path = await _fileHelper.UploadFileAsync(file, "Novedades");
                        extension = Path.GetExtension(file.FileName);

                        var archivoNovedades = new ArchivoNovedades
                        {
                            //capacitacion = await _dataContext.capacitaciones.FindAsync(model.Id),
                            ArchivoPath = path,
                            user = await _userHelper.GetUserAsync(this.User.Identity.Name),
                            Fecha = DateTime.Now,
                            tamanio = file.Length,
                            TipoArchivo = extension,
                            //Property = await _dataContext.Properties.FindAsync(model.Id)
                        };
                        archivoNovedadesList.Add(archivoNovedades);
                    }
                }

                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                var novedad = new Novedad
                {
                    Cedula = model.cedula,
                    EstadoSolucion="PENDIENTE",
                    Fecha = DateTime.Now,
                    Observaciones = model.Observaciones,
                    Placa = model.PlacaId,
                    Motivo = model.MotivoId,
                    SubMotivo = model.SubMotivoId,
                    ViaIngreso = model.ViaIngresoId,
                    archivoNovedades=archivoNovedadesList,                    
                    user = user
                };

                


                _dataContext.novedades.Add(novedad);
                await _dataContext.SaveChangesAsync();
                await _logRepository.SaveLogs("Crear", "Crea Novedades id: " + novedad.Id.ToString(), "Novedades", User.Identity.Name);


                //enviar correo
                //var datos = await _datosRepository.GetDatosCliente(model.cedula);
                //var tipoAnalisis = await _dataContext.TiposAnalisis.FindAsync(model.TipoAnalisisId);

                //var emails = "roy_chavez15@hotmail.com";
                var datos = await _userHelper.GetUserByCedulaAsync(model.cedula);
                var emails = user.Email + ',' + datos.Email;

                    var idMotivo = await _datosRepository.GetTipoIncidenciaIdAsync(model.MotivoId);
                    var idSubmotivo = await _datosRepository.GetSubMotivosIncidenciaIdAsync(model.SubMotivoId);
                    var incidencia = new IncidenciaCreateViewModel
                    {
                        Placa = novedad.Placa,
                        submotivo = idSubmotivo.Id,
                        motivo = idMotivo.Id.ToString(),
                        //usuario = user.UserName,
                        usuario = "RENTING",
                        observacion = novedad.Observaciones,
                        usuario_solucion = idSubmotivo.Usuario_asesor
                    };
                    var registroincidencia = await _datosRepository.IngresoIncidencia(incidencia);
                    if (idSubmotivo.Correo_solucionadores != "")
                    {
                        emails = emails + ',' + idSubmotivo.Correo_solucionadores;
                    }
                
                ///
                var lognovedad = new LogNovedad
                {
                    Estado="PENDIENTE",
                    Fecha=DateTime.Now,
                    Usuario=user,
                    novedad=novedad,
                    Observaciones= model.Observaciones,
                };
                _dataContext.logNovedades.Add(lognovedad);
                await _dataContext.SaveChangesAsync();


                //TODO: cambiar direccion de correo
                _mailHelper.SendMailAttachment(emails, "Plataforma Clientes",
                    $"<html xmlns='http://www.w3.org/1999/xhtml'>" +
                    $"<head>" +
                    $"<meta http-equiv=" + "Content-Type" + " content=" + "text/html; charset = UTF-8" + " />" +
                    $"<title>" +
                    $"</title>" +
                    $"</head>" +
                    $"<body>" +
                    //$"<h1>SoftCRP Nuevo Novedad</h1>" +
                    $"<hr width=100% align='center' size=30 color='#002D73' style='margin:0px;padding:0px'>" +
                    $"<hr width=100% align='center' size=5 color='#F2AE0B' style='margin:0px;padding:0px'>" +
                    $"<br><br>" +
                    $"<p>Estimado Cliente" +
                    $"<p>Renting Pichincha comunica que se ha cargado en la plataforma de clientes una nueva Novedad perteneciente al vehículo:" +
                    $"<table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' style='border-collapse:collapse; max-width:600px!important; width:100%; margin: auto'>" +
                    $"<tr><td style='font-weight:bold'>Placa</td><td>{model.PlacaId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Motivo</td><td>{model.MotivoId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>SubMotivo</td><td>{model.SubMotivoId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Vía Ingreso</td><td>{model.ViaIngresoId}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Fecha</td><td>{novedad.Fecha}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>SLA</td><td>{idSubmotivo.Dias_sla} Días</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Estado</td><td>PENDIENTE</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Creador por</td><td>{user.FullName}</td></tr>" +
                    $"<tr><td style='font-weight:bold'>Observación</td><td>{model.Observaciones}</td></tr></table>"+
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
            var novedad = await _novedadesRepository.GetNovedadByIdAsync(id);

            if (novedad == null)
            {
                return NotFound();
            }
            await _logRepository.SaveLogs("Detalles", "Detalles Novedades id: " + novedad.Id.ToString(), "Novedades", User.Identity.Name);
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

                    var lognovedad = new LogNovedad
                    {
                        novedad = novedad,
                        Estado=model.EstadoId,
                        Usuario=novedad.userSolucion,
                        Fecha=DateTime.Now,
                        Observaciones = model.Solucion,
                    };
                    _dataContext.logNovedades.Add(lognovedad);
                    await _dataContext.SaveChangesAsync();

                    await _logRepository.SaveLogs("Editar", "Edita Novedades id: " + novedad.Id.ToString()+"-"+novedad.EstadoSolucion, "Novedades", User.Identity.Name);
                    //////
                    var datos = await _userHelper.GetUserByCedulaAsync(novedad.Cedula);
                    var emails = novedad.userSolucion.Email + ',' + datos.Email;

                    //TODO: cambiar direccion de correo
                    _mailHelper.SendMail(emails, "Plataforma Clientes -- Actualización Novedad",
                        $"<html xmlns='http://www.w3.org/1999/xhtml'>" +
                        $"<head>" +
                        $"<title>" +
                        $"</title>" +
                        $"</head>" +
                        $"<body>" +
                    //$"<h1>Plataforma Clientes Actualización Novedad</h1>" +
                        $"<hr width=100% align='center' size=30 color='#002D73' style='margin:0px;padding:0px'>" +
                        $"<hr width=100% align='center' size=5 color='#F2AE0B' style='margin:0px;padding:0px'>" +
                        $"<br><br>" +
                        $"<p>Estimado Cliente" +
                        $"<p>Renting Pichincha comunica que se ha actualizado en la plataforma de clientes una Novedad perteneciente al vehículo:" +
                        $"<table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' style='border-collapse:collapse; max-width:600px!important; width:100%; margin: auto'>" +
                        $"<tr><td style='font-weight:bold'>Placa</td><td>{novedad.Placa}</td></tr>" +
                        $"<tr><td style='font-weight:bold'>Motivo</td><td>{novedad.Motivo}</td></tr>" +
                        $"<tr><td style='font-weight:bold'>SubMotivo</td><td>{novedad.SubMotivo}</td></tr>" +
                        $"<tr><td style='font-weight:bold'>Vía Ingreso</td><td>{novedad.ViaIngreso}</td></tr>" +
                        $"<tr><td style='font-weight:bold'>Estado</td><td>{model.EstadoId}</td></tr>" +
                        $"<tr><td style='font-weight:bold'>Solución</td><td>{model.Solucion}</td></tr>" +
                        $"<tr><td style='font-weight:bold'>Actualizado por</td><td>{novedad.userSolucion.FullName}</td></tr>" +
                        $"<tr><td style='font-weight:bold'>Fecha</td><td>{novedad.FechaSolucion}</td></tr></table>"+
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
                        );
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
                .Include(l=>l.logNovedades)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);

            var cedula = novedades.Cedula;

            await _logRepository.SaveLogs("Borrar", "Borrar Novedades id: " + novedades.Id.ToString(), "Novedades", User.Identity.Name);

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

                    await _logRepository.SaveLogs("Guarda", "Archivo Novedades id: " + archivoNovedades.Id.ToString(), "Novedades", User.Identity.Name);
                    //return RedirectToAction($"{nameof(Retorno)}/{archivoNovedades.novedad.Cedula}");
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

            var file = await _dataContext.archivoNovedades
                .Include(pi => pi.novedad)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);
            if (file == null)
            {
                return NotFound();
            }

            await _logRepository.SaveLogs("Borrar", "Borrar Archivo Novedades id: " + file.Id.ToString(), "Novedades", User.Identity.Name);
            _dataContext.archivoNovedades.Remove(file);
            await _dataContext.SaveChangesAsync();

            //return RedirectToAction($"{nameof(Retorno)}/{file.novedad.Cedula}");
            return RedirectToAction($"{nameof(Edit)}/{file.novedad.Id}");
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
            await _logRepository.SaveLogs("Descargar", "Descargar Novedades id: " + file.Id.ToString(), "Novedades", User.Identity.Name);
            //return _fileHelper.GetFileAsStream(file.ArchivoPath.Substring(1)) ?? (IActionResult)NotFound();
            return _fileHelper.GetFile(file.ArchivoPath.Substring(1)) ?? (IActionResult)NotFound();
        }
    }
}