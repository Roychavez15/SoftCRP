using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftCRP.Controllers.Api
{
    public class CGBController : ControllerBase
    {
        public VehiculoGps getPlate(string placa)
        {
            var auth = getAuth().Result;
            var values = new Dictionary<string, string>
            {
                {"plate", placa}
            };
            var data = getDataPlate(auth, values).Result;
            var response = (JObject) JsonConvert.DeserializeObject(data);
            VehiculoGps vehicleData = new VehiculoGps();
            vehicleData.latitude = response["latitude"].Value<string>();
            vehicleData.longitude = response["longitude"].Value<string>();
            vehicleData.trips = response["trips"].Value<int>();
            vehicleData.speeding = response["speeding"].Value<int>();
            vehicleData.hardbraking = response["hardBraking"].Value<int>();
            vehicleData.sharpacceleration = response["sharpAcceleration"].Value<int>();
            vehicleData.sharpturn = response["sharpTurn"].Value<int>();
            return vehicleData;
        }

        async static Task<string> getAuth()
        {
            var url = "http://174.138.125.50:8299/login";
            var values = new Dictionary<string, string>
            {
                  { "username", "admin" },
                  { "password", "Online1234" }
            };
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(values));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (HttpClient cliente = new HttpClient())
            {
                using (HttpResponseMessage response = await cliente.PostAsync(url, httpContent))
                {
                    HttpHeaders contenido = response.Headers;
                    var valor = contenido.GetValues("Authorization").First();
                    return valor;

                }
            }

        }
        async static Task<string> getDataPlate(string auth,Dictionary<string,string> placa)
        {
            var url = "http://174.138.125.50:8299/api/statistics";
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(placa));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Add("Authorization", auth);
                using (HttpResponseMessage response = await cliente.PostAsync(url, httpContent))
                {
                var result = await response.Content.ReadAsStringAsync();
                return result;
                }
            
        }
    }
}
