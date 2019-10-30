using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static SoftCRP.Web.Enum.Enum;

namespace SoftCRP.Web.Controllers
{
    public abstract class BaseController : Controller
    {

        public void Alert(string message, NotificationType notificationType)
        {
            var msg = "swal('" + notificationType.ToString().ToUpper() + "', '" + message + "','" + notificationType + "')" + "";
            //var msg = "swal({" +
            //    "type: 'success'," +
            //    "title: 'Registro Guardado con Exito',"+
            //    "showConfirmButton: false,"+              
            //    "});";

            TempData["notification"] = msg;

        }
    }
}