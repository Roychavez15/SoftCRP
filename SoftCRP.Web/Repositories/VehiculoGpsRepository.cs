using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public class VehiculoGpsRepository : GenericRepository<VehiculoGps>, IVehiculoGpsRepository
    {
        private readonly DataContext _dataContext;

        public VehiculoGpsRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<VehiculoGps> GetVehiculoByDateAsync(int dia, int mes, int anio, int vehiculoId)
        {

            return await _dataContext.vehiculosGps
                .Where(c => c.vehiculo.Id==vehiculoId && c.dia==dia && c.mes==mes && c.anio==anio)
                .FirstOrDefaultAsync();
        }
        public IEnumerable<VehiculoGps> GetVehiculosGPSAsync(int dia, int mes, int anio, string userId, string vehiculoId)
        {

            if (userId != "")
            {
                if (vehiculoId != "")
                {
                    //var consultas = _dataContext.vehiculosGps
                    //.Include(v => v.vehiculo)
                    //.ThenInclude(u => u.user)
                    //.Where(v => v.vehiculo.user.Cedula == userId && v.vehiculo.Placa==vehiculoId)
                    //.OrderByDescending(p => p.anio)
                    //.OrderByDescending(m => m.mes)
                    //.OrderByDescending(d => d.dia)
                    //.GroupBy(v => v.vehiculo)
                    //.ToList()
                    //.Select(g => new { g.Key, ultimo = g.FirstOrDefault() }).ToList()
                    //.Select(ve => ve.ultimo);

                    //return consultas.Count() > 0 ? consultas : null;
                    var consultas = _dataContext.vehiculosGps
                        .Include(v => v.vehiculo)
                        .ThenInclude(u => u.user)
                        .Where(v => v.vehiculo.user.Cedula == userId && v.vehiculo.Placa == vehiculoId)
                        .OrderByDescending(p => p.anio)
                        .ThenByDescending(m => m.mes)
                        .ThenByDescending(d => d.dia)
                        .GroupBy(v => new { v.vehiculo, v.anio, v.mes })
                        .Select(v => new { v.Key.vehiculo, v.Key.anio, v.Key.mes, Registros = v.ToList() });

                    List<VehiculoGps> vehiculoGps = new List<VehiculoGps>();
                    int contador = 0;
                    foreach (var item in consultas.ToList())
                    {
                        contador = contador + 1;
                        var placa = item.vehiculo.Placa;
                        var registros = item.Registros.ToList();
                        var mes1 = item.mes;
                        var anio1 = item.anio;
                        var incidencias = _dataContext.incidencias.FirstOrDefault(i => i.User.Id == item.vehiculo.user.Id);

                        VehiculoGps vehiculos = new VehiculoGps();
                        vehiculos.Id = contador;
                        vehiculos.vehiculo = item.vehiculo;
                        vehiculos.usuario = registros.FirstOrDefault().usuario;
                        vehiculos.ahorro = registros.Sum(a => a.ahorro);
                        vehiculos.anio = anio1;
                        vehiculos.mes = mes1;
                        vehiculos.conductores = registros.Sum(c => c.conductores);
                        vehiculos.hardbraking = registros.Sum(h => h.hardbraking);
                        vehiculos.kilometerstraveled = registros.Sum(k => k.kilometerstraveled);
                        vehiculos.latitude = registros.FirstOrDefault().latitude;
                        vehiculos.longitude = registros.FirstOrDefault().longitude;
                        vehiculos.sharpacceleration = registros.Sum(sh => sh.sharpacceleration);
                        vehiculos.sharpturn = registros.Sum(sha => sha.sharpturn);
                        vehiculos.siniestros = registros.Sum(si => si.siniestros);
                        vehiculos.speeding = registros.Sum(sp => sp.speeding);
                        vehiculos.talleres = registros.Sum(t => t.talleres);
                        vehiculos.trips = registros.Sum(tr => tr.trips);

                        var suma = 0M;
                        var recorrido = registros.Sum(k => k.kilometerstraveled) / 10;

                        if (incidencias != null)
                        {

                            suma = incidencias.ExcesoVelocidad * registros.Sum(sp => sp.speeding)
                            + incidencias.FrenazoBrusco * registros.Sum(h => h.hardbraking)
                            + incidencias.AceleracionesBruscas * registros.Sum(sh => sh.sharpacceleration)
                            + incidencias.GiroBrusco * registros.Sum(sha => sha.sharpturn);

                            if (recorrido > 0)
                            {
                                vehiculos.score = 100 - (suma / recorrido);
                                if(vehiculos.score<0)
                                {
                                    vehiculos.score = 0;
                                }
                            }
                            else
                            {
                                vehiculos.score = 0;
                            }
                        }
                        else
                        {
                            vehiculos.score = 0;
                        }

                        vehiculoGps.Add(vehiculos);

                    };

                    return vehiculoGps.Count() > 0 ? vehiculoGps : null;
                }
                else
                {
                    var consultas = _dataContext.vehiculosGps
                        .Include(v => v.vehiculo)
                        .ThenInclude(u => u.user)
                        .Where(v => v.vehiculo.user.Cedula == userId)
                        .OrderByDescending(p => p.anio)
                        .ThenByDescending(m => m.mes)
                        .ThenByDescending(d => d.dia)
                        .GroupBy(v => new { v.vehiculo, v.anio, v.mes })
                        .Select(v => new { v.Key.vehiculo, v.Key.anio, v.Key.mes, Registros = v.ToList() });

                    List<VehiculoGps> vehiculoGps = new List<VehiculoGps>();
                    int contador = 0;
                    foreach (var item in consultas.ToList())
                    {
                        contador = contador + 1;
                        var placa = item.vehiculo.Placa;
                        var registros = item.Registros.ToList();
                        var mes1 = item.mes;
                        var anio1 = item.anio;
                        var incidencias = _dataContext.incidencias.FirstOrDefault(i => i.User.Id == item.vehiculo.user.Id);

                        VehiculoGps vehiculos = new VehiculoGps();
                        vehiculos.Id = contador;
                        vehiculos.vehiculo = item.vehiculo;
                        vehiculos.usuario = registros.FirstOrDefault().usuario;
                        vehiculos.ahorro = registros.Sum(a => a.ahorro);
                        vehiculos.anio = anio1;
                        vehiculos.mes = mes1;
                        vehiculos.conductores = registros.Sum(c => c.conductores);
                        vehiculos.hardbraking = registros.Sum(h => h.hardbraking);
                        vehiculos.kilometerstraveled = registros.Sum(k => k.kilometerstraveled);
                        vehiculos.latitude = registros.FirstOrDefault().latitude;
                        vehiculos.longitude = registros.FirstOrDefault().longitude;
                        vehiculos.sharpacceleration = registros.Sum(sh => sh.sharpacceleration);
                        vehiculos.sharpturn = registros.Sum(sha => sha.sharpturn);
                        vehiculos.siniestros = registros.Sum(si => si.siniestros);
                        vehiculos.speeding = registros.Sum(sp => sp.speeding);
                        vehiculos.talleres = registros.Sum(t => t.talleres);
                        vehiculos.trips = registros.Sum(tr => tr.trips);

                        var suma = 0M;
                        var recorrido = registros.Sum(k => k.kilometerstraveled) / 10;

                        if (incidencias != null)
                        {

                            suma = incidencias.ExcesoVelocidad * registros.Sum(sp => sp.speeding)
                            + incidencias.FrenazoBrusco * registros.Sum(h => h.hardbraking)
                            + incidencias.AceleracionesBruscas * registros.Sum(sh => sh.sharpacceleration)
                            + incidencias.GiroBrusco * registros.Sum(sha => sha.sharpturn);

                            if (recorrido > 0)
                            {
                                vehiculos.score = 100 - (suma / recorrido);
                                if (vehiculos.score < 0)
                                {
                                    vehiculos.score = 0;
                                }
                            }
                            else
                            {
                                vehiculos.score = 0;
                            }
                        }
                        else
                        {
                            vehiculos.score = 0;
                        }

                        vehiculoGps.Add(vehiculos);

                    };

                    return vehiculoGps.Count() > 0 ? vehiculoGps : null;
                }
            }
            else
            {
                if (vehiculoId != "")
                {
                    //var consultas = _dataContext.vehiculosGps
                    //.Include(v => v.vehiculo)
                    //.ThenInclude(u => u.user)
                    //.Where(v => v.vehiculo.Placa == vehiculoId)
                    //.OrderByDescending(p => p.anio)
                    //.OrderByDescending(m => m.mes)
                    //.OrderByDescending(d => d.dia)
                    //.GroupBy(v => v.vehiculo)
                    //.ToList()
                    //.Select(g => new { g.Key, ultimo = g.FirstOrDefault() }).ToList()
                    //.Select(ve => ve.ultimo);

                    //return consultas.Count() > 0 ? consultas : null;
                   var consultas = _dataContext.vehiculosGps
                        .Include(v => v.vehiculo)
                        .ThenInclude(u => u.user)
                        .Where(v => v.vehiculo.Placa == vehiculoId)
                        .OrderByDescending(p => p.anio)
                        .ThenByDescending(m => m.mes)
                        .ThenByDescending(d => d.dia)
                        .GroupBy(v => new { v.vehiculo, v.anio, v.mes })
                        .Select(v => new { v.Key.vehiculo, v.Key.anio, v.Key.mes, Registros = v.ToList() });

                    List<VehiculoGps> vehiculoGps = new List<VehiculoGps>();
                    int contador = 0;
                    foreach (var item in consultas.ToList())
                    {
                        contador = contador + 1;
                        var placa = item.vehiculo.Placa;
                        var registros = item.Registros.ToList();
                        var mes1 = item.mes;
                        var anio1 = item.anio;
                        var incidencias = _dataContext.incidencias.FirstOrDefault(i => i.User.Id == item.vehiculo.user.Id);

                        VehiculoGps vehiculos = new VehiculoGps();
                        vehiculos.Id = contador;
                        vehiculos.vehiculo = item.vehiculo;
                        vehiculos.usuario = registros.FirstOrDefault().usuario;
                        vehiculos.ahorro = registros.Sum(a => a.ahorro);
                        vehiculos.anio = anio1;
                        vehiculos.mes = mes1;
                        vehiculos.conductores = registros.Sum(c => c.conductores);
                        vehiculos.hardbraking = registros.Sum(h => h.hardbraking);
                        vehiculos.kilometerstraveled = registros.Sum(k => k.kilometerstraveled);
                        vehiculos.latitude = registros.FirstOrDefault().latitude;
                        vehiculos.longitude = registros.FirstOrDefault().longitude;
                        vehiculos.sharpacceleration = registros.Sum(sh => sh.sharpacceleration);
                        vehiculos.sharpturn = registros.Sum(sha => sha.sharpturn);
                        vehiculos.siniestros = registros.Sum(si => si.siniestros);
                        vehiculos.speeding = registros.Sum(sp => sp.speeding);
                        vehiculos.talleres = registros.Sum(t => t.talleres);
                        vehiculos.trips = registros.Sum(tr => tr.trips);

                        var suma = 0M;
                        var recorrido = registros.Sum(k => k.kilometerstraveled) / 10;

                        if (incidencias != null)
                        {

                            suma = incidencias.ExcesoVelocidad * registros.Sum(sp => sp.speeding)
                            + incidencias.FrenazoBrusco * registros.Sum(h => h.hardbraking)
                            + incidencias.AceleracionesBruscas * registros.Sum(sh => sh.sharpacceleration)
                            + incidencias.GiroBrusco * registros.Sum(sha => sha.sharpturn);

                            if (recorrido > 0)
                            {
                                vehiculos.score = 100 - (suma / recorrido);
                                if (vehiculos.score < 0)
                                {
                                    vehiculos.score = 0;
                                }
                            }
                            else
                            {
                                vehiculos.score = 0;
                            }
                        }
                        else
                        {
                            vehiculos.score = 0;
                        }

                        vehiculoGps.Add(vehiculos);

                    };

                    return vehiculoGps.Count() > 0 ? vehiculoGps : null;
                }
                else
                {
                    //var consultas = _dataContext.vehiculosGps
                    //.Include(v => v.vehiculo)
                    //.ThenInclude(u => u.user)                    
                    //.OrderByDescending(p => p.anio)
                    //.OrderByDescending(m => m.mes)
                    //.OrderByDescending(d => d.dia)
                    //.GroupBy(v => v.vehiculo)
                    //.ToList()
                    //.Select(g => new { g.Key, ultimo = g.FirstOrDefault() }).ToList()
                    //.Select(ve => ve.ultimo);

                    //return consultas.Count() > 0 ? consultas : null;
                    var consultas = _dataContext.vehiculosGps
                        .Include(v => v.vehiculo)
                        .ThenInclude(u => u.user)
                        .OrderByDescending(p => p.anio)
                        .ThenByDescending(m => m.mes)
                        .ThenByDescending(d => d.dia)
                        .GroupBy(v => new { v.vehiculo, v.anio, v.mes })
                        .Select(v => new { v.Key.vehiculo, v.Key.anio, v.Key.mes, Registros = v.ToList() });

                    List<VehiculoGps> vehiculoGps = new List<VehiculoGps>();
                    int contador = 0;
                    foreach (var item in consultas.ToList())
                    {
                        contador = contador + 1;
                        var placa = item.vehiculo.Placa;
                        var registros = item.Registros.ToList();
                        var mes1 = item.mes;
                        var anio1 = item.anio;
                        var incidencias = _dataContext.incidencias.FirstOrDefault(i => i.User.Id == item.vehiculo.user.Id);

                        VehiculoGps vehiculos = new VehiculoGps();
                        vehiculos.Id = contador;
                        vehiculos.vehiculo = item.vehiculo;
                        vehiculos.usuario = registros.FirstOrDefault().usuario;
                        vehiculos.ahorro = registros.Sum(a => a.ahorro);
                        vehiculos.anio = anio1;
                        vehiculos.mes = mes1;
                        vehiculos.conductores = registros.Sum(c => c.conductores);
                        vehiculos.hardbraking = registros.Sum(h => h.hardbraking);
                        vehiculos.kilometerstraveled = registros.Sum(k => k.kilometerstraveled);
                        vehiculos.latitude = registros.FirstOrDefault().latitude;
                        vehiculos.longitude = registros.FirstOrDefault().longitude;
                        vehiculos.sharpacceleration = registros.Sum(sh => sh.sharpacceleration);
                        vehiculos.sharpturn = registros.Sum(sha => sha.sharpturn);
                        vehiculos.siniestros = registros.Sum(si => si.siniestros);
                        vehiculos.speeding = registros.Sum(sp => sp.speeding);
                        vehiculos.talleres = registros.Sum(t => t.talleres);
                        vehiculos.trips = registros.Sum(tr => tr.trips);

                        var suma = 0M;
                        var recorrido = registros.Sum(k => k.kilometerstraveled) / 10;

                        if (incidencias != null)
                        {

                            suma = incidencias.ExcesoVelocidad * registros.Sum(sp => sp.speeding)
                            + incidencias.FrenazoBrusco * registros.Sum(h => h.hardbraking)
                            + incidencias.AceleracionesBruscas * registros.Sum(sh => sh.sharpacceleration)
                            + incidencias.GiroBrusco * registros.Sum(sha => sha.sharpturn);

                            if (recorrido > 0)
                            {
                                vehiculos.score = 100 - (suma / recorrido);
                                if (vehiculos.score < 0)
                                {
                                    vehiculos.score = 0;
                                }
                            }
                            else
                            {
                                vehiculos.score = 0;
                            }
                        }
                        else
                        {
                            vehiculos.score = 0;
                        }

                        vehiculoGps.Add(vehiculos);

                    };

                    return vehiculoGps.Count() > 0 ? vehiculoGps : null;
                }
            }





        }


        public IEnumerable<VehiculoGps> GetVehiculosGPSPositionAsync(int dia, int mes, int anio, string userId, string vehiculoId)
        {

            if (userId != "")
            {
                if (vehiculoId != "")
                {
                    var consultas = _dataContext.vehiculosGps
                    .Include(v => v.vehiculo)
                    .ThenInclude(u => u.user)
                    .Where(v => v.vehiculo.user.Cedula == userId && v.vehiculo.Placa == vehiculoId)
                    .OrderByDescending(p => p.anio)
                    .ThenByDescending(m => m.mes)
                    .ThenByDescending(d => d.dia)
                    .GroupBy(v => v.vehiculo)
                    .ToList()
                    .Select(g => new { g.Key, ultimo = g.FirstOrDefault() }).ToList()
                    .Select(ve => ve.ultimo);

                    return consultas.Count() > 0 ? consultas : null;

                }
                else
                {
                    var consultas = _dataContext.vehiculosGps
                        .Include(v => v.vehiculo)
                        .ThenInclude(u => u.user)
                        .Where(v => v.vehiculo.user.Cedula == userId)
                        .OrderByDescending(p => p.anio)
                        .ThenByDescending(m => m.mes)
                        .ThenByDescending(d => d.dia)
                        .GroupBy(v => v.vehiculo)
                        .ToList()
                        .Select(g => new { g.Key, ultimo = g.FirstOrDefault() }).ToList()
                        .Select(ve => ve.ultimo);

                    return consultas.Count() > 0 ? consultas : null;
                }
            }
            else
            {
                if (vehiculoId != "")
                {
                    var consultas = _dataContext.vehiculosGps
                    .Include(v => v.vehiculo)
                    .ThenInclude(u => u.user)
                    .Where(v => v.vehiculo.Placa == vehiculoId)
                    .OrderByDescending(p => p.anio)
                    .ThenByDescending(m => m.mes)
                    .ThenByDescending(d => d.dia)
                    .GroupBy(v => v.vehiculo)
                    .ToList()
                    .Select(g => new { g.Key, ultimo = g.FirstOrDefault() }).ToList()
                    .Select(ve => ve.ultimo);

                    return consultas.Count() > 0 ? consultas : null;


                }
                else
                {
                    var consultas = _dataContext.vehiculosGps
                    .Include(v => v.vehiculo)
                    .ThenInclude(u => u.user)
                    .OrderByDescending(p => p.anio)
                    .ThenByDescending(m => m.mes)
                    .ThenByDescending(d => d.dia)
                    .GroupBy(v => v.vehiculo)
                    .ToList()
                    .Select(g => new { g.Key, ultimo = g.FirstOrDefault() }).ToList()
                    .Select(ve => ve.ultimo);

                    return consultas.Count() > 0 ? consultas : null;

                }
            }





        }
        public async Task InsertaVehiculo(VehiculoGps vehiculoGps)
        {
            await _dataContext.vehiculosGps.AddAsync(vehiculoGps);
            await _dataContext.SaveChangesAsync();
        }

        
    }
}
