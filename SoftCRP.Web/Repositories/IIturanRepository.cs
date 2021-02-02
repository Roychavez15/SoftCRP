using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface IIturanRepository
    {
        Task SaveLastRecord(string codigo);
        string LastRecordID();
        Task getDataTrips(Vehiculo vehiculo, string clientId, IturanDatosGps ituranDatosGps, int dia, int mes, int año);
        Task<IEnumerable<Vehiculo>> GetDataVehiculos();
        Task<WSIturan.CarOnlinePosItemInfo[]> getDataCarsInfo();
        Task<string> getDataDeviceInfo();
        Task<IEnumerable<TripsIturanViewModel>> getTrips();
    }
}
