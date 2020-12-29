using Newtonsoft.Json.Linq;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public interface ILocationWorldRepository
    {
        Task<IEnumerable<Vehiculo>> GetDataVehiculos();
        Task<string> UpdateIdAutomotor(string Id, string Placa);
        JObject getDataDevices(string token);
        string getClientId(string token);
        VehiculoGps getVehicleData(string deviceId, string token, string clientId);
        string getToken();
        Task<JObject> getTokenRefresh(string token);
        Task<string> getAdminSession(string token);
        Task<string> getAdminUsers(string id, string token);
        Task<JObject> getClientDevices(string id, string token);
        Task<JObject> getAutomotors(string id, string token);
        Task getDataTrips(Vehiculo vehiculo, string clientId, string token);

    }
}
