using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;

namespace SoftCRP.Web.Repositories
{
    public class DatosRepository : IDatosRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly WSDLCondelpiData.Service1Soap _service1Soap;
        private readonly IUserHelper _userHelper;
        private readonly IVehiculoProvGpsRepository _vehiculoProvGpsRepository;
        private readonly IVehiculoGpsRepository _vehiculoGpsRepository;
        private readonly IIncidenciasRepository _incidenciasRepository;
        private readonly ILogRepository _logRepository;
        private readonly IGamaRepository _gamaRepository;


        public DatosRepository(
            DataContext context,
            IConfiguration configuration,
            WSDLCondelpiData.Service1Soap service1Soap,
            IUserHelper userHelper,
            IVehiculoProvGpsRepository vehiculoProvGpsRepository,
            IVehiculoGpsRepository vehiculoGpsRepository,
            IIncidenciasRepository incidenciasRepository,
            ILogRepository logRepository,
            IGamaRepository gamaRepository)
        {
            _context = context;
            _configuration = configuration;
            _service1Soap = service1Soap;
            _userHelper = userHelper;
            _vehiculoProvGpsRepository = vehiculoProvGpsRepository;
            _vehiculoGpsRepository = vehiculoGpsRepository;
            _incidenciasRepository = incidenciasRepository;
            _logRepository = logRepository;
            _gamaRepository = gamaRepository;

        }
        public async Task<DatosAuto> GetDatosAutoAsync(string placa)
        {
            //throw new NotImplementedException();
            DatosAuto datos = new DatosAuto();

            var key = _configuration["KeyWs"];
            
            var dataxml = await _service1Soap.Consulta_Data_autoAsync(key, placa);
            if (dataxml != null)
            {


                XmlDocument document = new XmlDocument();

                document.LoadXml(dataxml.Nodes[1].ToString());
                XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

                if (Datos.Count > 0)
                {
                    XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                    foreach (XmlElement nodo in lista1)
                    {
                        datos = new DatosAuto
                        {
                            Adendum = verificanodo(nodo, "adendum"),
                            Año = verificanodo(nodo, "año"),
                            Canon = verificanodo(nodo, "canon"),
                            Chasis = verificanodo(nodo, "chasis"),
                            Ciudad_operacion = verificanodo(nodo, "ciudad_operacion"),
                            Clase = verificanodo(nodo, "clase"),
                            Cliente = verificanodo(nodo, "cliente"),
                            Color = verificanodo(nodo, "color"),
                            Contrato = verificanodo(nodo, "contrato"),
                            Cotizacion = verificanodo(nodo, "cotizacion"),
                            Des_modelo = verificanodo(nodo, "des_modelo"),
                            Dispositivo = verificanodo(nodo, "dispositivo"),
                            Ejecutivo = verificanodo(nodo, "ejecutivo"),
                            Estatus = verificanodo(nodo, "estatus"),
                            Fechacontrato = verificanodo(nodo, "fechacontrato"),
                            FechafinContrato = verificanodo(nodo, "fechafinContrato"),
                            Fecha_entrega = verificanodo(nodo, "fecha_entrega"),
                            Fecha_km = verificanodo(nodo, "fecha_km"),
                            Fecha_ultima_rutina = verificanodo(nodo, "fecha_ultima_rutina"),
                            FormaFacturacion = verificanodo(nodo, "FormaFacturacion"),

                            Id_ultima_rutina = verificanodo(nodo, "id_ultima_rutina"),
                            Km = verificanodo(nodo, "km"),
                            KmAnual = verificanodo(nodo, "KmAnual"),
                            Marca = verificanodo(nodo, "marca"),
                            Modelo = verificanodo(nodo, "modelo"),
                            Motor = verificanodo(nodo, "motor"),
                            Mto_correctivo = verificanodo(nodo, "mto_correctivo"),
                            Mto_llantas = verificanodo(nodo, "mto_llantas"),
                            Mto_preventivo = verificanodo(nodo, "mto_preventivo"),
                            Mto_sustituto = verificanodo(nodo, "mto_sustituto"),
                            NombreAseguradora = verificanodo(nodo, "NombreAseguradora"),
                            Nom_cliente = verificanodo(nodo, "nom_cliente"),
                            Nom_ejecutivo = verificanodo(nodo, "nom_ejecutivo"),
                            Placa = verificanodo(nodo, "placa"),
                            Plan_Seguro = verificanodo(nodo, "Plan_Seguro"),
                            Plazo = verificanodo(nodo, "Plazo"),
                            Plazo_pago = verificanodo(nodo, "Plazo_pago"),
                            Ramv = verificanodo(nodo, "ramv"),
                            Siniestros = verificanodo(nodo, "siniestros"),
                            Ultima_rutina = verificanodo(nodo, "ultima_rutina"),
                            pickup = verificanodo(nodo, "pickup"),

                        };

                    }
                }
            }
            return datos;
        }

        public async Task<IEnumerable<DatosAuto>> GetDatosAutoAllAsync()
        {
            //throw new NotImplementedException();
            List<DatosAuto> datos = new List<DatosAuto>();

            var key = _configuration["KeyWs"];

            var dataxml = await _service1Soap.Get_consulta_totalAsync(key);
            if (dataxml != null)
            {
                XmlDocument document = new XmlDocument();

                document.LoadXml(dataxml.Nodes[1].ToString());
                XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

                if (Datos.Count > 0)
                {
                    XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                    foreach (XmlElement nodo in lista1)
                    {
                        var auto = new DatosAuto
                        {
                            Adendum = verificanodo(nodo, "adendum"),
                            Año = verificanodo(nodo, "año"),
                            Canon = verificanodo(nodo, "canon"),
                            Chasis = verificanodo(nodo, "chasis"),
                            Ciudad_operacion = verificanodo(nodo, "ciudad_operacion"),
                            Clase = verificanodo(nodo, "clase"),
                            Cliente = verificanodo(nodo, "cliente"),
                            Color = verificanodo(nodo, "color"),
                            Contrato = verificanodo(nodo, "contrato"),
                            Cotizacion = verificanodo(nodo, "cotizacion"),
                            Des_modelo = verificanodo(nodo, "des_modelo"),
                            Dispositivo = verificanodo(nodo, "dispositivo"),
                            Ejecutivo = verificanodo(nodo, "ejecutivo"),
                            Estatus = verificanodo(nodo, "estatus"),
                            Fechacontrato = verificanodo(nodo, "fechacontrato"),
                            FechafinContrato = verificanodo(nodo, "fechafinContrato"),
                            Fecha_entrega = verificanodo(nodo, "fecha_entrega"),
                            Fecha_km = verificanodo(nodo, "fecha_km"),
                            Fecha_ultima_rutina = verificanodo(nodo, "fecha_ultima_rutina"),
                            FormaFacturacion = verificanodo(nodo, "FormaFacturacion"),

                            Id_ultima_rutina = verificanodo(nodo, "id_ultima_rutina"),
                            Km = verificanodo(nodo, "km"),
                            KmAnual = verificanodo(nodo, "KmAnual"),
                            Marca = verificanodo(nodo, "marca"),
                            Modelo = verificanodo(nodo, "modelo"),
                            Motor = verificanodo(nodo, "motor"),
                            Mto_correctivo = verificanodo(nodo, "mto_correctivo"),
                            Mto_llantas = verificanodo(nodo, "mto_llantas"),
                            Mto_preventivo = verificanodo(nodo, "mto_preventivo"),
                            Mto_sustituto = verificanodo(nodo, "mto_sustituto"),
                            NombreAseguradora = verificanodo(nodo, "NombreAseguradora"),
                            Nom_cliente = verificanodo(nodo, "nom_cliente"),
                            Nom_ejecutivo = verificanodo(nodo, "nom_ejecutivo"),
                            Placa = verificanodo(nodo, "placa"),
                            Plan_Seguro = verificanodo(nodo, "Plan_Seguro"),
                            Plazo = verificanodo(nodo, "Plazo"),
                            Plazo_pago = verificanodo(nodo, "Plazo_pago"),
                            Ramv = verificanodo(nodo, "ramv"),
                            Siniestros = verificanodo(nodo, "siniestros"),
                            Ultima_rutina = verificanodo(nodo, "ultima_rutina"),
                            pickup = verificanodo(nodo, "pickup"),

                        };

                        //VehiculoProvGpsViewModel vehiculoProvGps = await GetDatosAutoProvGpsAsync(auto.Cliente, auto.Placa);
                        
                        //var particularidades = await GetParticularidadesAsync(auto.Placa);

                        datos.Add(auto);
                    }
                }
            }

            return datos.Where(e => e.Estatus == "VIGENTE CON CONTRATO" || e.Estatus=="STAND BY");
        }

        public async Task<IEnumerable<DatosAuto>> GetDatosAutoAllGpsAsync()
        {
            //throw new NotImplementedException();
            List<DatosAuto> datos = new List<DatosAuto>();

            var key = _configuration["KeyWs"];

            var dataxml = await _service1Soap.Get_consulta_totalAsync(key);
            
            if (dataxml != null)
            {
                XmlDocument document = new XmlDocument();

                document.LoadXml(dataxml.Nodes[1].ToString());
                XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

                if (Datos.Count > 0)
                {
                    XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                    foreach (XmlElement nodo in lista1)
                    {
                        var auto = new DatosAuto
                        {
                            Adendum = verificanodo(nodo, "adendum"),
                            Año = verificanodo(nodo, "año"),
                            Canon = verificanodo(nodo, "canon"),
                            Chasis = verificanodo(nodo, "chasis"),
                            Ciudad_operacion = verificanodo(nodo, "ciudad_operacion"),
                            Clase = verificanodo(nodo, "clase"),
                            Cliente = verificanodo(nodo, "cliente"),
                            Color = verificanodo(nodo, "color"),
                            Contrato = verificanodo(nodo, "contrato"),
                            Cotizacion = verificanodo(nodo, "cotizacion"),
                            Des_modelo = verificanodo(nodo, "des_modelo"),
                            Dispositivo = verificanodo(nodo, "dispositivo"),
                            Ejecutivo = verificanodo(nodo, "ejecutivo"),
                            Estatus = verificanodo(nodo, "estatus"),
                            Fechacontrato = verificanodo(nodo, "fechacontrato"),
                            FechafinContrato = verificanodo(nodo, "fechafinContrato"),
                            Fecha_entrega = verificanodo(nodo, "fecha_entrega"),
                            Fecha_km = verificanodo(nodo, "fecha_km"),
                            Fecha_ultima_rutina = verificanodo(nodo, "fecha_ultima_rutina"),
                            FormaFacturacion = verificanodo(nodo, "FormaFacturacion"),

                            Id_ultima_rutina = verificanodo(nodo, "id_ultima_rutina"),
                            Km = verificanodo(nodo, "km"),
                            KmAnual = verificanodo(nodo, "KmAnual"),
                            Marca = verificanodo(nodo, "marca"),
                            Modelo = verificanodo(nodo, "modelo"),
                            Motor = verificanodo(nodo, "motor"),
                            Mto_correctivo = verificanodo(nodo, "mto_correctivo"),
                            Mto_llantas = verificanodo(nodo, "mto_llantas"),
                            Mto_preventivo = verificanodo(nodo, "mto_preventivo"),
                            Mto_sustituto = verificanodo(nodo, "mto_sustituto"),
                            NombreAseguradora = verificanodo(nodo, "NombreAseguradora"),
                            Nom_cliente = verificanodo(nodo, "nom_cliente"),
                            Nom_ejecutivo = verificanodo(nodo, "nom_ejecutivo"),
                            Placa = verificanodo(nodo, "placa"),
                            Plan_Seguro = verificanodo(nodo, "Plan_Seguro"),
                            Plazo = verificanodo(nodo, "Plazo"),
                            Plazo_pago = verificanodo(nodo, "Plazo_pago"),
                            Ramv = verificanodo(nodo, "ramv"),
                            Siniestros = verificanodo(nodo, "siniestros"),
                            Ultima_rutina = verificanodo(nodo, "ultima_rutina"),
                            pickup = verificanodo(nodo, "pickup"),

                        };

                        VehiculoProvGpsViewModel vehiculoProvGps = await GetDatosAutoProvGpsAsync(auto.Cliente, auto.Placa);

                        if (vehiculoProvGps != null)
                        {
                            if (vehiculoProvGps.Gps.Trim() != "No tiene dispositivo satelital")
                            {
                                Vehiculo vehiculo = new Vehiculo();

                                var usuario = await _userHelper.GetUserByCedulaAsync(auto.Cliente);

                                if (usuario != null)
                                {
                                    var encontrar = await _vehiculoProvGpsRepository.GetVehiculoByClientePlacaAsync(usuario.Id, auto.Placa);

                                    if (encontrar == null)
                                    {
                                        vehiculo.user = usuario;
                                        vehiculo.Placa = auto.Placa;
                                        vehiculo.gps_provider = vehiculoProvGps.Gps.Trim();
                                        
                                        if (vehiculoProvGps.Gps.Trim() != "LOCATION-WORLD")
                                        {
                                            vehiculo.gps_id = vehiculoProvGps.Gps.Trim();
                                        }
                                        else
                                        {
                                            //var token = _locationWorldRepository.getToken();
                                            //var dispositivos = _locationWorldRepository.getDataDevices(token);
                                            vehiculo.gps_id = null;
                                        }

                                        try
                                        {
                                            //_context.vehiculos.Add(vehiculo);
                                            await _vehiculoProvGpsRepository.CreateAsync(vehiculo);
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                    }
                                    else //para id de location world
                                    {

                                    }
                                }
                            }
                        }



                        datos.Add(auto);
                    }
                }


            }
            return datos.Where(e => e.Estatus == "VIGENTE CON CONTRATO" || e.Estatus == "STAND BY");
        }
        public async Task<VehiculoProvGpsViewModel> GetDatosAutoProvGpsAsync(string nit, string placa)
        {
            var key = _configuration["KeyWs"];

            var dataxml = await _service1Soap.WS_GPS_PLACAAsync(key, placa);

            if(dataxml!=null)
            { 

                XmlDocument document = new XmlDocument();

                document.LoadXml(dataxml.Nodes[1].ToString());
                XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

                if (Datos.Count > 0)
                {
                    XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                    foreach (XmlElement nodo in lista1)
                    {
                        var Responsable = verificanodo(nodo, "Responsable");
                        var Cargo = verificanodo(nodo, "Cargo");
                        var Placa = verificanodo(nodo, "Placa");
                        var ESTADO = verificanodo(nodo, "ESTADO");
                        var GPS = verificanodo(nodo, "GPS");
                        var ESTATUS = verificanodo(nodo, "ESTATUS");

                        var vehiculoGps = new VehiculoProvGpsViewModel
                        {
                            Responsable = Responsable,
                            Cargo = Cargo,
                            Placa = Cargo,
                            Estado = ESTADO,
                            Gps = GPS,
                            Estatus = ESTATUS
                        };
                        return vehiculoGps;
                    }                
                }
            }
            return null;
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
            catch {
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
        public async Task<List<VehiculosClientesViewModel>> GetVehiculosClienteAsync(string nit)
        {
            var key = _configuration["KeyWs"];
            List<VehiculosClientesViewModel> Vehiculos = new List<VehiculosClientesViewModel>();

            var dataxml = await _service1Soap.Consulta_Data_nit_autoAsync(key, nit);
            
            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if(Datos.Count>0)
            { 
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {
                    //var dat= nodo[0].InnerText
                    XmlNodeList codigo_activo =
                        nodo.GetElementsByTagName("codigo_activo");

                    XmlNodeList nom_cliente =
                        nodo.GetElementsByTagName("nom_cliente");

                    XmlNodeList nit_cliente =
                        nodo.GetElementsByTagName("nit_cliente");

                    XmlNodeList placa =
                        nodo.GetElementsByTagName("placa");

                    XmlNodeList historial_vh =
                        nodo.GetElementsByTagName("historial_vh");

                    VehiculosClientesViewModel vehiculos = new VehiculosClientesViewModel();

                    vehiculos.codigo_activo = codigo_activo[0].InnerText;
                    vehiculos.nom_cliente = nom_cliente[0].InnerText;
                    vehiculos.nit_cliente = nit_cliente[0].InnerText;
                    vehiculos.placa = placa[0].InnerText;
                    vehiculos.historial_vh = historial_vh[0].InnerText;
                    vehiculos.historial_pr = HistorialVehiculos(nit_cliente[0].InnerText, placa[0].InnerText);

                    Vehiculos.Add(vehiculos);
                }
                
            }
            if(nit!= "1791287835001")
            {
                return Vehiculos
                .Where(s => s.historial_vh == "VIGENTE CON CONTRATO" || s.historial_vh == "STAND BY")
                .OrderBy(o => o.codigo_activo)
                .ToList();
                        }
            else
            {
                return Vehiculos
                .Where(s => s.historial_vh != "VIGENTE CON CONTRATO" && s.historial_vh != "STAND BY")
                .OrderBy(o => o.codigo_activo)
                .ToList();
            }
            
        } 


        public async Task<ClienteViewModel> GetDatosCliente(string ruc)
        {
            ClienteViewModel cliente = new ClienteViewModel();

            var key = _configuration["KeyWs"];
            var dataxml = await _service1Soap.Consulta_clientesAsync(key, ruc);
            if(dataxml==null)
            {
                return null;
            }
            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {
                    cliente.nit = nodo.GetElementsByTagName("nit_cliente")[0].InnerText;
                    cliente.nombre = nodo.GetElementsByTagName("nom_cliente")[0].InnerText;
                    cliente.correo = nodo.GetElementsByTagName("correo")[0].InnerText;
                    cliente.correo_factura = nodo.GetElementsByTagName("correo_facturacion")[0].InnerText;
                }
            }

            return cliente;
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<VehiculosClientesViewModel>> GetPlacasClienteAsync(string nit)
        {
            var key = _configuration["KeyWs"];
            List<VehiculosClientesViewModel> Vehiculos = new List<VehiculosClientesViewModel>();

            var dataxml = await _service1Soap.Consulta_Data_nit_autoAsync(key, nit);
            
            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {
                    //var dat= nodo[0].InnerText
                    XmlNodeList codigo_activo =
                        nodo.GetElementsByTagName("codigo_activo");

                    XmlNodeList nom_cliente =
                        nodo.GetElementsByTagName("nom_cliente");

                    XmlNodeList nit_cliente =
                        nodo.GetElementsByTagName("nit_cliente");

                    XmlNodeList placa =
                        nodo.GetElementsByTagName("placa");

                    XmlNodeList historial_vh =
                        nodo.GetElementsByTagName("historial_vh");

                    VehiculosClientesViewModel vehiculos = new VehiculosClientesViewModel();

                    vehiculos.codigo_activo = codigo_activo[0].InnerText;
                    vehiculos.nom_cliente = nom_cliente[0].InnerText;
                    vehiculos.nit_cliente = nit_cliente[0].InnerText;
                    vehiculos.placa = placa[0].InnerText;
                    vehiculos.historial_vh = historial_vh[0].InnerText;

                    Vehiculos.Add(vehiculos);
                }

            }

            return Vehiculos
                .Where(s => s.historial_vh == "VIGENTE CON CONTRATO" || s.historial_vh == "STAND BY")
                .OrderBy(o => o.codigo_activo);

        }

        public async Task<List<EstadoIncidenciaViewModel>> GetEstadosIncidenciaAsync()
        {
            var key = _configuration["KeyWs"];
            List<EstadoIncidenciaViewModel> Estados = new List<EstadoIncidenciaViewModel>();

            var dataxml = await _service1Soap.Estado_incidenciaAsync(key);

            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {
                    //var dat= nodo[0].InnerText
                    XmlNodeList id =
                        nodo.GetElementsByTagName("id");

                    XmlNodeList valor =
                        nodo.GetElementsByTagName("valor");

                    EstadoIncidenciaViewModel estado = new EstadoIncidenciaViewModel();

                    estado.Id = int.Parse(id[0].InnerText);
                    estado.Estado = valor[0].InnerText;


                    Estados.Add(estado);
                }

            }

            return Estados
                .OrderBy(o => o.Estado)
                .ToList();

        }
        public async Task<List<ViaIngresoViewModel>> GetViaIngresoAsync()
        {
            var key = _configuration["KeyWs"];
            List<ViaIngresoViewModel> Estados = new List<ViaIngresoViewModel>();

            var dataxml = await _service1Soap.Via_IngresoAsync(key);

            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {
                    //var dat= nodo[0].InnerText
                    XmlNodeList id =
                        nodo.GetElementsByTagName("id");

                    XmlNodeList valor =
                        nodo.GetElementsByTagName("valor");

                    ViaIngresoViewModel estado = new ViaIngresoViewModel();

                    estado.Id = int.Parse(id[0].InnerText);
                    estado.Estado = valor[0].InnerText;


                    Estados.Add(estado);
                }

            }

            return Estados
                .OrderBy(o => o.Estado)
                .ToList();

        }

        public async Task<List<SubMotivosIncidenciasViewModel>> GetSubMotivosIncidenciasAsync()
        {
            var key = _configuration["KeyWs"];
            List<SubMotivosIncidenciasViewModel> Estados = new List<SubMotivosIncidenciasViewModel>();

            var dataxml = await _service1Soap.Responsable_slaAsync(key);

            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {
                    //var dat= nodo[0].InnerText
                    XmlNodeList id =
                        nodo.GetElementsByTagName("id");

                    XmlNodeList submotivo =
                        nodo.GetElementsByTagName("submotivo");

                    XmlNodeList usuario_asesor =
                        nodo.GetElementsByTagName("usuario_asesor");

                    XmlNodeList dias_sla =
                        nodo.GetElementsByTagName("dias_sla");

                    XmlNodeList correo_solucionadores =
                        nodo.GetElementsByTagName("correo_solucionadores");

                    SubMotivosIncidenciasViewModel estado = new SubMotivosIncidenciasViewModel();

                    estado.Id = int.Parse(id[0].InnerText);
                    estado.Submotivo = submotivo[0].InnerText;
                    estado.Usuario_asesor = usuario_asesor[0].InnerText;
                    estado.Dias_sla = int.Parse(dias_sla[0].InnerText);
                    estado.Correo_solucionadores = correo_solucionadores[0].InnerText;

                    Estados.Add(estado);
                }

            }

            return Estados
                .OrderBy(o => o.Submotivo)
                .ToList();

        }

        public async Task<SubMotivosIncidenciasViewModel> GetSubMotivosIncidenciaIdAsync(string motivo)
        {
            var key = _configuration["KeyWs"];
            List<SubMotivosIncidenciasViewModel> Estados = new List<SubMotivosIncidenciasViewModel>();

            var dataxml = await _service1Soap.Responsable_slaAsync(key);

            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {
                    //var dat= nodo[0].InnerText
                    XmlNodeList id =
                        nodo.GetElementsByTagName("id");

                    XmlNodeList submotivo =
                        nodo.GetElementsByTagName("submotivo");

                    XmlNodeList usuario_asesor =
                        nodo.GetElementsByTagName("usuario_asesor");

                    XmlNodeList dias_sla =
                        nodo.GetElementsByTagName("dias_sla");

                    XmlNodeList correo_solucionadores =
                        nodo.GetElementsByTagName("correo_solucionadores");

                    SubMotivosIncidenciasViewModel estado = new SubMotivosIncidenciasViewModel();

                    estado.Id = int.Parse(id[0].InnerText);
                    estado.Submotivo = submotivo[0].InnerText;
                    estado.Usuario_asesor = usuario_asesor[0].InnerText;
                    estado.Dias_sla = int.Parse(dias_sla[0].InnerText);
                    estado.Correo_solucionadores = correo_solucionadores[0].InnerText;

                    Estados.Add(estado);
                }

            }

            return Estados
                .Where(s => s.Submotivo == motivo)
                .FirstOrDefault();
        }

        public async Task<List<TiposIncidenciaViewModel>> GetTipoIncidenciasAsync()
        {
            var key = _configuration["KeyWs"];
            List<TiposIncidenciaViewModel> Estados = new List<TiposIncidenciaViewModel>();

            var dataxml = await _service1Soap.Tipo_incidenciaAsync(key);

            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {
                    //var dat= nodo[0].InnerText
                    XmlNodeList id =
                        nodo.GetElementsByTagName("id");

                    XmlNodeList valor =
                        nodo.GetElementsByTagName("valor");

                    TiposIncidenciaViewModel estado = new TiposIncidenciaViewModel();

                    estado.Id = int.Parse(id[0].InnerText);
                    estado.Tipo = valor[0].InnerText;


                    Estados.Add(estado);
                }

            }

            return Estados
                .OrderBy(o => o.Tipo)
                .ToList();

        }

        public async Task<TiposIncidenciaViewModel> GetTipoIncidenciaIdAsync(string tipo)
        {
            var key = _configuration["KeyWs"];
            List<TiposIncidenciaViewModel> Estados = new List<TiposIncidenciaViewModel>();

            var dataxml = await _service1Soap.Tipo_incidenciaAsync(key);

            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {
                    //var dat= nodo[0].InnerText
                    XmlNodeList id =
                        nodo.GetElementsByTagName("id");

                    XmlNodeList valor =
                        nodo.GetElementsByTagName("valor");

                    TiposIncidenciaViewModel estado = new TiposIncidenciaViewModel();

                    estado.Id = int.Parse(id[0].InnerText);
                    estado.Tipo = valor[0].InnerText;


                    Estados.Add(estado);
                }

            }

            return Estados                
                .Where(t=>t.Tipo==tipo)
                .FirstOrDefault();

        }

        public async Task<bool> IngresoIncidencia(IncidenciaCreateViewModel novedad)
        {
            var key = _configuration["KeyWs"];
            return await _service1Soap.Ingreso_IncidenciasAsync(key,novedad.Placa,novedad.submotivo,novedad.observacion,novedad.usuario,novedad.usuario_solucion,novedad.motivo);


            //return true;
        }

        private int HistorialVehiculos (string nit, string placa)
        {
            int total = 0;

            total = total + _context.Analises.Where(a => a.Placa == placa && a.Cedula != nit).Count();
            total = total + _context.tramites.Where(a => a.Placa == placa && a.Cedula != nit).Count();
            total = total + _context.novedades.Where(a => a.Placa == placa && a.Cedula != nit).Count();


            return total;
        }
        public async Task<string> ProcesoCGB()
        {
            var key = _configuration["cgb"];

            var url = key + "/login";
            var auth = getAuth(url).Result;

            var vehiculosCGB = await _vehiculoProvGpsRepository.GetVehiculosGCBAsync();
            foreach(var item in vehiculosCGB)
            {
                var proc= await getPlate(item.Placa, item.Id, auth);
                await _logRepository.SaveLogsGPS("Success", "Insertar Placa CGB " + item.Placa, "CGB", item.user.UserName);
            }
            //await _logRepository.SaveLogs("Success", "Proceso Completo CGB", "CGB", "");
            return "Procesado";
        }

        private async Task<string> getPlate(string placa, int vehiculoId, string auth)
        {
            var key = _configuration["cgb"];

            //var url = key+"/login";
            var url2 = key+"/api/statistics";
            //var auth = getAuth(url).Result;
            var values = new Dictionary<string, string>
            {
                {"plate", placa}
            };
            var data = getDataPlate(auth, url2, values).Result;
            var response = (JObject)JsonConvert.DeserializeObject(data);

            if(response!=null)
            {
                try
                {


                    var datosgps = await _vehiculoGpsRepository.GetVehiculoByDateAsync(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, vehiculoId);
                    var vehiculopro = await _vehiculoProvGpsRepository.GetVehiculoByIdAsync(vehiculoId);
                    var incidencias = await _incidenciasRepository.GetIncidenciaByNitAsync(vehiculopro.user.Cedula);

                    var conductores = await GetConductoresAsync(vehiculopro.user.Cedula, vehiculopro.Placa);
                    var talleres = await GetIngresosTallerAsync(vehiculopro.user.Cedula, vehiculopro.Placa);
                    var siniestros = await GetSiniestrosAsync(vehiculopro.user.Cedula, vehiculopro.Placa);

                    var sustitutos = await GetDiasSustitutosAsync(vehiculopro.user.Cedula, vehiculopro.Placa);
                    int diassustitutos = 0;

                    var conductor = await GetConductorPlacasAsync(placa);

                    if (sustitutos.Count()>0)
                    {
                        var strgama = sustitutos.ToList().FirstOrDefault().Gama.ToString();
                        var diassust = sustitutos.ToList().FirstOrDefault().Dias.ToString();
                        var gama = await _gamaRepository.GetGamaByTypeAsync(strgama);
                        if(gama!=null)
                        {
                            diassustitutos = gama.Monto * Convert.ToInt32(diassust);
                        }
                    }

                    var suma = 0M;
                    if(incidencias!=null)
                    {
                        suma = incidencias.ExcesoVelocidad * Convert.ToInt32(response["speeding"].Value<string>())
                            + incidencias.FrenazoBrusco * Convert.ToInt32(response["hardBraking"].Value<string>())
                            + incidencias.AceleracionesBruscas * Convert.ToInt32(response["sharpAcceleration"].Value<string>())
                            + incidencias.GiroBrusco * Convert.ToInt32(response["sharpTurn"].Value<string>());

                    }

                    var score = 0M;

                    if (Convert.ToDecimal(response["odometer"].Value<string>().Replace(".", ","))>0)
                    {
                        score = 100 - (suma / (Convert.ToDecimal(response["odometer"].Value<string>().Replace(".", ",")) / 100));
                    }
                    //var kilo = Convert.ToDecimal(response["odometer"].Value<string>().Replace(".", ",")) / 100;
                   
                    if (datosgps == null)
                    {
                        VehiculoGps vehiculo = new VehiculoGps
                        {
                            vehiculo = vehiculopro,
                            dia = DateTime.Now.Day,
                            mes = DateTime.Now.Month,
                            anio = DateTime.Now.Year,
                            //kilometerstraveled = Convert.ToInt32(response["odometer"].Value<string>().Replace(".","")),
                            kilometerstraveled = Convert.ToDecimal(response["odometer"].Value<string>().Replace(".", ",")),
                            trips = Convert.ToInt32(response["trips"].Value<string>()),
                            speeding = Convert.ToInt32(response["speeding"].Value<string>()),
                            hardbraking = Convert.ToInt32(response["hardBraking"].Value<string>()),
                            sharpacceleration = Convert.ToInt32(response["sharpAcceleration"].Value<string>()),
                            sharpturn = Convert.ToInt32(response["sharpTurn"].Value<string>()),
                            latitude = response["latitude"].Value<string>(),
                            longitude = response["longitude"].Value<string>(),
                            score = score,
                            conductores = conductores.Count() > 0 ? conductores.Sum(c => c.Conductores) : 0,
                            talleres = talleres.Count() > 0 ? talleres.Sum(t => t.Ingresos) : 0,
                            siniestros = siniestros.Count() > 0 ? siniestros.Sum(s => s.Total_Siniestros) : 0,
                            ahorro = sustitutos.Count() > 0 ? diassustitutos : 0,
                            usuario = conductor.ToUpper(),
                        };
                        await _vehiculoGpsRepository.CreateAsync(vehiculo);
                    }
                    else
                    {
                        datosgps.kilometerstraveled = Convert.ToDecimal(response["odometer"].Value<string>().Replace(".", ","));
                        datosgps.trips = Convert.ToInt32(response["trips"].Value<string>());
                        datosgps.speeding = Convert.ToInt32(response["speeding"].Value<string>());
                        datosgps.hardbraking = Convert.ToInt32(response["hardBraking"].Value<string>());
                        datosgps.sharpacceleration = Convert.ToInt32(response["sharpAcceleration"].Value<string>());
                        datosgps.sharpturn = Convert.ToInt32(response["sharpTurn"].Value<string>());
                        datosgps.latitude = response["latitude"].Value<string>();
                        datosgps.longitude = response["longitude"].Value<string>();
                        datosgps.score = score;
                        datosgps.conductores = conductores.Count() > 0 ? conductores.Sum(c => c.Conductores) : 0;
                        datosgps.talleres = talleres.Count() > 0 ? talleres.Sum(t => t.Ingresos) : 0;
                        datosgps.siniestros = siniestros.Count() > 0 ? siniestros.Sum(s => s.Total_Siniestros) : 0;
                        datosgps.ahorro = sustitutos.Count() > 0 ? diassustitutos : 0;
                        datosgps.usuario = conductor.ToUpper();
                        await _vehiculoGpsRepository.UpdateAsync(datosgps);
                    }
                }
                catch (Exception ex)
                {
                    await _logRepository.SaveLogsGPS("Error", ex.Message, "CGB", "");
                }
            }
            return "Ok";
        }

        async static Task<string> getAuth(string url)
        {

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
        async static Task<string> getDataPlate(string auth, string url, Dictionary<string, string> placa)
        {

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

        public async Task<IEnumerable<ConductoresViewModel>> GetConductoresAsync(string Nit, string Placa)
        {
            var key = _configuration["KeyWs"];
            List<ConductoresViewModel> Conductores = new List<ConductoresViewModel>();

            var dataxml = await _service1Soap.RENTING_CLIENTES_RENTING_CONDUCTORESAsync(key,Placa,Nit);
            
            if (dataxml != null)
            {
                XmlDocument document = new XmlDocument();

                document.LoadXml(dataxml.Nodes[1].ToString());
                XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

                if (Datos.Count > 0)
                {
                    XmlNodeList lista1 =
                        ((XmlElement)Datos[0]).GetElementsByTagName("data");

                    foreach (XmlElement nodo in lista1)
                    {
                        //var dat= nodo[0].InnerText
                        XmlNodeList valor =
                            nodo.GetElementsByTagName("conductores");

                        XmlNodeList NitCliente =
                            nodo.GetElementsByTagName("Nit_cliente");

                        XmlNodeList Cliente =
                            nodo.GetElementsByTagName("Cliente");

                        ConductoresViewModel estado = new ConductoresViewModel();

                        estado.Conductores = int.Parse(valor[0].InnerText);
                        estado.Nit = NitCliente[0].InnerText;
                        estado.Cliente = Cliente[0].InnerText;


                        Conductores.Add(estado);
                    }

                }
            }

            return Conductores
                //.Where(t => t.Tipo == tipo)
                .ToList();

        }

        public async Task<IEnumerable<IngresosTallerViewModel>> GetIngresosTallerAsync(string Nit, string Placa)
        {
            var key = _configuration["KeyWs"];
            List<IngresosTallerViewModel> Ingresos = new List<IngresosTallerViewModel>();

            var dataxml = await _service1Soap.RENTING_CLIENTES_INGRESO_TALLERAsync(key, Placa, Nit);

            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {
                    //var dat= nodo[0].InnerText
                    XmlNodeList ingresos =
                        nodo.GetElementsByTagName("ingresos");
                    XmlNodeList placa =
                        nodo.GetElementsByTagName("placa");

                    XmlNodeList NitCliente =
                        nodo.GetElementsByTagName("Nit_cliente");

                    XmlNodeList Cliente =
                        nodo.GetElementsByTagName("Cliente");
                    XmlNodeList id_estado =
                        nodo.GetElementsByTagName("id_estado");
                    IngresosTallerViewModel estado = new IngresosTallerViewModel();

                    estado.Ingresos = int.Parse(ingresos[0].InnerText);
                    estado.Placa = placa[0].InnerText;
                    estado.Nit = NitCliente[0].InnerText;
                    estado.Cliente = Cliente[0].InnerText;
                    estado.Id_Estado = id_estado[0].InnerText;

                    Ingresos.Add(estado);
                }

            }

            return Ingresos
                //.Where(t => t.Tipo == tipo)
                .ToList();

        }

        public async Task<IEnumerable<SiniestrosViewModel>> GetSiniestrosAsync(string Nit, string Placa)
        {
            var key = _configuration["KeyWs"];
            List<SiniestrosViewModel> Siniestros = new List<SiniestrosViewModel>();

            var dataxml = await _service1Soap.RENTING_CLIENTES_RENTING_SINIESTROSAsync(key, Placa, Nit);

            if (dataxml != null)
            {
                XmlDocument document = new XmlDocument();

                document.LoadXml(dataxml.Nodes[1].ToString());
                XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

                if (Datos.Count > 0)
                {
                    XmlNodeList lista1 =
                        ((XmlElement)Datos[0]).GetElementsByTagName("data");

                    foreach (XmlElement nodo in lista1)
                    {
                        //var dat= nodo[0].InnerText
                        XmlNodeList siniestros =
                            nodo.GetElementsByTagName("total_siniestros");
                        XmlNodeList siniestros_finalizados =
                            nodo.GetElementsByTagName("eventos_siniestro_finalizado");
                        XmlNodeList eventos_siniestro_no_finalizado =
                            nodo.GetElementsByTagName("eventos_siniestro_no_finalizado");

                        XmlNodeList placa =
                            nodo.GetElementsByTagName("placa");

                        XmlNodeList NitCliente =
                            nodo.GetElementsByTagName("cliente");

                        XmlNodeList Cliente =
                            nodo.GetElementsByTagName("nom_cliente");

                        SiniestrosViewModel estado = new SiniestrosViewModel();

                        estado.Total_Siniestros = int.Parse(siniestros[0].InnerText);
                        estado.Eventos_Siniestros = int.Parse(siniestros_finalizados[0].InnerText);
                        estado.Eventos_Siniestros1 = int.Parse(eventos_siniestro_no_finalizado[0].InnerText);
                        estado.Placa = placa[0].InnerText;
                        estado.Nit = NitCliente[0].InnerText;
                        estado.Cliente = Cliente[0].InnerText;


                        Siniestros.Add(estado);
                    }

                }
            }

            return Siniestros
                //.Where(t => t.Tipo == tipo)
                .ToList();

        }

        public async Task<IEnumerable<SiniestrosDetalleViewModel>> GetSiniestrosDetalleAsync(string Nit, string Placa)
        {
            var key = _configuration["KeyWs"];
            List<SiniestrosDetalleViewModel> Siniestros = new List<SiniestrosDetalleViewModel>();

            var dataxml = await _service1Soap.RENTING_CLIENTES_RENTING_SINIESTROS_DETALLEAsync(key, Placa, Nit);

            if (dataxml != null)
            {
                XmlDocument document = new XmlDocument();

                document.LoadXml(dataxml.Nodes[1].ToString());
                XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");
                if (Datos.Count > 0)
                {
                    XmlNodeList lista1 =
                        ((XmlElement)Datos[0]).GetElementsByTagName("data");
                    foreach (XmlElement nodo in lista1)
                    {
                        //var dat= nodo[0].InnerText
                        XmlNodeList placa =
                            nodo.GetElementsByTagName("placa");
                        XmlNodeList usuario =
                            nodo.GetElementsByTagName("usuario");
                        XmlNodeList evento =
                            nodo.GetElementsByTagName("evento");
                        XmlNodeList estado =
                            nodo.GetElementsByTagName("estado");
                        XmlNodeList tiempo_siniestro =
                            nodo.GetElementsByTagName("tiempo_siniestro");
                        XmlNodeList cliente =
                            nodo.GetElementsByTagName("cliente");
                        XmlNodeList nombre_cliente =
                            nodo.GetElementsByTagName("nom_cliente");
                        XmlNodeList tipo_siniestro =
                            nodo.GetElementsByTagName("tipo_siniestro");
                        XmlNodeList anotaciones =
                            nodo.GetElementsByTagName("anotaciones");
                        XmlNodeList fecha_siniestro =
                            nodo.GetElementsByTagName("fecha_siniestro");
                        XmlNodeList ano =
                            nodo.GetElementsByTagName("ano");
                        XmlNodeList mes =
                            nodo.GetElementsByTagName("mes");

                        SiniestrosDetalleViewModel siniestroData = new SiniestrosDetalleViewModel();

                        siniestroData.placa = placa[0].InnerText;
                        siniestroData.usuario = usuario[0].InnerText;
                        siniestroData.evento = int.Parse(evento[0].InnerText);
                        siniestroData.estado = estado[0].InnerText;
                        siniestroData.tiempo_siniestro = tiempo_siniestro[0].InnerText;
                        siniestroData.cliente = cliente[0].InnerText;
                        siniestroData.nombre_cliente = nombre_cliente[0].InnerText;
                        siniestroData.tipo_siniestro = tipo_siniestro[0].InnerText;
                        if (anotaciones.Count > 0)
                        {
                            siniestroData.anotaciones = anotaciones[0].InnerText;
                        }
                        if (fecha_siniestro.Count > 0)
                        {
                            siniestroData.fecha_siniestro = fecha_siniestro[0].InnerText;
                        }
                        if (ano.Count > 0)
                        {
                            siniestroData.anio = int.Parse(ano[0].InnerText);
                        }
                        if (mes.Count > 0)
                        {
                            siniestroData.mes = int.Parse(mes[0].InnerText);
                        }

                        Siniestros.Add(siniestroData);
                    }

                }
            }
            return Siniestros
            //.Where(t => t.Tipo == tipo)
            .ToList();
        }

        public async Task<IEnumerable<SustitutosViewModel>> GetCuantosSustitutosAsync(string Nit, string Placa, string mes, string anio)
        {
            var key = _configuration["KeyWs"];
            List<SustitutosViewModel> Sustitutos = new List<SustitutosViewModel>();

            var dataxml = await _service1Soap.RENTING_CLIENTES_RENTING_CUANTOS_SUSTITUTOSAsync(key, Placa, Nit, mes, anio);

            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {
                    //var dat= nodo[0].InnerText
                    XmlNodeList sustitutos =
                        nodo.GetElementsByTagName("sustitutos");

                    XmlNodeList NitCliente =
                        nodo.GetElementsByTagName("Cliente");

                    XmlNodeList Cliente =
                        nodo.GetElementsByTagName("RUC");

                    SustitutosViewModel estado = new SustitutosViewModel();

                    estado.Sustitutos = int.Parse(sustitutos[0].InnerText);
                    estado.Ruc = NitCliente[0].InnerText;
                    estado.Cliente = Cliente[0].InnerText;


                    Sustitutos.Add(estado);
                }

            }

            return Sustitutos
                //.Where(t => t.Tipo == tipo)
                .ToList();

        }

        public async Task<IEnumerable<DiasSustitutosViewModel>> GetDiasSustitutosAsync(string Nit, string Placa)
        {
            var key = _configuration["KeyWs"];
            List<DiasSustitutosViewModel> Sustitutos = new List<DiasSustitutosViewModel>();

            var dataxml = await _service1Soap.RENTING_CLIENTES_RENTING_DIAS_SUSTITUTO_PLACAAsync(key, Placa, Nit);

            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {
                    //var dat= nodo[0].InnerText
                    XmlNodeList Dias =
                        nodo.GetElementsByTagName("Dias");

                    XmlNodeList Placa_sustituto =
                        nodo.GetElementsByTagName("Placa_sustituto");

                    XmlNodeList Ram_sustituto =
                        nodo.GetElementsByTagName("Ram_sustituto");

                    XmlNodeList Gama_sustituto =
                        nodo.GetElementsByTagName("Gama_sustituto");

                    XmlNodeList Estado =
                        nodo.GetElementsByTagName("Estado");

                    XmlNodeList Disponible =
                        nodo.GetElementsByTagName("Disponible");

                    XmlNodeList Regional =
                        nodo.GetElementsByTagName("Regional");

                    XmlNodeList Tipo_sustituto =
                        nodo.GetElementsByTagName("Tipo_sustituto");

                    XmlNodeList Modelo_sustituto =
                        nodo.GetElementsByTagName("Modelo_sustituto");

                    XmlNodeList Marca_sustituto =
                        nodo.GetElementsByTagName("Marca_sustituto");

                    XmlNodeList Año_sustituto =
                        nodo.GetElementsByTagName("Año_sustituto");

                    XmlNodeList Estatus =
                        nodo.GetElementsByTagName("Estatus");

                    DiasSustitutosViewModel estado = new DiasSustitutosViewModel();

                    estado.Dias = int.Parse(Dias[0].InnerText);
                    estado.Placa = Placa_sustituto[0].InnerText;
                    estado.Ram = Ram_sustituto[0].InnerText;
                    estado.Gama = Gama_sustituto[0].InnerText;
                    estado.Estado = Estado[0].InnerText;
                    estado.Disponible = Disponible[0].InnerText;
                    estado.Regional = Regional[0].InnerText;
                    estado.TipoSustituto = Tipo_sustituto[0].InnerText;
                    estado.ModeloSustituto = Modelo_sustituto[0].InnerText;
                    estado.MarcaSustituto = Marca_sustituto[0].InnerText;
                    estado.AnioSustituto = Año_sustituto[0].InnerText;
                    estado.Estatus = Estatus[0].InnerText;

                    Sustitutos.Add(estado);
                }

            }

            return Sustitutos
                //.Where(t => t.Tipo == tipo)
                .ToList();

        }

        public async Task<IEnumerable<ResumenPlacasViewModel>> GetResumePlacasAsync(string Nit, string Placa)
        {
            var key = _configuration["KeyWs"];
            List<ResumenPlacasViewModel> Resumen = new List<ResumenPlacasViewModel>();

            var dataxml = await _service1Soap.RENTING_CLIENTES_RENTING_RESUMEN_PLACASAsync(key, Placa, Nit);
            if (dataxml != null)
            {
                XmlDocument document = new XmlDocument();

                document.LoadXml(dataxml.Nodes[1].ToString());
                XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

                if (Datos.Count > 0)
                {
                    XmlNodeList lista1 =
                        ((XmlElement)Datos[0]).GetElementsByTagName("data");

                    foreach (XmlElement nodo in lista1)
                    {
                        var Nit_cliente = verificanodo(nodo, "Nit_cliente");
                        var Cliente = verificanodo(nodo, "Cliente");
                        var placa = verificanodo(nodo, "placa");
                        var usuario = verificanodo(nodo, "usuario");
                        var evento = verificanodo(nodo, "evento");
                        var tipo = verificanodo(nodo, "tipo");
                        var estado = verificanodo(nodo, "estado");
                        var detalle_cita = verificanodo(nodo, "detalle_cita");
                        var detalle_oc = verificanodo(nodo, "detalle_oc");
                        var usuario_asesor = verificanodo(nodo, "usuario_asesor");
                        var ciudad_ult_mmto = verificanodo(nodo, "ciudad_ult_mmto");
                        var ult_rutina = verificanodo(nodo, "ult_rutina");
                        var fecha_mmto = verificanodo(nodo, "fecha_mmto");

                        ResumenPlacasViewModel resumen = new ResumenPlacasViewModel();

                        resumen.Nit_cliente = Nit_cliente;
                        resumen.Cliente = Cliente;
                        resumen.placa = placa;
                        resumen.usuario = usuario;
                        resumen.evento = evento;
                        resumen.tipo = tipo;
                        resumen.estado = estado;
                        resumen.detalle_cita = detalle_cita;
                        resumen.detalle_oc = detalle_oc;
                        resumen.usuario_asesor = usuario_asesor;
                        resumen.ciudad_ult_mmto = ciudad_ult_mmto;
                        resumen.ult_rutina = ult_rutina;
                        resumen.fecha_mmto = fecha_mmto;

                        Resumen.Add(resumen);
                    }

                }
            }
            return Resumen
                //.Where(t => t.Tipo == tipo)
                .ToList();

        }

        public async Task<string> GetConductorPlacasAsync(string Placa)
        {
            string nombre="";
            var key = _configuration["KeyWs"];

            var dataxml = await _service1Soap.RENTING_CLIENTES_RENTING_NOMBRE_CONDUCTOR_PLACAAsync(key, Placa);
            
            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {
                    nombre = verificanodo(nodo, "conductor");
                }

            }

            return nombre;

        }
        public async Task<IEnumerable<ParticularidadViewModel>> GetParticularidadesAsync(string Placa)
        {

            var key = _configuration["KeyWs"];
            List<ParticularidadViewModel> Resumen = new List<ParticularidadViewModel>();

            var dataxml = await _service1Soap.WS_consulta_total_renting_vh_particularidadesAsync(key, Placa);

            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {

                    var Placastr = verificanodo(nodo, "PLACA");
                    var Particularidad = verificanodo(nodo, "PARTICULARIDAD");
                    var Fecha = verificanodo(nodo, "FECHA_CREACION");
                    var Asesor = verificanodo(nodo, "ASESOR");
                    var Tipo = verificanodo(nodo, "TIPO");


                    ParticularidadViewModel resumen = new ParticularidadViewModel();

                    resumen.Placa = Placastr;
                    resumen.Particularidad = Particularidad;
                    resumen.Fecha = Convert.ToDateTime(Fecha);
                    resumen.Asesor = Asesor;
                    resumen.Tipo = Tipo;


                    Resumen.Add(resumen);
                }

            }

            return Resumen;

        }

        public async Task<string> GetEmailConductorAsync(string Placa)
        {
            string nombre = "";
            var key = _configuration["KeyWs"];

            var dataxml = await _service1Soap.WS_clientes_renting_nombre_conductorAsync(key, Placa);

            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                XmlNodeList lista1 =
                    ((XmlElement)Datos[0]).GetElementsByTagName("data");

                foreach (XmlElement nodo in lista1)
                {
                    nombre = verificanodo(nodo, "correo");
                }

            }

            return nombre;

        }
        public async Task<IEnumerable<MantEstadosCuantosViewModel>> GetMantenimientoEstadoCuantos(string Nit, string mes, string anio)
        {
            var key = _configuration["KeyWs"];
            List<MantEstadosCuantosViewModel> Cuantos = new List<MantEstadosCuantosViewModel>();

            var dataxml = await _service1Soap.RENTING_clientes_renting_mantenimientos_estados_cuantosAsync(key, Nit, mes, anio);
            if (dataxml != null)
            {
                XmlDocument document = new XmlDocument();

                document.LoadXml(dataxml.Nodes[1].ToString());
                XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

                if (Datos.Count > 0)
                {
                    XmlNodeList lista1 =
                        ((XmlElement)Datos[0]).GetElementsByTagName("data");

                    foreach (XmlElement nodo in lista1)
                    {
                        var Nit_cliente = verificanodo(nodo, "Nit_cliente");
                        var Cliente = verificanodo(nodo, "Cliente");
                        var id_estado = verificanodo(nodo, "id_estado");
                        var estado = verificanodo(nodo, "estado");
                        var cuantos = verificanodo(nodo, "cuantos");

                        MantEstadosCuantosViewModel cuantosVM = new MantEstadosCuantosViewModel();

                        cuantosVM.Nit_cliente = Nit_cliente;
                        cuantosVM.Cliente = Cliente;
                        cuantosVM.id_estado = id_estado;
                        cuantosVM.estado = estado;
                        cuantosVM.cuantos = cuantos;

                        Cuantos.Add(cuantosVM);
                    }

                }
            }
            return Cuantos
                //.Where(t => t.Tipo == tipo)
                .ToList();

        }
    }
}
