using Microsoft.EntityFrameworkCore;
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

        public NovedadesRepository(
            DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<NovedadesViewModel> GetNovedadAsync(string cedula)
        {
            NovedadesViewModel novedadesViewModel = new NovedadesViewModel();

            List<Novedad> novedades = await _dataContext.novedades
                .Include(a => a.archivoNovedades)
                .Include(u => u.user)
                .Include(us=>us.userSolucion)
                .Where(c => c.Cedula == cedula)
                .OrderByDescending(o => o.Fecha).ToListAsync();

            novedadesViewModel.cedula = cedula;
            novedadesViewModel.novedades = novedades;

            return novedadesViewModel;
            //throw new NotImplementedException();
        }
        public async Task<Novedad> GetNovedadByIdAsync(int? id)
        {

            var novedad = await _dataContext.novedades
                .Include(a => a.archivoNovedades)
                .Include(u => u.user)
                .Include(us => us.userSolucion)
                .Where(c => c.Id == id).FirstOrDefaultAsync();

            return novedad;
        }
        public int GetNovedadAllNotSolution(string nit)
        {

            var novedad = _dataContext.novedades
                //.Where(c => c.Cedula == nit);
                .Where(s => s.EstadoSolucion == null && s.Cedula == nit).Count();

            return novedad;
        }
    }
}
