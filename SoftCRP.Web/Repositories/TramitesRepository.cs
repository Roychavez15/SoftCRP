﻿using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public class TramitesRepository : GenericRepository<Tramite>, ITramitesRepository
    {
        private readonly DataContext _dataContext;
        private readonly IDatosRepository _datosRepository;

        public TramitesRepository(
            DataContext dataContext,
            IDatosRepository datosRepository) : base(dataContext)
        {
            _dataContext = dataContext;
            _datosRepository = datosRepository;
        }
        public async Task<TramitesViewModel> GetTramiteAsync(string cedula)
        {
            TramitesViewModel tramitesViewModel = new TramitesViewModel();
            var cliente = _datosRepository.GetDatosCliente(cedula);

            if (cliente.Result.nit != null)
            {
                List<Tramite> tramites = await _dataContext.tramites
                .Include(t => t.tipoTramite)
                .Include(a => a.archivoTramites)
                .Include(u => u.user)
                .Where(c => c.Cedula == cedula)
                .OrderByDescending(o => o.Fecha).ToListAsync();

                tramitesViewModel.cedula = cedula;
                tramitesViewModel.tramites = tramites;
            }

            return tramitesViewModel;
        }
        public async Task<Tramite> GetTramiteByIdAsync(int? id)
        {

            var tramite = await _dataContext.tramites
                .Include(t => t.tipoTramite)
                .Include(a => a.archivoTramites)
                .Include(u => u.user)
                .Where(c => c.Id == id).FirstOrDefaultAsync();

            return tramite;
        }
        public int GetCountAllTramites(string nit)
        {

            //var novedad = _dataContext.tramites
            //    .Where(c => c.Cedula == nit).Count();
            //return novedad;

            if(nit!="")
            {
                return _dataContext.tramites
                        .Where(c => c.Cedula == nit).Count();
            }
            else
            {
                return _dataContext.tramites
                        .Count();
            }

        }

        public async Task<IEnumerable<Tramite>> GetTramiteReportesAsync(DateTime Inicio, DateTime Fin, string filter, string Ciudad)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                if(!string.IsNullOrEmpty(Ciudad))
                {
                    return await _dataContext.tramites
                        .Include(t => t.tipoTramite)
                        .Include(a => a.archivoTramites)
                        .Include(u => u.user)
                        .Where(f => f.Cedula == filter && f.Ciudad== Ciudad && (f.Fecha >= Inicio && f.Fecha <= Fin.AddDays(1))).ToListAsync();
                }
                else
                {
                    return await _dataContext.tramites
                        .Include(t => t.tipoTramite)
                        .Include(a => a.archivoTramites)
                        .Include(u => u.user)
                        .Where(f => f.Cedula == filter && (f.Fecha >= Inicio && f.Fecha <= Fin.AddDays(1))).ToListAsync();
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(Ciudad))
                {
                    return await _dataContext.tramites
                        .Include(t => t.tipoTramite)
                        .Include(a => a.archivoTramites)
                        .Include(u => u.user)
                        .Where(f => f.Ciudad == Ciudad && f.Fecha >= Inicio && f.Fecha <= Fin.AddDays(1)).ToListAsync();
                }
                else
                {
                    return await _dataContext.tramites
                        .Include(t => t.tipoTramite)
                        .Include(a => a.archivoTramites)
                        .Include(u => u.user)
                        .Where(f => f.Fecha >= Inicio && f.Fecha <= Fin.AddDays(1)).ToListAsync();
                }


            }
        }
    }
}
