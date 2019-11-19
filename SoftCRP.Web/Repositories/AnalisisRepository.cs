using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Models;

namespace SoftCRP.Web.Repositories
{
    public class AnalisisRepository :  GenericRepository<Analisis>, IAnalisisRepository
    {
        private readonly DataContext _dataContext;
        private readonly IDatosRepository _datosRepository;

        public AnalisisRepository(
            DataContext dataContext,
            IDatosRepository datosRepository) : base(dataContext)
        {
            _dataContext = dataContext;
            _datosRepository = datosRepository;
        }
        public async Task<AnalisisViewModel> GetAnalisis(string cedula)
        {
            AnalisisViewModel analisisViewModel = new AnalisisViewModel();

            var cliente = _datosRepository.GetDatosCliente(cedula);

            if (cliente.Result.nit != null)
            {
                List<Analisis> analisis = await _dataContext.Analises
                    .Include(t => t.tipoAnalisis)
                    .Include(a => a.ArchivosAnalisis)
                    .Include(u => u.user)
                    //.ThenInclude(l=>l.tipo)
                    .Where(c => c.Cedula == cedula)
                    .OrderByDescending(o => o.Fecha).ToListAsync();


                analisisViewModel.cedula = cedula;
                analisisViewModel.analisis = analisis;
            }

            return analisisViewModel;
            //throw new NotImplementedException();
        }
        public async Task<Analisis> GetAnalisisByIdAsync(int? id)
        {

            var analisis = await _dataContext.Analises
                .Include(t => t.tipoAnalisis)
                .Include(a => a.ArchivosAnalisis)
                .Include(u => u.user)
                .Where(c => c.Id == id).FirstOrDefaultAsync();

            return analisis;
        }
        public async Task<IEnumerable<Analisis>> GetAnalisisReportesAsync(DateTime Inicio, DateTime Fin, string filter)
        {

            if (!string.IsNullOrEmpty(filter))
            {
                return await _dataContext.Analises
                    .Include(t => t.tipoAnalisis)
                    .Include(a => a.ArchivosAnalisis)
                    .Include(u => u.user)
                    .Where(f => f.Cedula==filter && (f.Fecha >= Inicio && f.Fecha <= Fin)).ToListAsync();
            }
            else
            {
                return await _dataContext.Analises
                    .Include(t => t.tipoAnalisis)
                    .Include(a => a.ArchivosAnalisis)
                    .Include(u => u.user)
                    .Where(f => f.Fecha >= Inicio && f.Fecha <= Fin).ToListAsync();
            }
            //return analisis;
        }

    }
}
