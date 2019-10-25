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

        public AnalisisRepository(
            DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<AnalisisViewModel> GetAnalisis(string cedula)
        {
            AnalisisViewModel analisisViewModel = new AnalisisViewModel();

            List<Analisis> analisis= await _dataContext.Analises
                .Include(t => t.tipoAnalisis)
                .Include(a => a.ArchivosAnalisis)
                .Include(u=>u.user)
                //.ThenInclude(l=>l.tipo)
                .Where(c => c.Cedula == cedula)                
                .OrderByDescending(o => o.Fecha).ToListAsync();

            analisisViewModel.cedula = cedula;
            analisisViewModel.analisis = analisis;

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


    }
}
