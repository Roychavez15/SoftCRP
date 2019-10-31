using Microsoft.AspNetCore.Mvc.Rendering;
using SoftCRP.Web.Data;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly IDatosRepository _datosRepository;
        private readonly DataContext _dataContext;

        public CombosHelper(
            IDatosRepository datosRepository,
            DataContext dataContext)
        {
            _datosRepository = datosRepository;
            _dataContext = dataContext;
        }
        public IEnumerable<SelectListItem> GetComboTipoAnalisis()
        {
            var list = _dataContext.TiposAnalisis.Select(ta => new SelectListItem
            {
                Text = ta.Tipo,
                Value = $"{ta.Id}",
            })
                .OrderBy(ta => ta.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccionar un tipo de Analisis...)",
                Value = "0"
            });

            return list;
            //throw new NotImplementedException();
        }
        public async Task<IEnumerable<SelectListItem>> GetComboPlacas(string nit)
        {

            List<VehiculosClientesViewModel> placas = new List<VehiculosClientesViewModel>();

            placas = await _datosRepository.GetVehiculosClienteAsync(nit);
            List<SelectListItem> myList = new List<SelectListItem>();
            int id = 1;
            foreach (var Data in placas)
            {

                var data =
                 new SelectListItem
                 {
                     Value = Data.placa,
                     Text = Data.placa
                 };
                myList.Add(data);
                id = id + 1;
            };
            myList.Insert(0, new SelectListItem
            {
                Text = "Flota",
                Value = "Flota"
            });

            return myList;

            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<SelectListItem>> GetComboTipoNovedades()
        {
            List<TiposIncidenciaViewModel> tipos = new List<TiposIncidenciaViewModel>();

            tipos = await _datosRepository.GetTipoIncidenciasAsync();
            List<SelectListItem> myList = new List<SelectListItem>();
            int id = 1;
            foreach (var Data in tipos)
            {

                var data =
                 new SelectListItem
                 {
                     Value = Data.Tipo,
                     Text = Data.Tipo
                 };
                myList.Add(data);
                id = id + 1;
            };

            myList.Insert(0, new SelectListItem
            {
                Text = "(Seleccionar un tipo de Novedad...)",
                Value = ""
            });

            return myList;
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<SelectListItem>> GetComboSubMotivos()
        {
            List<SubMotivosIncidenciasViewModel> tipos = new List<SubMotivosIncidenciasViewModel>();

            tipos = await _datosRepository.GetSubMotivosIncidenciasAsync();
            List<SelectListItem> myList = new List<SelectListItem>();
            int id = 1;
            foreach (var Data in tipos)
            {

                var data =
                 new SelectListItem
                 {
                     Value = Data.Submotivo,
                     Text = Data.Submotivo
                 };
                myList.Add(data);
                id = id + 1;
            };

            myList.Insert(0, new SelectListItem
            {
                Text = "(Seleccionar un SubMotivo...)",
                Value = ""
            });

            return myList;
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<SelectListItem>> GetComboViaIngreso()
        {
            List<ViaIngresoViewModel> tipos = new List<ViaIngresoViewModel>();

            tipos = await _datosRepository.GetViaIngresoAsync();
            List<SelectListItem> myList = new List<SelectListItem>();
            int id = 1;
            foreach (var Data in tipos)
            {

                var data =
                 new SelectListItem
                 {
                     Value = Data.Estado,
                     Text = Data.Estado
                 };
                myList.Add(data);
                id = id + 1;
            };

            myList.Insert(0, new SelectListItem
            {
                Text = "(Seleccionar una Via de Ingreso...)",
                Value = ""
            });

            return myList;
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<SelectListItem>> GetComboEstadoNovedad()
        {
            List<EstadoIncidenciaViewModel> tipos = new List<EstadoIncidenciaViewModel>();

            tipos = await _datosRepository.GetEstadosIncidenciaAsync();
            List<SelectListItem> myList = new List<SelectListItem>();
            int id = 1;
            foreach (var Data in tipos)
            {

                var data =
                 new SelectListItem
                 {
                     Value = Data.Estado,
                     Text = Data.Estado
                 };
                myList.Add(data);
                id = id + 1;
            };

            myList.Insert(0, new SelectListItem
            {
                Text = "(Seleccionar un Estado...)",
                Value = ""
            });

            return myList;
            //throw new NotImplementedException();
        }
    }
}
