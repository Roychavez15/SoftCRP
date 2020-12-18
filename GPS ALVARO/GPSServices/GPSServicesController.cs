using Microsoft.AspNetCore.Mvc;
using SoftCRP.Controllers.Api;
using SoftCRP.Controllers.LW;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftCRP.Web.Controllers.GPSServices
{
    [Route("api/[controller]")]
    [ApiController]
    public class GPSServicesController : Controller
    {
        DataContext _context;
        public GPSServicesController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("{plate}")]
        public string Get(string plate)
        {
            var datalist = _context.vehiculos.Where(p => p.Placa == plate);
            var count = datalist.Count<Vehiculo>();
            if (count == 1)
            {
                var plateData = datalist.First<Vehiculo>();
                var provider = plateData.gps_provider;
                var deviceId = plateData.gps_id;
                var vehicle = new VehiculoGps();
                if (provider.Equals("CGB"))
                {
                    CGBController cgb = new CGBController();
                    vehicle = cgb.getPlate(plate);
                    _context.vehiculosGps.Add(vehicle);
                    _context.SaveChanges();
                }
                else if (provider.Equals("LOCATION-WORLD"))
                {
                    LWController lw = new LWController();
                    
                    var token = lw.getToken();
                    var clientId = lw.getClientId(token);
                    lw.getDataDevices(token);
                    var obj = lw.getVehicleData(deviceId, token,clientId);
                }
                else if (provider.Contains("CHEVISTAR"))
                {

                }
            }
            //foreach (var elem in datalist)
            //{
            //    Console.WriteLine(elem);
            //}

            return "";
        }

    }
}
