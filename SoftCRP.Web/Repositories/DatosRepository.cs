using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Configuration;
using SoftCRP.Web.Data;
using SoftCRP.Web.Models;

namespace SoftCRP.Web.Repositories
{
    public class DatosRepository : IDatosRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly WSDLCondelpiData.Service1Soap _service1Soap;

        public DatosRepository(
            DataContext context,
            IConfiguration configuration,
            WSDLCondelpiData.Service1Soap service1Soap)
        {
            _context = context;
            _configuration = configuration;
            _service1Soap = service1Soap;
        }
        public async Task<DatosAuto> GetDatosAutoAsync(string placa)
        {
            //throw new NotImplementedException();
            DatosAuto datos = new DatosAuto();

            var key = _configuration["KeyWs"];

            var dataxml = await _service1Soap.Consulta_Data_autoAsync(key, placa);

            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");


            XmlNodeList lista1 =
                ((XmlElement)Datos[0]).GetElementsByTagName("data");

            foreach (XmlElement nodo in lista1)
            {
                datos= new DatosAuto
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
            return datos;
        }
        private string verificanodo(XmlElement lista1, string nodo)
        {
            var aux = "";
            try
            {
                aux = lista1.GetElementsByTagName(nodo)[0].InnerText;
            }
            catch { aux = ""; }
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
    }
}
