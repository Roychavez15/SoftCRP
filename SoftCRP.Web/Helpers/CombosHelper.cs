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
            //var list = _datosRepository.GetPlacasClienteAsync(nit).Select(ta => new SelectListItem
            //{
            //    Text = ta,
            //    Value = $"{ta.Id}",
            //})
            //    .OrderBy(ta => ta.Text)
            //    .ToList();

            //list.Insert(0, new SelectListItem
            //{
            //    Text = "(Seleccionar una Placa...)",
            //    Value = "0"
            //});
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

            //myList.Insert(0, new SelectListItem
            //{
            //    Text = "(Seleccionar una Placa...)",
            //    Value = ""
            //});

            myList.Insert(0, new SelectListItem
            {
                Text = "Flota",
                Value = "Flota"
            });

            return myList;

            //throw new NotImplementedException();
        }
    }
}
