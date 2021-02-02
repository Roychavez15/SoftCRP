using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public class LocationWorldRepository : ILocationWorldRepository
    {
        private readonly DataContext _dataContext;
        private readonly IDatosRepository _datosRepository;
        private readonly IVehiculoGpsRepository _vehiculoGpsRepository;
        private readonly IVehiculoProvGpsRepository _vehiculoProvGpsRepository;
        private readonly IIncidenciasRepository _incidenciasRepository;
        private readonly IGamaRepository _gamaRepository;
        private readonly ILogRepository _logRepository;

        public LocationWorldRepository(
            DataContext dataContext,
            IDatosRepository datosRepository,
            IVehiculoGpsRepository vehiculoGpsRepository,
            IVehiculoProvGpsRepository vehiculoProvGpsRepository,
            IIncidenciasRepository incidenciasRepository,
            IGamaRepository gamaRepository,
            ILogRepository logRepository)
        {
            _dataContext = dataContext;
            _datosRepository = datosRepository;
            _vehiculoGpsRepository = vehiculoGpsRepository;
            _vehiculoProvGpsRepository = vehiculoProvGpsRepository;
            _incidenciasRepository = incidenciasRepository;
            _gamaRepository = gamaRepository;
            _logRepository = logRepository;
        }

        public JObject getDataDevices(string token)
        {
            var adminSession = getAdminSession(token).Result;
            var adminUser = getAdminUsers(adminSession, token).Result;
            var deviceIds = getClientDevices(adminUser, token).Result;
            return deviceIds;
        }
        public string getClientId(string token)
        {
            var adminSession = getAdminSession(token).Result;
            var adminUser = getAdminUsers(adminSession, token).Result;
            return adminUser;
        }
        public VehiculoGps getVehicleData(string deviceId, string token, string clientId)
        {
            //var automotors = getAutomotors(deviceId, token).Result;
            //var vehicleId = automotors["id"].Value<string>();
            //var vehicleData = getDataTrips(vehicleId, clientId, token).Result;
            //return vehicleData;
            return null;
        }
        public string getToken()
        {
            return getAuth().Result;
        }
        public async Task<string> getAuth()
        {
            var url = "https://fleet-api.location-world.com/auth/token";
            var values = new Dictionary<string, string>
            {
                  { "username", "molestinam@condelpi.com" },
                  { "password", "fPMnp6tmJlTYIhEC&Letp0@j" },
                 { "realm", "partners" }
            };
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(values));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (HttpClient cliente = new HttpClient())
            {
                using (HttpResponseMessage response = await cliente.PostAsync(url, httpContent))
                {
                    var content = response.Content.ReadAsStringAsync();
                    var resp = (JObject)JsonConvert.DeserializeObject(content.Result);
                    return resp["accessToken"].Value<string>();
                }
            }

        }
        public async Task<JObject> getTokenRefresh(string token)
        {
            var url = "https://fleet-api.location-world.com/auth/refreshtoken";
            var values = new Dictionary<string, string>
            {
                  { "expiredAccessToken", token }
            };
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(values));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (HttpClient cliente = new HttpClient())
            {
                using (HttpResponseMessage response = await cliente.PostAsync(url, httpContent))
                {
                    var content = response.Content.ReadAsStringAsync();
                    var resp = (JObject)JsonConvert.DeserializeObject(content.Result);
                    return resp;
                }
            }

        }
        public async Task<string> getAdminSession(string token)
        {
            var url = "https://fleet-api.location-world.com/admin-sessions";
            var values = new Dictionary<string, string>
            {
                  { "username", "condelpi.api" },
                  { "password", "Condelpi2020.." }
            };
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(values));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (HttpClient cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Add("domain", "fleet");
                cliente.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (HttpResponseMessage response = await cliente.PostAsync(url, httpContent))
                {
                    var content = response.Content.ReadAsStringAsync();
                    var resp = (JObject)JsonConvert.DeserializeObject(content.Result);
                    return resp["userId"].Value<string>();
                }
            }

        }
        public async Task<string> getAdminUsers(string id, string token)
        {
            var url = "https://fleet-api.location-world.com/admin-users";
            var subdomain = "condelpi";
            var urlQuery = url + "/" + id + "/" + subdomain + "/clients?page=0&pageSize=50";
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(""));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (HttpClient cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Add("domain", "fleet");
                cliente.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (HttpResponseMessage response = await cliente.PostAsync(urlQuery, httpContent))
                {
                    var content = response.Content.ReadAsStringAsync();
                    var resp = (JObject)JsonConvert.DeserializeObject(content.Result);
                    var valores = resp["items"];
                    var consorID = "";
                    foreach (var elem in valores)
                    {
                        var nombre = elem["name"].Value<string>();
                        if (nombre.Equals("CONSORCIO DEL PICHINCHA S.A. CONDELPI P."))
                        {
                            consorID = elem["id"].Value<string>();
                        }
                    }
                    return consorID;
                }
            }

        }

        public async Task<JObject> getClientDevices(string id, string token)
        {
            var url = "https://fleet-api.location-world.com/clients";
            var urlQuery = url + "/" + id + "/devices";
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(""));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (HttpClient cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Add("domain", "fleet");
                cliente.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (HttpResponseMessage response = await cliente.GetAsync(urlQuery))
                {
                    var content = response.Content.ReadAsStringAsync();
                    var resp = (JObject)JsonConvert.DeserializeObject(content.Result);
                    return resp;
                }
            }

        }
        public async Task<JObject> getAutomotors(string id, string token)
        {
            var url = "https://fleet-api.location-world.com/automotors";
            var urlQuery = url + "/" + id;
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(""));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (HttpClient cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Add("domain", "fleet");
                cliente.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (HttpResponseMessage response = await cliente.GetAsync(urlQuery))
                {
                    var content = response.Content.ReadAsStringAsync();
                    var resp = (JObject)JsonConvert.DeserializeObject(content.Result);
                    return resp;
                }
            }

        }

        public async Task getDataTrips(Vehiculo vehiculo, string clientId, string token)
        {
            DateTime now = DateTime.Now;
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0);
            var currentDay = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            var desde = firstDayOfMonth.ToString("yyyy-MM-ddTHH:mm:ss");
            var hasta = currentDay.ToString("yyyy-MM-ddTHH:mm:ss");
            var url = "https://fleet-api.location-world.com/devices";
            var urlQuery = url + "/" + vehiculo.gps_id + "/trips?clientId=" + clientId + "&from=" + desde + "&to=" + hasta + "&page=0&pageSize=50";
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(""));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            VehiculoGps vehicleGPS = new VehiculoGps();
            using (HttpClient cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Add("domain", "fleet");
                cliente.DefaultRequestHeaders.Add("subdomain", "condelpi");
                cliente.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (HttpResponseMessage response = await cliente.GetAsync(urlQuery))
                {
                    var content = response.Content.ReadAsStringAsync();
                    var resp = (JObject)JsonConvert.DeserializeObject(content.Result);

                    var valores = resp["items"];

                    var date = "";
                    var day = "";
                    var year = "";
                    var month = "";
                    var trips = 0;
                    var distance = 0M;
                    var sharpAcceleration = 0;
                    var speeding = 0;
                    var hardBreaking = 0;
                    var score = 0M;
                    var longitude = "";
                    var latitude = "";
                    var sharpTurn = 0;

                    if (valores.Count() > 0)
                    {

                        foreach (var elem in valores)
                        {
                            distance += elem["distance"].Value<decimal>();
                            score += elem["score"].Value<decimal>();
                            speeding += elem["overSpeeds"].Value<int>();
                            hardBreaking += elem["suddenBreakings"].Value<int>();
                            sharpAcceleration += elem["harshAccelerations"].Value<int>();
                        }
                        JArray items = (JArray)valores;
                        var dateDB = valores.First["endedOn"].Value<string>();
                        var dateDT = DateTime.ParseExact(dateDB, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        latitude = valores.First["endLatitude"].Value<string>();
                        longitude = valores.First["endLongitude"].Value<string>();
                        //vehicle_id = id;
                        date = dateDT.ToString();
                        day = dateDT.Day.ToString();
                        year = dateDT.Year.ToString();
                        month = dateDT.Month.ToString();
                        trips = items.Count;
                        //score /= trips;
                        //hardBreaking /= trips;
                        //sharpAcceleration /= trips;

                        var datosgps = await _vehiculoGpsRepository.GetVehiculoByDateAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, vehiculo.Id);
                        var vehiculopro = await _vehiculoProvGpsRepository.GetVehiculoByIdAsync(vehiculo.Id);
                        var incidencias = await _incidenciasRepository.GetIncidenciaByNitAsync(vehiculopro.user.Cedula);

                        var conductores = await _datosRepository.GetConductoresAsync(vehiculopro.user.Cedula, vehiculopro.Placa);
                        var talleres = await _datosRepository.GetIngresosTallerAsync(vehiculopro.user.Cedula, vehiculopro.Placa);
                        var siniestros = await _datosRepository.GetSiniestrosAsync(vehiculopro.user.Cedula, vehiculopro.Placa);

                        var sustitutos = await _datosRepository.GetDiasSustitutosAsync(vehiculopro.user.Cedula, vehiculopro.Placa);
                        int diassustitutos = 0;

                        var conductor = await _datosRepository.GetConductorPlacasAsync(vehiculo.Placa);

                        if (sustitutos.Count() > 0)
                        {
                            var strgama = sustitutos.ToList().FirstOrDefault().Gama.ToString();
                            var diassust = sustitutos.ToList().FirstOrDefault().Dias.ToString();
                            var gama = await _gamaRepository.GetGamaByTypeAsync(strgama);
                            if (gama != null)
                            {
                                diassustitutos = gama.Monto * Convert.ToInt32(diassust);
                            }
                        }

                        var suma = 0M;
                        
                        if(incidencias!=null)
                        {
                            suma = incidencias.ExcesoVelocidad * Convert.ToInt32(speeding)
                                + incidencias.FrenazoBrusco * Convert.ToInt32(hardBreaking)
                                + incidencias.AceleracionesBruscas * Convert.ToInt32(sharpAcceleration)
                                + incidencias.GiroBrusco * Convert.ToInt32(sharpTurn); //TODO

                        }
                        else
                        {
                            suma = 0;
                        }


                        if (distance > 0)
                        {
                            score = 100 - (suma / (Convert.ToDecimal(distance) / 100));
                        }

                        if (datosgps == null)
                        {
                            VehiculoGps vehiculoGPS = new VehiculoGps
                            {
                                vehiculo = vehiculo,
                                dia = DateTime.Now.Day,
                                mes = DateTime.Now.Month,
                                anio = DateTime.Now.Year,
                                //kilometerstraveled = Convert.ToInt32(response["odometer"].Value<string>().Replace(".","")),
                                kilometerstraveled = distance,
                                trips = trips,
                                speeding = speeding,
                                hardbraking = hardBreaking,
                                sharpacceleration = sharpAcceleration,
                                sharpturn = 0, ///no tiene
                                latitude = latitude,
                                longitude = longitude,
                                score = score,
                                conductores = conductores.Count() > 0 ? conductores.Sum(c => c.Conductores) : 0,
                                talleres = talleres.Count() > 0 ? talleres.Sum(t => t.Ingresos) : 0,
                                siniestros = siniestros.Count() > 0 ? siniestros.Sum(s => s.Total_Siniestros) : 0,
                                ahorro = sustitutos.Count() > 0 ? diassustitutos : 0,
                                usuario = conductor.ToUpper(),
                            };
                            try
                            {
                                await _vehiculoGpsRepository.CreateAsync(vehiculoGPS);

                            }
                            catch (Exception ex)
                            {
                                var mensaje = ex.Message;
                            }
                        }
                        else
                        {
                            datosgps.kilometerstraveled = distance;
                            datosgps.trips = trips;
                            datosgps.speeding = speeding;
                            datosgps.hardbraking = hardBreaking;
                            datosgps.sharpacceleration = sharpAcceleration;
                            datosgps.sharpturn = 0;
                            datosgps.latitude = latitude;
                            datosgps.longitude = longitude;
                            datosgps.score = score;
                            datosgps.conductores = conductores.Count() > 0 ? conductores.Sum(c => c.Conductores) : 0;
                            datosgps.talleres = talleres.Count() > 0 ? talleres.Sum(t => t.Ingresos) : 0;
                            datosgps.siniestros = siniestros.Count() > 0 ? siniestros.Sum(s => s.Total_Siniestros) : 0;
                            datosgps.ahorro = sustitutos.Count() > 0 ? diassustitutos : 0;
                            datosgps.usuario = conductor.ToUpper();
                            await _vehiculoGpsRepository.UpdateAsync(datosgps);
                        }
                        //return vehicleGPS;

                    }

                }
            }

        }


        public async Task<string> UpdateIdAutomotor(string Id, string Placa)
        {

            var vehiculo = await _dataContext.vehiculos
                .Include(u=>u.user)
                .Where(p => p.Placa == Placa)
                .FirstOrDefaultAsync();
            
            if(vehiculo!=null)
            {
                vehiculo.gps_id = Id;
                _dataContext.vehiculos.Update(vehiculo);
                _dataContext.SaveChanges();
            }

            return "OK";
        }
        public async Task<IEnumerable<Vehiculo>> GetDataVehiculos()
        {
            return await _dataContext.vehiculos
                .Include(u => u.user)
                .Where(p => p.gps_provider.Contains("LOCATION-WORLD") && p.gps_id != null)
                .ToListAsync();


            //foreach(var item in vehiculos)
            //{
            //    await getDataTrips(item, ClientId, token);
            //}
        }
    }
}
