using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class TripsIturanViewModel
    {
        public int TID { get; set; }  //id unico de viaje
        public string VID { get; set; } //id del sipositivo
        public string NN { get; set; } //nombre del vehiculo
        public string PN { get; set; } //placa
        public DateTime T1 { get; set; } //tiempo de inicio de viaje
        public DateTime T2 { get; set; } //tiempo de fin de viaje
        public string DID { get; set; } //id del conductor
        public string DN { get; set; } //nombre conductor
        public string O1 { get; set; } //odomentro inicial del viaje km
        public string O2 { get; set; } //odometro final del viaje km
        public string TT { get; set; } //tipo de viaje
        public string OST { get; set; } //umbral de exceso de velicidad
        public string OSD { get; set; } //duracion del exceso de velocidad
        public string OSO { get; set; } //ocurrecia de exceso de velocidad
        public string EH1 { get; set; } //horas de motor inicio de viaje seg
        public string EH2 { get; set; } //horas de motor fin de viaje seg
        public string IDD { get; set; } //tiempo de inactividad seg
        public string IDO { get; set; } //ocurrecia de inactividad
        public string TPS { get; set; } //velocidad superior
        public string TPR { get; set; } //RPM superior
        public string ORD { get; set; } //duracion exceos RPM seg
        public string FUS { get; set; } //uso de combustible en el viaje litros
        public string ACO { get; set; } //ocurrecias de aceleracion
        public string DCO { get; set; } //ocurrencias de desaceleracion
        public string CAO { get; set; } //ocurrencia de aceleracion en curva
        public string X1 { get; set; } //longitud de inicio de viaje 
        public string Y1 { get; set; } //latitud de inicio de viaje
        public string AD1 { get; set; } //ubicacion inicio de viaje
        public string X2 { get; set; } //longitud de fin de viaje
        public string Y2 { get; set; } //latitud de fin de viaje
        public string AD2 { get; set; } //ubicacion fin de viaje
    }
}
