using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SoftCRP.Web.Models
{

    public class DatosAuto
    {
        [XmlElement(ElementName = "adendum")]
        public string Adendum { get; set; }
        [XmlElement(ElementName = "año")]
        public string Año { get; set; }
        [XmlElement(ElementName = "canon")]
        public string Canon { get; set; }
        [XmlElement(ElementName = "chasis")]
        public string Chasis { get; set; }
        [XmlElement(ElementName = "ciudad_operacion")]
        public string Ciudad_operacion { get; set; }
        [XmlElement(ElementName = "clase")]
        public string Clase { get; set; }
        [XmlElement(ElementName = "cliente")]
        public string Cliente { get; set; }
        [XmlElement(ElementName = "color")]
        public string Color { get; set; }
        [XmlElement(ElementName = "contrato")]
        public string Contrato { get; set; }
        [XmlElement(ElementName = "cotizacion")]
        public string Cotizacion { get; set; }
        [XmlElement(ElementName = "des_modelo")]
        public string Des_modelo { get; set; }
        [XmlElement(ElementName = "dispositivo")]
        public string Dispositivo { get; set; }
        [XmlElement(ElementName = "ejecutivo")]
        public string Ejecutivo { get; set; }
        [XmlElement(ElementName = "estatus")]
        public string Estatus { get; set; }
        [XmlElement(ElementName = "fecha_entrega")]
        public string Fecha_entrega { get; set; }
        [XmlElement(ElementName = "fecha_km")]
        public string Fecha_km { get; set; }
        [XmlElement(ElementName = "fecha_ultima_rutina")]
        public string Fecha_ultima_rutina { get; set; }
        [XmlElement(ElementName = "fechacontrato")]
        public string Fechacontrato { get; set; }
        [XmlElement(ElementName = "fechafinContrato")]
        public string FechafinContrato { get; set; }
        [XmlElement(ElementName = "FormaFacturacion")]
        public string FormaFacturacion { get; set; }
        
        [XmlElement(ElementName = "id_ultima_rutina")]
        public string Id_ultima_rutina { get; set; }
        [XmlElement(ElementName = "km")]
        public string Km { get; set; }
        [XmlElement(ElementName = "KmAnual")]
        public string KmAnual { get; set; }
        [XmlElement(ElementName = "marca")]
        public string Marca { get; set; }
        [XmlElement(ElementName = "modelo")]
        public string Modelo { get; set; }
        [XmlElement(ElementName = "motor")]
        public string Motor { get; set; }
        [XmlElement(ElementName = "mto_correctivo")]
        public string Mto_correctivo { get; set; }
        [XmlElement(ElementName = "mto_llantas")]
        public string Mto_llantas { get; set; }
        [XmlElement(ElementName = "mto_preventivo")]
        public string Mto_preventivo { get; set; }
        [XmlElement(ElementName = "mto_sustituto")]
        public string Mto_sustituto { get; set; }
        [XmlElement(ElementName = "nom_cliente")]
        public string Nom_cliente { get; set; }
        [XmlElement(ElementName = "nom_ejecutivo")]
        public string Nom_ejecutivo { get; set; }
        [XmlElement(ElementName = "NombreAseguradora")]
        public string NombreAseguradora { get; set; }
        [XmlElement(ElementName = "placa")]
        public string Placa { get; set; }
        [XmlElement(ElementName = "Plan_Seguro")]
        public string Plan_Seguro { get; set; }
        [XmlElement(ElementName = "Plazo")]
        public string Plazo { get; set; }
        [XmlElement(ElementName = "Plazo_pago")]
        public string Plazo_pago { get; set; }
        [XmlElement(ElementName = "ramv")]
        public string Ramv { get; set; }
        [XmlElement(ElementName = "siniestros")]
        public string Siniestros { get; set; }
        [XmlElement(ElementName = "ultima_rutina")]
        public string Ultima_rutina { get; set; }
    }

}
