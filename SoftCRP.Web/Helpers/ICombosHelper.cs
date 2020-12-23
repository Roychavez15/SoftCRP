using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboMesNew();
        IEnumerable<SelectListItem> GetComboDias(string Anio, string Mes);
        IEnumerable<SelectListItem> GetComboCiudades();
        Task<IEnumerable<SelectListItem>> GetComboPlacasGPS(string Nit);
        IEnumerable<SelectListItem> GetComboAnio();
        IEnumerable<SelectListItem> GetComboMes();
        IEnumerable<SelectListItem> GetComboTipoAnalisis();
        IEnumerable<SelectListItem> GetComboTipoTramites();
        IEnumerable<SelectListItem> GetComboTipoCapacitaciones();
        Task<IEnumerable<SelectListItem>> GetComboPlacas(string nit);

        Task<IEnumerable<SelectListItem>> GetComboPlacasSN(string nit);

        Task<IEnumerable<SelectListItem>> GetComboTipoNovedades();

        Task<IEnumerable<SelectListItem>> GetComboSubMotivos();
        Task<IEnumerable<SelectListItem>> GetComboViaIngreso();

        Task<IEnumerable<SelectListItem>> GetComboEstadoNovedad();

        IEnumerable<SelectListItem> GetComboClientes();
    }
}
