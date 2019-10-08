using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftCRP.Web.Data;
using SoftCRP.Web.Helpers;

namespace SoftCRP.Web.Controllers
{
    public class InformacionController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserHelper _userHelper;
        private readonly WSDLCondelpiData.Service1Soap _service1Soap;

        public InformacionController(
            DataContext context,
            IConfiguration configuration,
            IUserHelper userHelper,
            WSDLCondelpiData.Service1Soap service1Soap)
        {
            _context = context;
            _configuration = configuration;
            _userHelper = userHelper;
            _service1Soap = service1Soap;
        }
        public async Task<IActionResult> Index()
        {
            var key = _configuration["KeyWs"];

            var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            if(user != null)
            {
                if (this.User.IsInRole("Cliente"))
                {
                    var dataxml = await _service1Soap.Consulta_Data_nit_autoAsync(key, user.Cedula);
                }

            }
            

            return View();
        }
    }
}