using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface IDatosRepository
    {
        Task<string> ProcesoCGB();
        Task<VehiculoProvGpsViewModel> GetDatosAutoProvGpsAsync(string nit, string placa);
        Task<List<VehiculosClientesViewModel>> GetVehiculosClienteAsync(string nit);

        Task<IEnumerable<VehiculosClientesViewModel>> GetPlacasClienteAsync(string nit);
        
        Task<DatosAuto> GetDatosAutoAsync(string placa);

        Task<ClienteViewModel> GetDatosCliente(string ruc);

        Task<List<EstadoIncidenciaViewModel>> GetEstadosIncidenciaAsync();
        Task<List<ViaIngresoViewModel>> GetViaIngresoAsync();
        Task<List<SubMotivosIncidenciasViewModel>> GetSubMotivosIncidenciasAsync();
        Task<List<TiposIncidenciaViewModel>> GetTipoIncidenciasAsync();

        Task<IEnumerable<DatosAuto>> GetDatosAutoAllAsync();
        Task<bool> IngresoIncidencia(IncidenciaCreateViewModel novedad);

        Task<TiposIncidenciaViewModel> GetTipoIncidenciaIdAsync(string tipo);
        Task<SubMotivosIncidenciasViewModel> GetSubMotivosIncidenciaIdAsync(string motivo);
    }
}
