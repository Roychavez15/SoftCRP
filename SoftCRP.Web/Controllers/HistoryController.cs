using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftCRP.Web.Data;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Controllers
{
    [Authorize(Roles = "Admin,Renting")]
    public class HistoryController : BaseController
    {
        private readonly DataContext _dataContext;
        private readonly DatosRepository _datosRepository;
        private readonly IUserHelper _userHelper;
        private readonly ILogRepository _logRepository;

        public HistoryController(
            DataContext dataContext,
            DatosRepository datosRepository, 
            IUserHelper userHelper,
            ILogRepository logRepository)
        {
            _dataContext = dataContext;
            _datosRepository = datosRepository;
            _userHelper = userHelper;
            _logRepository = logRepository;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            if (user != null)
            {
                var clientes = await _userHelper.GetListUsersInRole("Cliente");
                await _logRepository.SaveLogs("Get", "Obtiene Lista de Clientes", "Historial", User.Identity.Name);
                return View(clientes);
            }
            return View();
        }
    }
}
