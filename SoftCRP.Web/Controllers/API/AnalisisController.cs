using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftCRP.Common.Models;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;

namespace SoftCRP.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AnalisisController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IFileHelper _fileHelper;
        private readonly IUserHelper _userHelper;
        private readonly ILogRepository _logRepository;
        private readonly IMailHelper _mailHelper;

        public AnalisisController(
            DataContext dataContext,
            IFileHelper fileHelper,
            IUserHelper userHelper,
            IMailHelper mailHelper,
            ILogRepository logRepository)
        {
            _dataContext = dataContext;
            _fileHelper = fileHelper;
            _userHelper = userHelper;
            _logRepository = logRepository;
            _mailHelper = mailHelper;
        }

        [HttpGet]
        [Route("TiposAnalisisAsync")]
        public async Task<IEnumerable<TipoAnalisis>> GetTiposAnalisisAsync()
        {
            return await _dataContext.TiposAnalisis
                .Where(e => e.isActive == true)
                .OrderBy(i=>i.Id)
                .ToListAsync();
        }

        [HttpPost]
        [Route("CreateAnalisis")]
        public async Task<IActionResult> CreateAsyncAnalisis([FromForm] AnalisisRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            List<ArchivoAnalisis> archivoAnalises = new List<ArchivoAnalisis>();
            var user = await _userHelper.GetUserByIdAsync(model.JsonAnalisis.UserId.ToString());
            if(user==null)
            {
                return BadRequest();
            }

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
                    user = user,
                    Fecha = DateTime.Now,
                    tamanio = file.Length,
                    TipoArchivo = extension,
                    //Property = await _dataContext.Properties.FindAsync(model.Id)
                };
                archivoAnalises.Add(archivoAnalisis);
            }

            
            var analisis = new Analisis
            {
                Cedula = model.JsonAnalisis.cedula,
                Fecha = DateTime.Now,
                Observaciones = model.JsonAnalisis.Observaciones,
                Placa = model.JsonAnalisis.PlacaId,
                tipoAnalisisId = model.JsonAnalisis.TipoAnalisisId,
                ArchivosAnalisis = archivoAnalises,
                user = user
            };

            _dataContext.Analises.Add(analisis);
            await _dataContext.SaveChangesAsync();
            //enviar correo
            await _logRepository.SaveLogs("API Crear", "Crea Análisis Id: " + analisis.Id.ToString(), "Análisis", user.UserName);

            var tipoAnalisis = await _dataContext.TiposAnalisis.FindAsync(model.JsonAnalisis.TipoAnalisisId);

            //var emails = "roy_chavez15@hotmail.com";
            var datos = await _userHelper.GetUserByCedulaAsync(model.JsonAnalisis.cedula);
            var emails = user.Email.Trim() + ',' + datos.Email.Trim();

            //TODO: cambiar direccion de correo



            _mailHelper.SendMailAttachment(emails, "Plataforma Clientes",
                $"<html xmlns='http://www.w3.org/1999/xhtml'>" +
                $"<head>" +
                $"<meta http-equiv='Content-Type' content='text/html; charset = UTF-8'/>" +
                $"<meta content = 'text/html; charset=utf-8'/>" +
                $"<title>" +
                $"</title>" +
                $"</head>" +
                $"<body>" +
                //$"<h1>Plataforma Clientes Nuevo Análisis</h1>" +
                $"<hr width=100% align='center' size=30 color='#002D73' style='margin:0px;padding:0px'>" +
                $"<hr width=100% align='center' size=5 color='#F2AE0B' style='margin:0px;padding:0px'>" +
                $"<br><br>" +
                $"<p>Estimado Cliente" +
                $"<p>Renting Pichincha comunica que se ha cargado en la plataforma de clientes un nuevo Análisis perteneciente al vehículo:" +
                $"<table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' style='border-collapse:collapse; max-width:600px!important; width:100%; margin: auto'>" +
                $"<tr><td style='font-weight:bold'>Placa</td><td>{model.JsonAnalisis.PlacaId}</td></tr>" +
                $"<tr><td style='font-weight:bold'>Tipo</td><td>{tipoAnalisis.Tipo}</td></tr>" +
                $"<tr><td style='font-weight:bold'>Observación</td><td>{model.JsonAnalisis.Observaciones}</td></tr>" +
                $"<tr><td style='font-weight:bold'>Creador por</td><td>{user.FullName}</td></tr>" +
                $"<tr><td style='font-weight:bold'>Fecha</td><td>{analisis.Fecha}</td></tr></table>" +
                $"<br><br>" +
                $"<p>Para poder revisar la información de su plataforma ingrese a su cuenta con su usuario y contraseña." +
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
                , model.Files);

            return Ok();
        }
    }
}