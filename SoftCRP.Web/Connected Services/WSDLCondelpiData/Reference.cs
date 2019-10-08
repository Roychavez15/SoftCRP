﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WSDLCondelpiData
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1-preview-30422-0661")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WSDLCondelpiData.Service1Soap")]
    public interface Service1Soap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DENCP", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> DENCPAsync(string key, string campo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ENCP", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> ENCPAsync(string key, string campo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetDataNw", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> GetDataNwAsync(string key, string identidad);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetDataNwLogin", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> GetDataNwLoginAsync(string key, string identidad, string grupo, int orden);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ACTULIZARINFONW", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<bool> ACTULIZARINFONWAsync(string key, string GRUPO, int ORDEN, string CEDULA, string NOMBRE, string TELEFONO, string TELEFONO1, string CELULAR, string CALLE_PRIN, string NUMERO, string CALLE_SEC, string CIUDAD, string EMAIL, string IP_CLIENTE, System.DateTime FECHA_ACT);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CotizadorApc", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<bool> CotizadorApcAsync(
                    string key, 
                    string NOMBRE1, 
                    string NOMBRE2, 
                    string APELLIDO1, 
                    string APELLIDO2, 
                    string CEDULA, 
                    string TELEFONO, 
                    string MAIL, 
                    System.DateTime FECHA_REGISTRO, 
                    int TIPCLI_CODIGO, 
                    int ID_PLAN, 
                    int Monto, 
                    int REFERIDO, 
                    string RELACION_REF, 
                    int COD_REGIONAL, 
                    int CODE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CotizadorApc_LAND", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<bool> CotizadorApc_LANDAsync(
                    string key, 
                    string NOMBRE1, 
                    string NOMBRE2, 
                    string APELLIDO1, 
                    string APELLIDO2, 
                    string CEDULA, 
                    string TELEFONO, 
                    string MAIL, 
                    System.DateTime FECHA_REGISTRO, 
                    int TIPCLI_CODIGO, 
                    int ID_PLAN, 
                    int Monto, 
                    int REFERIDO, 
                    string RELACION_REF, 
                    int COD_REGIONAL, 
                    int CODE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LOG_LOGIN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<bool> LOG_LOGINAsync(string key, string Aplicacion, string Usuario, string Equipo, string Tipo, string Descripcion, int error);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/registroAPP_clientes", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> registroAPP_clientesAsync(string key, string DOCUMENTO, string CODIGO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/registroAPP_client_max", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> registroAPP_client_maxAsync(string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Boton_Pagos", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Boton_PagosAsync(string key, string CEDULA, string CODIGO_PAGO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/registroAPP_consulta", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> registroAPP_consultaAsync(string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/registroAPP_user", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> registroAPP_userAsync(string key, string DOCUMENTO, string TIPO, string NOMBRE, string APELLIDO, string CIUDAD, string EMAIL, string TELEFONO, string CODIGO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Comision_user", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Comision_userAsync(string key, string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Validacion_user_apc", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Validacion_user_apcAsync(string key, string cedula);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Inserta_Prospectos", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Inserta_ProspectosAsync(string key, float CODIGO, string NOMBRE1, string APELLIDO1, string CEDULA, string TIPODOCU, string MAIL, string TELEFONO, string BUSSINES_ADVISOR, string ESTABLIS_TYPE_ID, float MONTO, string FUENTE, string OS, string ESTABLIS_NAME, int USER_CODE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Estado_Prospecto", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Estado_ProspectoAsync(string key, string id, string CODIGOCONS);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Cantidad_Facturada", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Cantidad_FacturadaAsync(string key, int ID, string TIPO, string DATE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Establecimiento_Punto_VENTA", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Establecimiento_Punto_VENTAAsync(string key, int CODIGO, string NOMBRE, string TIPO, string DIRECCION, string TELEFONO, string REPRESENTANTE_LEGAL, string CONTACTO, string CODE, int TIPO_ESTABL, string RUC_ESTABL);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Consulta_Data_auto", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Consulta_Data_autoAsync(string key, string Placa);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Consulta_Data_nit_auto", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Consulta_Data_nit_autoAsync(string key, string NIT);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Consulta_clientes", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Consulta_clientesAsync(string key, string ruc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Ingreso_Incidencias", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<bool> Ingreso_IncidenciasAsync(string key, string Placa, int submotivo, string Observacion, string usuario, string usuario_solucion, string motivo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Responsable_sla", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Responsable_slaAsync(string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Tipo_incidencia", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Tipo_incidenciaAsync(string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Via_Ingreso", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Via_IngresoAsync(string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Estado_incidencia", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Estado_incidenciaAsync(string key);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1-preview-30422-0661")]
    public interface Service1SoapChannel : WSDLCondelpiData.Service1Soap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1-preview-30422-0661")]
    public partial class Service1SoapClient : System.ServiceModel.ClientBase<WSDLCondelpiData.Service1Soap>, WSDLCondelpiData.Service1Soap
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public Service1SoapClient(EndpointConfiguration endpointConfiguration) : 
                base(Service1SoapClient.GetBindingForEndpoint(endpointConfiguration), Service1SoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1SoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(Service1SoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1SoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(Service1SoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> DENCPAsync(string key, string campo)
        {
            return base.Channel.DENCPAsync(key, campo);
        }
        
        public System.Threading.Tasks.Task<string> ENCPAsync(string key, string campo)
        {
            return base.Channel.ENCPAsync(key, campo);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> GetDataNwAsync(string key, string identidad)
        {
            return base.Channel.GetDataNwAsync(key, identidad);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> GetDataNwLoginAsync(string key, string identidad, string grupo, int orden)
        {
            return base.Channel.GetDataNwLoginAsync(key, identidad, grupo, orden);
        }
        
        public System.Threading.Tasks.Task<bool> ACTULIZARINFONWAsync(string key, string GRUPO, int ORDEN, string CEDULA, string NOMBRE, string TELEFONO, string TELEFONO1, string CELULAR, string CALLE_PRIN, string NUMERO, string CALLE_SEC, string CIUDAD, string EMAIL, string IP_CLIENTE, System.DateTime FECHA_ACT)
        {
            return base.Channel.ACTULIZARINFONWAsync(key, GRUPO, ORDEN, CEDULA, NOMBRE, TELEFONO, TELEFONO1, CELULAR, CALLE_PRIN, NUMERO, CALLE_SEC, CIUDAD, EMAIL, IP_CLIENTE, FECHA_ACT);
        }
        
        public System.Threading.Tasks.Task<bool> CotizadorApcAsync(
                    string key, 
                    string NOMBRE1, 
                    string NOMBRE2, 
                    string APELLIDO1, 
                    string APELLIDO2, 
                    string CEDULA, 
                    string TELEFONO, 
                    string MAIL, 
                    System.DateTime FECHA_REGISTRO, 
                    int TIPCLI_CODIGO, 
                    int ID_PLAN, 
                    int Monto, 
                    int REFERIDO, 
                    string RELACION_REF, 
                    int COD_REGIONAL, 
                    int CODE)
        {
            return base.Channel.CotizadorApcAsync(key, NOMBRE1, NOMBRE2, APELLIDO1, APELLIDO2, CEDULA, TELEFONO, MAIL, FECHA_REGISTRO, TIPCLI_CODIGO, ID_PLAN, Monto, REFERIDO, RELACION_REF, COD_REGIONAL, CODE);
        }
        
        public System.Threading.Tasks.Task<bool> CotizadorApc_LANDAsync(
                    string key, 
                    string NOMBRE1, 
                    string NOMBRE2, 
                    string APELLIDO1, 
                    string APELLIDO2, 
                    string CEDULA, 
                    string TELEFONO, 
                    string MAIL, 
                    System.DateTime FECHA_REGISTRO, 
                    int TIPCLI_CODIGO, 
                    int ID_PLAN, 
                    int Monto, 
                    int REFERIDO, 
                    string RELACION_REF, 
                    int COD_REGIONAL, 
                    int CODE)
        {
            return base.Channel.CotizadorApc_LANDAsync(key, NOMBRE1, NOMBRE2, APELLIDO1, APELLIDO2, CEDULA, TELEFONO, MAIL, FECHA_REGISTRO, TIPCLI_CODIGO, ID_PLAN, Monto, REFERIDO, RELACION_REF, COD_REGIONAL, CODE);
        }
        
        public System.Threading.Tasks.Task<bool> LOG_LOGINAsync(string key, string Aplicacion, string Usuario, string Equipo, string Tipo, string Descripcion, int error)
        {
            return base.Channel.LOG_LOGINAsync(key, Aplicacion, Usuario, Equipo, Tipo, Descripcion, error);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> registroAPP_clientesAsync(string key, string DOCUMENTO, string CODIGO)
        {
            return base.Channel.registroAPP_clientesAsync(key, DOCUMENTO, CODIGO);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> registroAPP_client_maxAsync(string key)
        {
            return base.Channel.registroAPP_client_maxAsync(key);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Boton_PagosAsync(string key, string CEDULA, string CODIGO_PAGO)
        {
            return base.Channel.Boton_PagosAsync(key, CEDULA, CODIGO_PAGO);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> registroAPP_consultaAsync(string key)
        {
            return base.Channel.registroAPP_consultaAsync(key);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> registroAPP_userAsync(string key, string DOCUMENTO, string TIPO, string NOMBRE, string APELLIDO, string CIUDAD, string EMAIL, string TELEFONO, string CODIGO)
        {
            return base.Channel.registroAPP_userAsync(key, DOCUMENTO, TIPO, NOMBRE, APELLIDO, CIUDAD, EMAIL, TELEFONO, CODIGO);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Comision_userAsync(string key, string id)
        {
            return base.Channel.Comision_userAsync(key, id);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Validacion_user_apcAsync(string key, string cedula)
        {
            return base.Channel.Validacion_user_apcAsync(key, cedula);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Inserta_ProspectosAsync(string key, float CODIGO, string NOMBRE1, string APELLIDO1, string CEDULA, string TIPODOCU, string MAIL, string TELEFONO, string BUSSINES_ADVISOR, string ESTABLIS_TYPE_ID, float MONTO, string FUENTE, string OS, string ESTABLIS_NAME, int USER_CODE)
        {
            return base.Channel.Inserta_ProspectosAsync(key, CODIGO, NOMBRE1, APELLIDO1, CEDULA, TIPODOCU, MAIL, TELEFONO, BUSSINES_ADVISOR, ESTABLIS_TYPE_ID, MONTO, FUENTE, OS, ESTABLIS_NAME, USER_CODE);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Estado_ProspectoAsync(string key, string id, string CODIGOCONS)
        {
            return base.Channel.Estado_ProspectoAsync(key, id, CODIGOCONS);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Cantidad_FacturadaAsync(string key, int ID, string TIPO, string DATE)
        {
            return base.Channel.Cantidad_FacturadaAsync(key, ID, TIPO, DATE);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Establecimiento_Punto_VENTAAsync(string key, int CODIGO, string NOMBRE, string TIPO, string DIRECCION, string TELEFONO, string REPRESENTANTE_LEGAL, string CONTACTO, string CODE, int TIPO_ESTABL, string RUC_ESTABL)
        {
            return base.Channel.Establecimiento_Punto_VENTAAsync(key, CODIGO, NOMBRE, TIPO, DIRECCION, TELEFONO, REPRESENTANTE_LEGAL, CONTACTO, CODE, TIPO_ESTABL, RUC_ESTABL);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Consulta_Data_autoAsync(string key, string Placa)
        {
            return base.Channel.Consulta_Data_autoAsync(key, Placa);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Consulta_Data_nit_autoAsync(string key, string NIT)
        {
            return base.Channel.Consulta_Data_nit_autoAsync(key, NIT);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Consulta_clientesAsync(string key, string ruc)
        {
            return base.Channel.Consulta_clientesAsync(key, ruc);
        }
        
        public System.Threading.Tasks.Task<bool> Ingreso_IncidenciasAsync(string key, string Placa, int submotivo, string Observacion, string usuario, string usuario_solucion, string motivo)
        {
            return base.Channel.Ingreso_IncidenciasAsync(key, Placa, submotivo, Observacion, usuario, usuario_solucion, motivo);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Responsable_slaAsync(string key)
        {
            return base.Channel.Responsable_slaAsync(key);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Tipo_incidenciaAsync(string key)
        {
            return base.Channel.Tipo_incidenciaAsync(key);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Via_IngresoAsync(string key)
        {
            return base.Channel.Via_IngresoAsync(key);
        }
        
        public System.Threading.Tasks.Task<WSDLCondelpiData.ArrayOfXElement> Estado_incidenciaAsync(string key)
        {
            return base.Channel.Estado_incidenciaAsync(key);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.Service1Soap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.Service1Soap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.Service1Soap))
            {
                return new System.ServiceModel.EndpointAddress("http://ws.condelpi.com/APC_WEBSERVICE/service1.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.Service1Soap12))
            {
                return new System.ServiceModel.EndpointAddress("http://ws.condelpi.com/APC_WEBSERVICE/service1.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            Service1Soap,
            
            Service1Soap12,
        }
    }
    
    [System.Xml.Serialization.XmlSchemaProviderAttribute(null, IsAny=true)]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil-lib", "2.0.0.1")]
    public partial class ArrayOfXElement : object, System.Xml.Serialization.IXmlSerializable
    {
        
        private System.Collections.Generic.List<System.Xml.Linq.XElement> nodesList = new System.Collections.Generic.List<System.Xml.Linq.XElement>();
        
        public ArrayOfXElement()
        {
        }
        
        public virtual System.Collections.Generic.List<System.Xml.Linq.XElement> Nodes
        {
            get
            {
                return this.nodesList;
            }
        }
        
        public virtual System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new System.NotImplementedException();
        }
        
        public virtual void WriteXml(System.Xml.XmlWriter writer)
        {
            System.Collections.Generic.IEnumerator<System.Xml.Linq.XElement> e = nodesList.GetEnumerator();
            for (
            ; e.MoveNext(); 
            )
            {
                ((System.Xml.Serialization.IXmlSerializable)(e.Current)).WriteXml(writer);
            }
        }
        
        public virtual void ReadXml(System.Xml.XmlReader reader)
        {
            for (
            ; (reader.NodeType != System.Xml.XmlNodeType.EndElement); 
            )
            {
                if ((reader.NodeType == System.Xml.XmlNodeType.Element))
                {
                    System.Xml.Linq.XElement elem = new System.Xml.Linq.XElement("default");
                    ((System.Xml.Serialization.IXmlSerializable)(elem)).ReadXml(reader);
                    Nodes.Add(elem);
                }
                else
                {
                    reader.Skip();
                }
            }
        }
    }
}
