using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IUserHelper _userHelper;
        
        private readonly IDatosRepository _datosRepository;
        private readonly DataContext _dataContext;

        public ClientesController(
            IUserHelper userHelper,
            IDatosRepository datosRepository,
            DataContext dataContext)
        {
            _userHelper = userHelper;
            _datosRepository = datosRepository;
            _dataContext = dataContext;
        }
        [HttpGet]
        [Route("GetClientes")]
        public async Task<IEnumerable<User>> GetClientesAsync()
        {
            return await _userHelper.GetListUsersInRole("Cliente");
        }

        [HttpPost]
        [Route("GetClientesFlota")]
        public async Task<IEnumerable<VehiculosClientesViewModel>> GetClientesFlotaAsync([FromForm] string cedula)
        {            
            return await _datosRepository.GetVehiculosClienteAsync(cedula);
            
        }
    }
}
