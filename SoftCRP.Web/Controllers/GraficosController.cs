using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Controllers
{
    public class GraficosController : BaseController
    {
        public IActionResult Index()
        {
            string markers = "[";
            markers += "{";
            markers += string.Format("'title': '{0}',", "Casa");
            markers += string.Format("'lat': '{0}',", "-0.2107683");
            markers += string.Format("'lng': '{0}',", "-78.4865491");
            markers += string.Format("'description': '{0}'", "Casa");
            markers += "},";
            markers += "];";
            ViewBag.Markers = markers;
            return View();
        }
    }
}
