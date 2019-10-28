﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class AnalisisController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IDatosRepository _datosRepository;
        private readonly IAnalisisRepository _analisisRepository;
        private readonly ICombosHelper _combosHelper;
        private readonly IFileHelper _fileHelper;
        private readonly DataContext _dataContext;

        public AnalisisController(
            IUserHelper userHelper,
            IDatosRepository datosRepository,
            IAnalisisRepository analisisRepository,
            ICombosHelper combosHelper,     
            IFileHelper fileHelper,
            DataContext dataContext
            )
        {
            _userHelper = userHelper;
            _datosRepository = datosRepository;
            _analisisRepository = analisisRepository;
            _combosHelper = combosHelper;
            _fileHelper = fileHelper;
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

            return View(model);
        }

        public async Task<IActionResult> Retorno(string id)
        {

            AnalisisViewModel model = new AnalisisViewModel();

            model = await _analisisRepository.GetAnalisis(id);
            ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string buscarcliente)
        {

            AnalisisViewModel model = new AnalisisViewModel();
            var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            if (user != null)
            {
                model = await _analisisRepository.GetAnalisis(buscarcliente);
            }
            ViewBag.ClienteViewModel = await _datosRepository.GetDatosCliente(buscarcliente);

            return View(model);
        }

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnalisisCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var analisis = new Analisis
                {
                    Cedula = model.cedula,
                    Fecha = DateTime.Now,
                    Observaciones = model.Observaciones,
                    Placa = model.PlacaId,
                    tipoAnalisisId = model.TipoAnalisisId,
                    user = await _userHelper.GetUserAsync(this.User.Identity.Name)
                };

                _dataContext.Analises.Add(analisis);
                await _dataContext.SaveChangesAsync();
                //Alert("Cliente Agregado con EXITO", Enum.Enum.NotificationType.success);

                return RedirectToAction(nameof(Retorno), new { id= model.cedula});
            }
            //Alert("No se pudo agregar el cliente, revise los datos", Enum.Enum.NotificationType.error);
            return View(model);
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

            return View(analisis);
        }
        // GET: Clientes/Edit/5
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
        [ValidateAntiForgeryToken]
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


        public async Task<IActionResult> Delete(int? id)
        {
            var analisis = await _dataContext.Analises
                .Include(a => a.ArchivosAnalisis)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);

            var cedula = analisis.Cedula;

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
                }

                var archivoAnalisis = new ArchivoAnalisis
                {
                    analisis=await _dataContext.Analises.FindAsync(model.Id),
                    ArchivoPath=path,
                    user= await _userHelper.GetUserAsync(this.User.Identity.Name),
                    Fecha=DateTime.Now,
                    tamanio=model.Archivo.Length,
                    TipoArchivo= extension,
                    //Property = await _dataContext.Properties.FindAsync(model.Id)
                };

                _dataContext.ArchivosAnalisis.Add(archivoAnalisis);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"{nameof(Retorno)}/{archivoAnalisis.analisis.Cedula}");
            }

            return View(model);
        }
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

            _dataContext.ArchivosAnalisis.Remove(file);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction($"{nameof(Retorno)}/{file.analisis.Cedula}");
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


            return _fileHelper.GetFileAsStream(file.ArchivoPath.Substring(1)) ?? (IActionResult)NotFound();
        }

    }
}