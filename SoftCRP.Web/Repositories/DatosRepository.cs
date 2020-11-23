using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Configuration;
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

        public DatosRepository(
            DataContext context,
            IConfiguration configuration,
            WSDLCondelpiData.Service1Soap service1Soap,
            IUserHelper userHelper,
            IVehiculoProvGpsRepository vehiculoProvGpsRepository)
        {
            _context = context;
            _configuration = configuration;
            _service1Soap = service1Soap;
            _userHelper = userHelper;
            _vehiculoProvGpsRepository = vehiculoProvGpsRepository;
        }
        public async Task<DatosAuto> GetDatosAutoAsync(string placa)
        {
            //throw new NotImplementedException();
            DatosAuto datos = new DatosAuto();

            var key = _configuration["KeyWs"];

            var dataxml = await _service1Soap.Consulta_Data_autoAsync(key, placa);
            //var dataxml = await _service1Soap.WS_GPS_PLACAAsync(key, placa);
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

            return datos;
        }

        public async Task<IEnumerable<DatosAuto>> GetDatosAutoAllAsync()
        {
            //throw new NotImplementedException();
            List<DatosAuto> datos = new List<DatosAuto>();

            var key = _configuration["KeyWs"];

            var dataxml = await _service1Soap.Get_consulta_totalAsync(key);

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

                    if(vehiculoProvGps!=null)
                    {
                        if(vehiculoProvGps.Gps.Trim() != "No tiene dispositivo satelital")
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
                                    vehiculo.gps_id = vehiculoProvGps.Gps.Trim();

                                    try
                                    {
                                        //_context.vehiculos.Add(vehiculo);
                                        await _vehiculoProvGpsRepository.CreateAsync(vehiculo);
                                    }
                                    catch (Exception ex)
                                    {

                                    }

                                }
                            }
                        }
                    }



                    datos.Add(auto);
                }
            }
            return datos.Where(e => e.Estatus == "VIGENTE CON CONTRATO");
        }


        public async Task<VehiculoProvGpsViewModel> GetDatosAutoProvGpsAsync(string nit, string placa)
        {
            var key = _configuration["KeyWs"];

            var dataxml = await _service1Soap.WS_GPS_PLACAAsync(key, placa);

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

            return Vehiculos
                .Where(s => s.historial_vh == "VIGENTE CON CONTRATO")
                .OrderBy(o=> o.codigo_activo)
                .ToList();            
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
                .Where(s => s.historial_vh == "VIGENTE CON CONTRATO")
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


    }
}
