using SoftCRP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface IDatosRepository
    {

        Task<List<VehiculosClientesViewModel>> GetVehiculosClienteAsync(string nit);

        Task<DatosAuto> GetDatosAutoAsync(string placa);

    }
}
