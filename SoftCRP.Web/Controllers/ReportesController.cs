using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;

namespace SoftCRP.Web.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IDatosRepository _datosRepository;
        private readonly IAnalisisRepository _analisisRepository;
        private readonly ICapacitacionesRepository _capacitacionesRepository;
        private readonly INovedadesRepository _novedadesRepository;
        private readonly ICombosHelper _combosHelper;
        private readonly ILogRepository _logRepository;
        private readonly ITramitesRepository _tramitesRepository;
        private readonly ILogger<ReportesController> _logger;
        private readonly IUserHelper _userHelper;

        public ReportesController(
            IDatosRepository datosRepository,
            IAnalisisRepository analisisRepository,
            ICapacitacionesRepository capacitacionesRepository,
            INovedadesRepository novedadesRepository,
            ICombosHelper combosHelper,
            ILogRepository logRepository,
            ITramitesRepository tramitesRepository,
            ILogger<ReportesController> logger,
            IUserHelper userHelper)
        {
            _datosRepository = datosRepository;
            _analisisRepository = analisisRepository;
            _capacitacionesRepository = capacitacionesRepository;
            _novedadesRepository = novedadesRepository;
            _combosHelper = combosHelper;
            _logRepository = logRepository;
            _tramitesRepository = tramitesRepository;
            _logger = logger;
            _userHelper = userHelper;
        }

        public async Task<IActionResult> IndexInformacion()
        {
            //ReporteAnalisisViewModel reporte = new ReporteAnalisisViewModel();
            //reporte.Inicio = DateTime.Now;
            //reporte.Fin = DateTime.Now;
            //reporte.Analises = null;
            //reporte.Clientes = _combosHelper.GetComboClientes();
            //return View(reporte);
            var model = await _datosRepository.GetDatosAutoAllAsync();
            return View(model);
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
            var analisis= await _analisisRepository.GetAnalisisReportesAsync(model.Inicio, model.Fin, model.ClienteId);
            //reporte.Analises = await _analisisRepository.GetAnalisisReportesAsync(model.Inicio, model.Fin, model.ClienteId);
            reporte.Analises = await TonewAnalisis(analisis.ToList());

            reporte.Clientes = _combosHelper.GetComboClientes();
            return View(reporte);
        }
        private async Task<List<Analisis>> TonewAnalisis(List<Analisis> analisis)
        {
            List<Analisis> lista = new List<Analisis>();
            foreach(Analisis ana in analisis)
            {
                Analisis ali = new Analisis();
                ali.ArchivosAnalisis = ana.ArchivosAnalisis;
                ali.Cedula = ana.Cedula;
                ali.Fecha = ana.Fecha;
                //ali.FechaLocal = ana.FechaLocal;
                ali.Id = ana.Id;
                ali.Observaciones = ana.Observaciones;
                ali.Placa = ana.Placa;
                ali.tipoAnalisis = ana.tipoAnalisis;
                ali.tipoAnalisisId = ana.tipoAnalisisId;
                ali.user = ana.user;
                ali.userCliente = await _userHelper.GetUserByCedulaAsync(ana.Cedula);
                lista.Add(ali);
            }
            return lista;
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
            var novedadeslist = await _novedadesRepository.GetNovedadReportesAsync(model.Inicio, model.Fin, model.ClienteId);
            reporte.novedades = await TonewNovedades(novedadeslist.ToList());
            reporte.Clientes = _combosHelper.GetComboClientes();
            return View(reporte);


        }
        private async Task<List<Novedad>> TonewNovedades(List<Novedad> novedades)
        {
            List<Novedad> lista = new List<Novedad>();
            foreach (Novedad ana in novedades)
            {
                Novedad ali = new Novedad();
                ali.archivoNovedades = ana.archivoNovedades;
                ali.Cedula = ana.Cedula;
                ali.EstadoSolucion = ana.EstadoSolucion;
                ali.Fecha = ana.Fecha;
                ali.FechaSolucion = ana.FechaSolucion;
                ali.Id = ana.Id;
                ali.logNovedades = ana.logNovedades;
                ali.Motivo = ana.Motivo;
                ali.Observaciones = ana.Observaciones;
                ali.Placa = ana.Placa;
                ali.Solucion = ana.Solucion;
                ali.SubMotivo = ana.SubMotivo;
                ali.user = ana.user;
                ali.userSolucion = ana.userSolucion;
                ali.ViaIngreso = ana.ViaIngreso;

                ali.userCliente = await _userHelper.GetUserByCedulaAsync(ana.Cedula);
                lista.Add(ali);
            }
            return lista;
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
            var tramiteslist = await _tramitesRepository.GetTramiteReportesAsync(model.Inicio, model.Fin, model.ClienteId);
            reporte.tramites = await TonewTramites(tramiteslist.ToList());
            reporte.Clientes = _combosHelper.GetComboClientes();
            return View(reporte);
        }
        private async Task<List<Tramite>> TonewTramites(List<Tramite> tramites)
        {
            List<Tramite> lista = new List<Tramite>();
            foreach (Tramite ana in tramites)
            {
                Tramite ali = new Tramite();
                ali.Anio = ana.Anio;
                ali.archivoTramites = ana.archivoTramites;
                ali.Cedula = ana.Cedula;
                ali.Fecha = ana.Fecha;
                ali.Id = ana.Id;
                ali.Mes = ana.Mes;
                ali.Observaciones = ana.Observaciones;
                ali.Placa = ana.Placa;
                ali.tipoTramite = ana.tipoTramite;
                ali.tipoTramiteId = ana.tipoTramiteId;
                ali.user = ana.user;
                ali.userCliente = await _userHelper.GetUserByCedulaAsync(ana.Cedula);
                
                lista.Add(ali);
            }
            return lista;
        }
        public IActionResult IndexLogs()
        {
            ReporteLogsViewModel reporte = new ReporteLogsViewModel();
            reporte.Inicio = DateTime.Now;
            reporte.Fin = DateTime.Now;
            return View(reporte);
        }

        [HttpPost]
        public async Task<IActionResult> IndexLogs(ReporteLogsViewModel model)
        {
            var inicio = model.Inicio;
            var fin = model.Fin;

            //var reporte = await _analisisRepository.GetAnalisisReportesAsync(model.Inicio, model.Fin);
            ReporteLogsViewModel reporte = new ReporteLogsViewModel();

            reporte.Inicio = inicio;
            reporte.Fin = fin;
            reporte.Logs = await _logRepository.GetLogsReportesAsync(model.Inicio, model.Fin);
            
            return View(reporte);
        }
    }
}