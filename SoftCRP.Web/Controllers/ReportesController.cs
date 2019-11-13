using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SoftCRP.Web.Controllers
{
    public class ReportesController : Controller
    {
        public IActionResult IndexAnalisis()
        {
            return View();
        }
    }
}