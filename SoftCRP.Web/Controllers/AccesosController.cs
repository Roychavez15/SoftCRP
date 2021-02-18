using Microsoft.AspNetCore.Mvc;
using SoftCRP.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Controllers
{
    public class AccesosController : BaseController
    {
        private readonly IAccesosRepository _accesosRepository;

        public AccesosController(IAccesosRepository accesosRepository)
        {
            _accesosRepository = accesosRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _accesosRepository.GetListAccesosAsync());
        }

        public async Task<IActionResult> ActivaTodos(string Valor)
        {

            var accesos = _accesosRepository.GetAll().ToList();
            if(Valor=="Informacion")
            {
                accesos.ForEach(i => i.Informacion = true);                
            }
            if (Valor == "Analisis")
            {
                accesos.ForEach(i => i.Analisis = true);
            }
            if (Valor == "Seguimiento")
            {
                accesos.ForEach(i => i.Seguimiento = true);
            }
            if (Valor == "Tramites")
            {
                accesos.ForEach(i => i.Tramites = true);
            }
            if (Valor == "Capacitaciones")
            {
                accesos.ForEach(i => i.Capacitaciones = true);
            }
            if (Valor == "Conduccion")
            {
                accesos.ForEach(i => i.Conduccion = true);
            }
            if (Valor == "Mantenimiento")
            {
                accesos.ForEach(i => i.Mantenimiento = true);
            }
            if (Valor == "Siniestros")
            {
                accesos.ForEach(i => i.Siniestros = true);
            }
            if (Valor == "Sustitutos")
            {
                accesos.ForEach(i => i.Sustitutos = true);
            }
            if (Valor == "Graficos")
            {
                accesos.ForEach(i => i.Graficos = true);
            }
            await _accesosRepository.Actualiza(accesos);


            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DesactivaTodos(string Valor)
        {

            var accesos = _accesosRepository.GetAll().ToList();
            if (Valor == "Informacion")
            {
                accesos.ForEach(i => i.Informacion = false);
            }
            if (Valor == "Analisis")
            {
                accesos.ForEach(i => i.Analisis = false);
            }
            if (Valor == "Seguimiento")
            {
                accesos.ForEach(i => i.Seguimiento = false);
            }
            if (Valor == "Tramites")
            {
                accesos.ForEach(i => i.Tramites = false);
            }
            if (Valor == "Capacitaciones")
            {
                accesos.ForEach(i => i.Capacitaciones = false);
            }
            if (Valor == "Conduccion")
            {
                accesos.ForEach(i => i.Conduccion = false);
            }
            if (Valor == "Mantenimiento")
            {
                accesos.ForEach(i => i.Mantenimiento = false);
            }
            if (Valor == "Siniestros")
            {
                accesos.ForEach(i => i.Siniestros = false);
            }
            if (Valor == "Sustitutos")
            {
                accesos.ForEach(i => i.Sustitutos = false);
            }
            if (Valor == "Graficos")
            {
                accesos.ForEach(i => i.Graficos = false);
            }


            await _accesosRepository.Actualiza(accesos);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Activa(string Valor)
        {
            string[] dats = Valor.Split('-');

            var accesos = _accesosRepository.GetAll().Where(i=>i.Id==Convert.ToInt32(dats[1])).ToList();
            if (dats[0].Trim() == "Informacion")
            {
                accesos.ForEach(i => i.Informacion = true);
            }
            if (dats[0].Trim() == "Analisis")
            {
                accesos.ForEach(i => i.Analisis = true);
            }
            if (dats[0].Trim() == "Seguimiento")
            {
                accesos.ForEach(i => i.Seguimiento = true);
            }
            if (dats[0].Trim() == "Tramites")
            {
                accesos.ForEach(i => i.Tramites = true);
            }
            if (dats[0].Trim() == "Capacitaciones")
            {
                accesos.ForEach(i => i.Capacitaciones = true);
            }
            if (dats[0].Trim() == "Conduccion")
            {
                accesos.ForEach(i => i.Conduccion = true);
            }
            if (dats[0].Trim() == "Mantenimiento")
            {
                accesos.ForEach(i => i.Mantenimiento = true);
            }
            if (dats[0].Trim() == "Siniestros")
            {
                accesos.ForEach(i => i.Siniestros = true);
            }
            if (dats[0].Trim() == "Sustitutos")
            {
                accesos.ForEach(i => i.Sustitutos = true);
            }
            if (dats[0].Trim() == "Graficos")
            {
                accesos.ForEach(i => i.Graficos = true);
            }
            await _accesosRepository.Actualiza(accesos);


            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Desactiva(string Valor)
        {

            string[] dats = Valor.Split('-');

            var accesos = _accesosRepository.GetAll().Where(i => i.Id == Convert.ToInt32(dats[1])).ToList();
            if (dats[0].Trim() == "Informacion")
            {
                accesos.ForEach(i => i.Informacion = false);
            }
            if (dats[0].Trim() == "Analisis")
            {
                accesos.ForEach(i => i.Analisis = false);
            }
            if (dats[0].Trim() == "Seguimiento")
            {
                accesos.ForEach(i => i.Seguimiento = false);
            }
            if (dats[0].Trim() == "Tramites")
            {
                accesos.ForEach(i => i.Tramites = false);
            }
            if (dats[0].Trim() == "Capacitaciones")
            {
                accesos.ForEach(i => i.Capacitaciones = false);
            }
            if (dats[0].Trim() == "Conduccion")
            {
                accesos.ForEach(i => i.Conduccion = false);
            }
            if (dats[0].Trim() == "Mantenimiento")
            {
                accesos.ForEach(i => i.Mantenimiento = false);
            }
            if (dats[0].Trim() == "Siniestros")
            {
                accesos.ForEach(i => i.Siniestros = false);
            }
            if (dats[0].Trim() == "Sustitutos")
            {
                accesos.ForEach(i => i.Sustitutos = false);
            }
            if (dats[0].Trim() == "Graficos")
            {
                accesos.ForEach(i => i.Graficos = false);
            }


            await _accesosRepository.Actualiza(accesos);

            return RedirectToAction(nameof(Index));
        }
    }
}
