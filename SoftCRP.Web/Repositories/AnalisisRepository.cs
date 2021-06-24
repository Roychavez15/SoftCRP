using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Models;
using Z.EntityFramework.Plus;

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
            if(cliente.Result==null)
            {
                return null;
            }

            if (cliente.Result.nit != null)
            {
                List<Analisis> analisis = await _dataContext.Analises
                    .Include(t => t.tipoAnalisis)
                    .Include(a => a.ArchivosAnalisis)
                    .Include(u => u.user)
                    //.Include(h=>h.history)
                    //.ThenInclude(l=>l.tipo)
                    .Where(c => c.Cedula == cedula)
                    //.Where(p=>p.history.isActive==true)
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
                    .Where(f => f.Cedula == filter && (f.Fecha >= Inicio && f.Fecha <= Fin.AddDays(1)))
                    .Include(t => t.tipoAnalisis)
                    .Include(a => a.ArchivosAnalisis)
                    .Include(u => u.user)

                    .ToListAsync();
            }
            else
            {
                return await _dataContext.Analises
                    .Include(t => t.tipoAnalisis)
                    .Include(a => a.ArchivosAnalisis)
                    .Include(u => u.user)
                    
                    //.FromSql("select b.* from AspNetUsers b inner join analises a on b.cedula =a.cedula")
                    .Where(f => f.Fecha >= Inicio && f.Fecha <= Fin.AddDays(1)).ToListAsync();
            }
            //return analisis;
        }

        public int GetCountAllAnalisis(string nit)
        {

            //var analisis = _dataContext.Analises
            //    .Where(c => c.Cedula == nit).Count();
            //return analisis;
            if(nit!="")
            {
                return _dataContext.Analises
                        .Where(c => c.Cedula == nit).Count();
            }
            else
            {
                return _dataContext.Analises
                        .Count();
            }

        }
    }
}
