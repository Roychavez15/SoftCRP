using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace SoftCRP.Controllers.LW
{
    public class LWController : Controller
    {
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
        public  VehiculoGps getVehicleData(string deviceId, string token, string clientId)
        {
            var automotors = getAutomotors(deviceId, token).Result;
            var vehicleId = automotors["id"].Value<string>();
            var vehicleData = getDataTrips(vehicleId, clientId, token).Result;
            return vehicleData;
        }
        public string getToken()
        {
            return getAuth().Result;
        }
        async static Task<string> getAuth()
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
        async static Task<JObject> getTokenRefresh(string token)
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
        async static Task<string> getAdminSession(string token)
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
        async static Task<string> getAdminUsers(string id, string token)
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

        async static Task<JObject> getClientDevices(string id, string token)
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
        async static Task<JObject> getAutomotors(string id, string token)
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

        async static Task<VehiculoGps> getDataTrips(string id,string clientId ,string token)
        {
            DateTime now = DateTime.Now;
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0);
            var currentDay = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            var desde = firstDayOfMonth.ToString("yyyy-MM-ddTHH:mm:ss");
            var hasta = currentDay.ToString("yyyy-MM-ddTHH:mm:ss");
            var url = "https://fleet-api.location-world.com/devices";
            var urlQuery = url + "/" + id+ "/trips?clientId="+clientId+"&from="+desde+"&to="+hasta+"&page=0&pageSize=50";
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
                    var consorID = "";
                    var vehicle_id = "";
                    var date = "";
                    var day = "";
                    var year = "";
                    var month = "";
                    var trips = 0;
                    var distance = 0.00;
                    var sharpAcceleration = 0.00;
                    var speeding = 0.00;
                    var hardBreaking = 0.00;
                    var longitude = "";
                    var latitude = "";
                    foreach (var elem in valores)
                    {
                        distance += elem["distance"].Value<double>();
                        speeding += elem["overSpeeds"].Value<double>();
                        hardBreaking += elem["suddenBreakings"].Value<double>();
                        sharpAcceleration += elem["harshAccelerations"].Value<double>();
                    }
                    JArray items = (JArray)valores;
                    var dateDB = valores.First["endedOn"].Value<string>();
                    var dateDT = DateTime.ParseExact(dateDB, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    latitude = valores.First["endLatitude"].Value<string>();
                    longitude = valores.First["endLongitude"].Value<string>();
                    vehicle_id = id;
                    date = dateDT.ToString();
                    day = dateDT.Day.ToString();
                    year = dateDT.Year.ToString();
                    month = dateDT.Month.ToString();
                    trips = items.Count;
                    speeding /= trips;
                    hardBreaking /= trips;
                    sharpAcceleration /= trips;
                    vehicleGPS.longitude = longitude;
                    vehicleGPS.latitude = latitude;
                    vehicleGPS.dia = day;
                    vehicleGPS.mes = month;
                    vehicleGPS.anio = year;
                    vehicleGPS.trips = trips;
                    vehicleGPS.speeding = speeding;
                    vehicleGPS.hardbraking = hardBreaking;
                    vehicleGPS.sharpacceleration = sharpAcceleration;

                    return vehicleGPS;
                }
            }

        }
    }
}
