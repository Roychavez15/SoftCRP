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
                    Adendum = nodo.GetElementsByTagName("adendum")[0].InnerText,
                    Año = nodo.GetElementsByTagName("año")[0].InnerText,
                    Canon = nodo.GetElementsByTagName("canon")[0].InnerText,
                    Chasis = nodo.GetElementsByTagName("chasis")[0].InnerText,
                    Ciudad_operacion = nodo.GetElementsByTagName("ciudad_operacion")[0].InnerText,
                    Clase = nodo.GetElementsByTagName("clase")[0].InnerText,
                    Cliente = nodo.GetElementsByTagName("cliente")[0].InnerText,
                    Color = nodo.GetElementsByTagName("color")[0].InnerText,
                    Contrato = nodo.GetElementsByTagName("contrato")[0].InnerText,
                    Cotizacion = nodo.GetElementsByTagName("cotizacion")[0].InnerText,
                    Des_modelo = nodo.GetElementsByTagName("des_modelo")[0].InnerText,
                    Dispositivo = nodo.GetElementsByTagName("dispositivo")[0].InnerText,
                    Ejecutivo = nodo.GetElementsByTagName("ejecutivo")[0].InnerText,
                    Estatus = nodo.GetElementsByTagName("estatus")[0].InnerText,
                    Fechacontrato = nodo.GetElementsByTagName("fechacontrato")[0].InnerText.Substring(0,10),
                    FechafinContrato = nodo.GetElementsByTagName("fechafinContrato")[0].InnerText.Substring(0, 10),
                    Fecha_entrega = nodo.GetElementsByTagName("fecha_entrega")[0].InnerText.Substring(0, 10),
                    Fecha_km = nodo.GetElementsByTagName("fecha_km")[0].InnerText.Substring(0, 10),
                    Fecha_ultima_rutina = nodo.GetElementsByTagName("fecha_ultima_rutina")[0].InnerText.Substring(0, 10),
                    FormaFacturacion = nodo.GetElementsByTagName("FormaFacturacion")[0].InnerText,

                    Id_ultima_rutina = nodo.GetElementsByTagName("id_ultima_rutina")[0].InnerText,
                    Km = nodo.GetElementsByTagName("km")[0].InnerText,
                    KmAnual = nodo.GetElementsByTagName("KmAnual")[0].InnerText,
                    Marca = nodo.GetElementsByTagName("marca")[0].InnerText,
                    Modelo = nodo.GetElementsByTagName("modelo")[0].InnerText,
                    Motor = nodo.GetElementsByTagName("motor")[0].InnerText,
                    Mto_correctivo = nodo.GetElementsByTagName("mto_correctivo")[0].InnerText,
                    Mto_llantas = nodo.GetElementsByTagName("mto_llantas")[0].InnerText,
                    Mto_preventivo = nodo.GetElementsByTagName("mto_preventivo")[0].InnerText,
                    Mto_sustituto = nodo.GetElementsByTagName("mto_sustituto")[0].InnerText,
                    NombreAseguradora = nodo.GetElementsByTagName("NombreAseguradora")[0].InnerText,
                    Nom_cliente = nodo.GetElementsByTagName("nom_cliente")[0].InnerText,
                    Nom_ejecutivo = nodo.GetElementsByTagName("nom_ejecutivo")[0].InnerText,
                    Placa = nodo.GetElementsByTagName("placa")[0].InnerText,
                    Plan_Seguro = nodo.GetElementsByTagName("Plan_Seguro")[0].InnerText,
                    Plazo = nodo.GetElementsByTagName("Plazo")[0].InnerText,
                    Plazo_pago = nodo.GetElementsByTagName("Plazo_pago")[0].InnerText,
                    Ramv = nodo.GetElementsByTagName("ramv")[0].InnerText,
                    Siniestros = nodo.GetElementsByTagName("siniestros")[0].InnerText,
                    Ultima_rutina = nodo.GetElementsByTagName("ultima_rutina")[0].InnerText,
                };
                
            }
            return datos;
        }

        public async Task<List<VehiculosClientesViewModel>> GetVehiculosClienteAsync(string nit)
        {
            var key = _configuration["KeyWs"];
            List<VehiculosClientesViewModel> Vehiculos = new List<VehiculosClientesViewModel>();

            var dataxml = await _service1Soap.Consulta_Data_nit_autoAsync(key, nit);

            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");


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

            return Vehiculos
                .Where(s => s.historial_vh == "VIGENTE CON CONTRATO")
                .OrderBy(o=> o.codigo_activo)
                .ToList();            
        }

    }
}
