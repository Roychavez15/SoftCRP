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
    public class NovedadesRepository : GenericRepository<Novedad>, INovedadesRepository
    {
        private readonly DataContext _dataContext;
        private readonly IDatosRepository _datosRepository;

        public NovedadesRepository(
            DataContext dataContext,
            IDatosRepository datosRepository) : base(dataContext)
        {
            _dataContext = dataContext;
            _datosRepository = datosRepository;
        }

        public async Task<NovedadesViewModel> GetNovedadAsync(string cedula)
        {
            NovedadesViewModel novedadesViewModel = new NovedadesViewModel();
            var cliente = _datosRepository.GetDatosCliente(cedula);

            if (cliente.Result.nit != null)
            {
                List<Novedad> novedades = await _dataContext.novedades
                .Include(a => a.archivoNovedades)
                .Include(u => u.user)
                .Include(us => us.userSolucion)
                .Where(c => c.Cedula == cedula)
                .OrderByDescending(o => o.Fecha).ToListAsync();

                novedadesViewModel.cedula = cedula;
                novedadesViewModel.novedades = novedades;
            }

            return novedadesViewModel;
            //throw new NotImplementedException();
        }
        public async Task<Novedad> GetNovedadByIdAsync(int? id)
        {

            var novedad = await _dataContext.novedades
                .Include(a => a.archivoNovedades)
                .Include(u => u.user)
                .Include(us => us.userSolucion)
                .Include(l=>l.logNovedades)
                .Where(c => c.Id == id).FirstOrDefaultAsync();

            return novedad;
        }
        public int GetNovedadAllNotSolution(string nit)
        {

            //var novedad = _dataContext.novedades
            //    .Where(s => (s.EstadoSolucion == null || s.EstadoSolucion != "CERRADO") && s.Cedula == nit).Count();

            //return novedad;
            if(nit!="")
            {
                return _dataContext.novedades
                        .Where(s => (s.EstadoSolucion == null || s.EstadoSolucion != "CERRADO") && s.Cedula == nit).Count();
            }
            else //administrador o renting
            {
                return _dataContext.novedades
                    .Where(s => (s.EstadoSolucion == null || s.EstadoSolucion != "CERRADO")).Count();

            }
        }
        public async Task<IEnumerable<Novedad>> GetNovedadReportesAsync(DateTime Inicio, DateTime Fin, string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return await _dataContext.novedades
                    .Include(a => a.archivoNovedades)
                    .Include(u => u.user)
                    .Include(us => us.userSolucion)
                    .Where(f => f.Cedula == filter &&(f.Fecha >= Inicio && f.Fecha <= Fin)).ToListAsync();
            }
            else
            {
                return await _dataContext.novedades
                    .Include(a => a.archivoNovedades)
                    .Include(u => u.user)
                    .Include(us => us.userSolucion)
                    .Where(f => f.Fecha >= Inicio && f.Fecha <= Fin.AddDays(1)).ToListAsync();
            }

        }
    }
}
