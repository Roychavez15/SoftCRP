using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftCRP.Controllers.Ituran
{
    public class IturanController : ControllerBase
    {
        // GET: api/<ValuesController>
        WSIturan.LoginInfo login = new WSIturan.LoginInfo();
        WSIturan.OnlineSoapClient cliente = new WSIturan.OnlineSoapClient(WSIturan.OnlineSoapClient.EndpointConfiguration.OnlineSoap12);
        public async Task<WSIturan.CarOnlinePosItemInfo[]> getData()
        {
            login.Username = "PABLOMOYA";
            login.Password = "123456";
            login.Company = "CONSORCIO PICHINCHA";
            object authResponse = cliente.AuthenticateAsync(login.Username, login.Password, login.Company);
            var request = new WSIturan.GetCarsInfoRequest();
            request.LoginInfo = login;
            WSIturan.GetCarsInfoResponse infoCarro = await cliente.GetCarsInfoAsync(request.LoginInfo);
            return infoCarro.GetCarsInfoResult;
        }
    }
}
