using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;

namespace SoftCRP.Web.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IAnalisisRepository _analisisRepository;
        private readonly ICapacitacionesRepository _capacitacionesRepository;
        private readonly INovedadesRepository _novedadesRepository;
        private readonly ICombosHelper _combosHelper;
        private readonly ITramitesRepository _tramitesRepository;

        public ReportesController(
            IAnalisisRepository analisisRepository,
            ICapacitacionesRepository capacitacionesRepository,
            INovedadesRepository novedadesRepository,
            ICombosHelper combosHelper,
            ITramitesRepository tramitesRepository)
        {
            _analisisRepository = analisisRepository;
            _capacitacionesRepository = capacitacionesRepository;
            _novedadesRepository = novedadesRepository;
            _combosHelper = combosHelper;
            _tramitesRepository = tramitesRepository;
        }
        public IActionResult IndexAnalisis()
        {
            ReporteAnalisisViewModel reporte = new ReporteAnalisisViewModel();
            reporte.Inicio = DateTime.Now;
            reporte.Fin = DateTime.Now;
            reporte.Analises = null;
            reporte.Clientes = _combosHelper.GetComboClientes();
            return View(reporte);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAnalisis(ReporteAnalisisViewModel model)
        {
            var inicio = model.Inicio;
            var fin = model.Fin;

            //var reporte = await _analisisRepository.GetAnalisisReportesAsync(model.Inicio, model.Fin);
            ReporteAnalisisViewModel reporte = new ReporteAnalisisViewModel();

            reporte.Inicio = inicio;
            reporte.Fin = fin;
            reporte.Analises = await _analisisRepository.GetAnalisisReportesAsync(model.Inicio, model.Fin, model.ClienteId);
            reporte.Clientes = _combosHelper.GetComboClientes();
            return View(reporte);
        }


        public IActionResult IndexCapacitaciones()
        {
            ReporteCapacitacionesViewModel reporte = new ReporteCapacitacionesViewModel();
            reporte.Inicio = DateTime.Now;
            reporte.Fin = DateTime.Now;
            reporte.capacitaciones = null;
            return View(reporte);
        }

        [HttpPost]
        public async Task<IActionResult> IndexCapacitaciones(ReporteCapacitacionesViewModel model)
        {
            var inicio = model.Inicio;
            var fin = model.Fin;

            //var reporte = await _analisisRepository.GetAnalisisReportesAsync(model.Inicio, model.Fin);
            ReporteCapacitacionesViewModel reporte = new ReporteCapacitacionesViewModel();

            reporte.Inicio = inicio;
            reporte.Fin = fin;
            reporte.capacitaciones = await _capacitacionesRepository.GetCapacitacionReportesAsync(model.Inicio, model.Fin);

            return View(reporte);
        }

        public IActionResult IndexNovedades()
        {
            ReporteNovedadesViewModel reporte = new ReporteNovedadesViewModel();
            reporte.Inicio = DateTime.Now;
            reporte.Fin = DateTime.Now;
            reporte.novedades = null;
            reporte.Clientes = _combosHelper.GetComboClientes();
            return View(reporte);
        }

        [HttpPost]
        public async Task<IActionResult> IndexNovedades(ReporteNovedadesViewModel model)
        {
            var inicio = model.Inicio;
            var fin = model.Fin;

            //var reporte = await _analisisRepository.GetAnalisisReportesAsync(model.Inicio, model.Fin);
            ReporteNovedadesViewModel reporte = new ReporteNovedadesViewModel();

            reporte.Inicio = inicio;
            reporte.Fin = fin;
            reporte.novedades = await _novedadesRepository.GetNovedadReportesAsync(model.Inicio, model.Fin, model.ClienteId);
            reporte.Clientes = _combosHelper.GetComboClientes();
            return View(reporte);


        }

        public IActionResult IndexTramites()
        {
            ReporteTramitesViewModel reporte = new ReporteTramitesViewModel();
            reporte.Inicio = DateTime.Now;
            reporte.Fin = DateTime.Now;
            reporte.tramites = null;
            reporte.Clientes = _combosHelper.GetComboClientes();
            return View(reporte);
        }

        [HttpPost]
        public async Task<IActionResult> IndexTramites(ReporteTramitesViewModel model)
        {
            var inicio = model.Inicio;
            var fin = model.Fin;

            //var reporte = await _analisisRepository.GetAnalisisReportesAsync(model.Inicio, model.Fin);
            ReporteTramitesViewModel reporte = new ReporteTramitesViewModel();

            reporte.Inicio = inicio;
            reporte.Fin = fin;
            reporte.tramites = await _tramitesRepository.GetTramiteReportesAsync(model.Inicio, model.Fin, model.ClienteId);
            reporte.Clientes = _combosHelper.GetComboClientes();
            return View(reporte);
        }
    }
}