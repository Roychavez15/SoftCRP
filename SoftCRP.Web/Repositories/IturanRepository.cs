using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace SoftCRP.Web.Repositories
{
    public class IturanRepository : IIturanRepository
    {
        private readonly DataContext _context;
        private readonly IDatosRepository _datosRepository;
        private readonly IVehiculoGpsRepository _vehiculoGpsRepository;
        private readonly IVehiculoProvGpsRepository _vehiculoProvGpsRepository;
        private readonly IIncidenciasRepository _incidenciasRepository;
        private readonly IGamaRepository _gamaRepository;
        private readonly ILogRepository _logRepository;
        WSIturan.LoginInfo loginIT = new WSIturan.LoginInfo();
        WSIturan.OnlineSoapClient cliente = new WSIturan.OnlineSoapClient(WSIturan.OnlineSoapClient.EndpointConfiguration.OnlineSoap12);

        WSIturanNextTrips.LoginInfo loginNT = new WSIturanNextTrips.LoginInfo();
        WSIturanNextTrips.ManagedServicesSoapClient nextTripsClient = new WSIturanNextTrips.ManagedServicesSoapClient(WSIturanNextTrips.ManagedServicesSoapClient.EndpointConfiguration.ManagedServicesSoap12);

        public IturanRepository(
            DataContext context,
            IDatosRepository datosRepository,
            IVehiculoGpsRepository vehiculoGpsRepository,
            IVehiculoProvGpsRepository vehiculoProvGpsRepository,
            IIncidenciasRepository incidenciasRepository,
            IGamaRepository gamaRepository,
            ILogRepository logRepository)
        {
            _context = context;
            _datosRepository = datosRepository;
            _vehiculoGpsRepository = vehiculoGpsRepository;
            _vehiculoProvGpsRepository = vehiculoProvGpsRepository;
            _incidenciasRepository = incidenciasRepository;
            _gamaRepository = gamaRepository;
            _logRepository = logRepository;
        }
        public async Task<WSIturan.CarOnlinePosItemInfo[]> getDataCarsInfo()
        {
            loginIT.Username = "PABLOMOYA";
            loginIT.Password = "123456";
            loginIT.Company = "CONSORCIO PICHINCHA";
            var request = new WSIturan.GetCarsInfoRequest();
            request.LoginInfo = loginIT;
            var infoCarro = await cliente.GetCarsInfoAsync(request.LoginInfo);
            var resultados = infoCarro.GetCarsInfoResult;
            foreach (var info in resultados)
            {
                var vehicle_tool_tip = info.Vehicle_Tool_Tip.Split('#');
                var vehiculo = vehicle_tool_tip[0].Split('=')[1];
                var conductor = vehicle_tool_tip[1].Split('=')[1];
                var carga = vehicle_tool_tip[2].Split('=')[1];
                var hora = vehicle_tool_tip[3].Split('=')[1];
                var servicio = vehicle_tool_tip[4].Split('=')[1];
                var kilometraje = vehicle_tool_tip[8].Split('=')[1];//estaba 5
                var velocidad = vehicle_tool_tip[6].Split('=')[1];
                var aceleracion = vehicle_tool_tip[7].Split('=')[1];
                var satelites = vehicle_tool_tip[8].Split('=')[1];
                var versionHW = vehicle_tool_tip[9].Split('=')[1];
                var versionSW = vehicle_tool_tip[10].Split('=')[1];
                var nivelBateria = vehicle_tool_tip[1].Split('=')[1];
            }
            return resultados;
        }
        public async Task<string> getDataDeviceInfo()
        {
            loginIT.Username = "PABLOMOYA";
            loginIT.Password = "123456";
            loginIT.Company = "CONSORCIO PICHINCHA";
            var request = new WSIturan.GetCarsInfoRequest();
            request.LoginInfo = loginIT;
            var infoCarro = await cliente.GetCarsInfoNewAsync(request.LoginInfo, "");
            var resultados = infoCarro.GetCarsInfoNewResult;
            XmlDocument document = new XmlDocument();
            document.LoadXml(resultados);
            XmlNodeList Datos = document.GetElementsByTagName("VEHICLES");
            if (Datos.Count > 0)
            {
                XmlNodeList lista1 = ((XmlElement)Datos[0]).GetElementsByTagName("V");
                foreach (XmlElement nodo in lista1)
                {
                    //Se obtiene la información del vehículo, mas no la del GPS
                    //Las etiquetas se encuentran en los documentos de estas funciones
                }
            }
            return resultados;
        }

        public async Task<IEnumerable<TripsIturanViewModel>> getTrips()
        {
            loginNT.Username = "pablomoya";
            loginNT.Password = "123456";
            loginNT.Company = "consorcio pichincha";
            var request = new WSIturanNextTrips.GetNextTripsRecordsRequest();
           
            request.LoginInfo = loginNT;
            request.LastRecordID = Convert.ToInt64( LastRecordID());
            var infoNextTrips = await nextTripsClient.GetNextTripsRecordsAsync(request.LoginInfo, request.LastRecordID, 3000, "", DateTime.Parse("2010-01-01"));
            var nextTripsData = infoNextTrips.GetNextTripsRecordsResult;
            XmlDocument document = new XmlDocument();
            document.LoadXml(nextTripsData);
            XmlNodeList Datos = document.GetElementsByTagName("TRIPS");
            List<TripsIturanViewModel> tripsIturanViewModel = new List<TripsIturanViewModel>();
            
            if (Datos.Count > 0)
            {
                XmlNodeList lista1 = ((XmlElement)Datos[0]).GetElementsByTagName("T");
                foreach (XmlElement nodo in lista1)
                {
                    //Se obtiene la información de los viajes de los vehículos... para el modelo VehiculoGps
                    //Las etiquetas se encuentran en los documentos de estas funciones
                    /**
                     *    -Frenazos bruscos = DCO
                         -Excesos de velocidad = OSO
                         -Aceleraciones bruscas = ACO
                         -Giros bruscos = CAO
                         -Conductor = DN
                         -Longitud de fin de viaje = X1
                         -Latitud de fin de viaje = X2
                         -Kilometros = resta de los valores de las etiquetas o1 y o2
                         -Tiempo de fin de viaje = T1
                         -Tipo de viaje = TT
                     */
                    try
                    {


                        TripsIturanViewModel tripsIturanViewModel1 = new TripsIturanViewModel();
                        tripsIturanViewModel1.TID = Convert.ToInt32(verificanodo(nodo, "TID"));
                        tripsIturanViewModel1.VID = verificanodo(nodo, "VID");
                        tripsIturanViewModel1.NN = verificanodo(nodo, "NN");
                        tripsIturanViewModel1.PN = verificanodo(nodo, "PN");
                        tripsIturanViewModel1.T1 = Convert.ToDateTime(verificanodo(nodo, "T1"));
                        tripsIturanViewModel1.T2 = Convert.ToDateTime(verificanodo(nodo, "T2"));
                        tripsIturanViewModel1.DID = verificanodo(nodo, "DID");
                        tripsIturanViewModel1.DN = verificanodo(nodo, "DN");
                        tripsIturanViewModel1.O1 = verificanodo(nodo, "O1");
                        tripsIturanViewModel1.O2 = verificanodo(nodo, "O2");
                        tripsIturanViewModel1.TT = verificanodo(nodo, "TT");
                        tripsIturanViewModel1.OST = verificanodo(nodo, "OST");
                        tripsIturanViewModel1.OSD = verificanodo(nodo, "OSD");
                        tripsIturanViewModel1.OSO = verificanodo(nodo, "OSO");
                        tripsIturanViewModel1.EH1 = verificanodo(nodo, "EH1");
                        tripsIturanViewModel1.EH2 = verificanodo(nodo, "EH2");
                        tripsIturanViewModel1.IDD = verificanodo(nodo, "IDD");
                        tripsIturanViewModel1.IDO = verificanodo(nodo, "IDO");
                        tripsIturanViewModel1.TPS = verificanodo(nodo, "TPS");
                        tripsIturanViewModel1.TPR = verificanodo(nodo, "TPR");
                        tripsIturanViewModel1.ORD = verificanodo(nodo, "ORD");
                        tripsIturanViewModel1.FUS = verificanodo(nodo, "FUS");
                        tripsIturanViewModel1.ACO = verificanodo(nodo, "ACO");
                        tripsIturanViewModel1.DCO = verificanodo(nodo, "DCO"); //
                        tripsIturanViewModel1.CAO = verificanodo(nodo, "CAO");
                        tripsIturanViewModel1.X1 = verificanodo(nodo, "X1");
                        tripsIturanViewModel1.Y1 = verificanodo(nodo, "Y1");
                        tripsIturanViewModel1.AD1 = verificanodo(nodo, "AD1");
                        tripsIturanViewModel1.X2 = verificanodo(nodo, "X2");
                        tripsIturanViewModel1.Y2 = verificanodo(nodo, "Y2");
                        tripsIturanViewModel1.AD2 = verificanodo(nodo, "AD2");

                        tripsIturanViewModel.Add(tripsIturanViewModel1);
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }
            return tripsIturanViewModel.OrderByDescending(o=>o.TID);
        }
        public async Task<IEnumerable<Vehiculo>> GetDataVehiculos()
        {
            return await _context.vehiculos
                .Include(u => u.user)
                .Where(p => p.gps_provider.Contains("CHEVISTAR"))
                .ToListAsync();

        }
        public async Task getDataTrips(Vehiculo vehiculo, string clientId, IturanDatosGps ituranDatosGps, int dia, int mes, int año)
        {
            var datosgps = await _vehiculoGpsRepository.GetVehiculoByDateAsync(dia, mes, año, vehiculo.Id);
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

            var distance = ituranDatosGps.kilometros;
            var suma = 0M;
            var score = 0M;

            if (incidencias!=null)
            {
                suma = incidencias.ExcesoVelocidad * ituranDatosGps.speeding
                    + incidencias.FrenazoBrusco * ituranDatosGps.hardbraking
                    + incidencias.AceleracionesBruscas * ituranDatosGps.sharpacceleration
                + incidencias.GiroBrusco * ituranDatosGps.sharpturn;

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
                    dia = dia,
                    mes = mes,
                    anio = año,
                    //kilometerstraveled = Convert.ToInt32(response["odometer"].Value<string>().Replace(".","")),
                    kilometerstraveled = distance,
                    trips = ituranDatosGps.viajes,
                    speeding = ituranDatosGps.speeding,
                    hardbraking = ituranDatosGps.hardbraking,
                    sharpacceleration = ituranDatosGps.sharpacceleration,
                    sharpturn = ituranDatosGps.sharpturn, ///no tiene
                    latitude = ituranDatosGps.latitud,
                    longitude = ituranDatosGps.longitud,
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
                datosgps.trips = ituranDatosGps.viajes;
                datosgps.speeding = ituranDatosGps.speeding;
                datosgps.hardbraking = ituranDatosGps.hardbraking;
                datosgps.sharpacceleration = ituranDatosGps.sharpacceleration;
                datosgps.sharpturn = ituranDatosGps.sharpturn;
                datosgps.latitude = ituranDatosGps.latitud;
                datosgps.longitude = ituranDatosGps.longitud;
                datosgps.score = score;
                datosgps.conductores = conductores.Count() > 0 ? conductores.Sum(c => c.Conductores) : 0;
                datosgps.talleres = talleres.Count() > 0 ? talleres.Sum(t => t.Ingresos) : 0;
                datosgps.siniestros = siniestros.Count() > 0 ? siniestros.Sum(s => s.Total_Siniestros) : 0;
                datosgps.ahorro = sustitutos.Count() > 0 ? diassustitutos : 0;
                datosgps.usuario = conductor.ToUpper();
                await _vehiculoGpsRepository.UpdateAsync(datosgps);
            }


        }
        private string verificanodo(XmlElement lista1, string nodo)
        {
            var aux = "";
            try
            {
                int conta = lista1.GetElementsByTagName(nodo).Count;
                if (conta != 0)
                {


                    aux = lista1.GetElementsByTagName(nodo)[0].InnerText;
                    if (nodo == "pickup")
                    {
                        if (aux == "")
                        {
                            aux = "NO";
                        }
                    }
                }
                else
                {
                    if (nodo == "pickup")
                    {
                        if (aux == "")
                        {
                            aux = "NO";
                        }
                    }
                }
            }
            catch
            {
                aux = "";
                if (nodo == "pickup")
                {
                    if (aux == "")
                    {
                        aux = "NO";
                    }
                }
            }
            return aux;
        }
        public string LastRecordID()
        {
            return _context.lastRecords.OrderByDescending(d => d.dia).FirstOrDefault().codigo;
        }
        public async Task SaveLastRecord(string codigo)
        {
            await _context.lastRecords.AddAsync(new LastRecordIturan { dia = DateTime.Now, codigo = codigo });
            await _context.SaveChangesAsync();
        }
    }
}
