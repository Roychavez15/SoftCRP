using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Adendum")]
        public string Adendum { get; set; }

        [Display(Name = "Año")]
        public string Año { get; set; }

        [Display(Name = "Canon")]        
        public string Canon { get; set; }

        [Display(Name = "Chasis")]
        public string Chasis { get; set; }

        [Display(Name = "Ciudad de Operación")]
        public string Ciudad_operacion { get; set; }

        [Display(Name = "Clase")]
        public string Clase { get; set; }

        [Display(Name = "Cliente")]
        public string Cliente { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Contrato")]
        public string Contrato { get; set; }

        [Display(Name = "Cotización")]
        public string Cotizacion { get; set; }

        [Display(Name = "Descripción Modelo")]
        public string Des_modelo { get; set; }

        [Display(Name = "Dispositivo")]
        public string Dispositivo { get; set; }

        [Display(Name = "Ejecutivo")]
        public string Ejecutivo { get; set; }

        [Display(Name = "Estado")]
        public string Estatus { get; set; }

        [Display(Name = "Fecha Entrega")]
        public string Fecha_entrega { get; set; }

        [Display(Name = "Fecha Km")]
        public string Fecha_km { get; set; }

        [Display(Name = "Fecha última rutina")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        public string Fecha_ultima_rutina { get; set; }

        [Display(Name = "Fecha de Contrato")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        public string Fechacontrato { get; set; }

        [Display(Name = "Fecha Fin de Contrato")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        public string FechafinContrato { get; set; }

        [Display(Name = "Forma de Facturación")]
        public string FormaFacturacion { get; set; }

        [Display(Name = "Id última rutina")]
        public string Id_ultima_rutina { get; set; }

        [Display(Name = "Km")]
        public string Km { get; set; }

        [Display(Name = "Km Anual")]
        public string KmAnual { get; set; }

        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [Display(Name = "Motor")]
        public string Motor { get; set; }

        [Display(Name = "Mantenimiento Correctivo")]
        public string Mto_correctivo { get; set; }

        [Display(Name = "Mantenimiento Llantas")]
        public string Mto_llantas { get; set; }

        [Display(Name = "Mantenimiento Preventivo")]
        public string Mto_preventivo { get; set; }

        [Display(Name = "Mantenimiento Sustituto")]
        public string Mto_sustituto { get; set; }

        [Display(Name = "Nombre Cliente")]
        public string Nom_cliente { get; set; }

        [Display(Name = "Nombre Ejecutivo")]
        public string Nom_ejecutivo { get; set; }

        [Display(Name = "Nombre Aseguradora")]
        public string NombreAseguradora { get; set; }

        [Display(Name = "Placa")]
        public string Placa { get; set; }

        [Display(Name = "Plan Seguro")]
        public string Plan_Seguro { get; set; }

        [Display(Name = "Plazo")]
        public string Plazo { get; set; }

        [Display(Name = "Plazo Pago")]
        public string Plazo_pago { get; set; }

        [Display(Name = "Ramv")]
        public string Ramv { get; set; }

        [Display(Name = "Siniestros")]
        public string Siniestros { get; set; }

        [Display(Name = "Ultima rutina")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        public string Ultima_rutina { get; set; }
    }

}
