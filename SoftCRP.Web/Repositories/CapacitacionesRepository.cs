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
    public class CapacitacionesRepository : GenericRepository<Capacitacion>, ICapacitacionesRepository
    {
        private readonly DataContext _dataContext;

        public CapacitacionesRepository(
            DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<CapacitacionesViewModel> GetCapacitacionesAsync()
        {
            CapacitacionesViewModel capacitacionesViewModel = new CapacitacionesViewModel();

            List<Capacitacion> capacitaciones = await _dataContext.capacitaciones
                .Include(t => t.tipoCapacitacion)
                .Include(a => a.archivoCapacitaciones)
                .Include(u => u.user)
                //.ThenInclude(l=>l.tipo)
                //.Where(c => c.Cedula == cedula)
                .OrderByDescending(o => o.Fecha).ToListAsync();

            //capacitacionesViewModel.cedula = cedula;
            capacitacionesViewModel.capacitaciones = capacitaciones;

            return capacitacionesViewModel;
            //throw new NotImplementedException();
        }
        public async Task<Capacitacion> GetCapacitacionByIdAsync(int? id)
        {

            var capacitacion = await _dataContext.capacitaciones
                .Include(t => t.tipoCapacitacion)
                .Include(a => a.archivoCapacitaciones)
                .Include(u => u.user)
                .Where(c => c.Id == id).FirstOrDefaultAsync();

            return capacitacion;
        }

        public int GetCountAllCapacitaciones()
        {

            var capacitacion = _dataContext.capacitaciones.Count();

                //.Where(c => c.Cedula == nit).Count();
            //.Where(s => s.EstadoSolucion == null && s.Cedula == nit).Count();

            return capacitacion;
        }
    }
}
