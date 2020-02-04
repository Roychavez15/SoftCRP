using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
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
        private readonly IUserHelper _userHelper;
        private readonly DataContext _dataContext;

        public CombosHelper(
            IDatosRepository datosRepository,
            IUserHelper userHelper,
            DataContext dataContext)
        {
            _datosRepository = datosRepository;
            _userHelper = userHelper;
            _dataContext = dataContext;
        }
        public IEnumerable<SelectListItem> GetComboMes()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem {Text = "Enero", Value = "Enero"},
                new SelectListItem {Text = "Febrero", Value = "Febrero"},
                new SelectListItem {Text = "Marzo", Value = "Marzo"},
                new SelectListItem {Text = "Abril", Value = "Abril"},
                new SelectListItem {Text = "Mayo", Value = "Mayo"},
                new SelectListItem {Text = "Junio", Value = "Junio"},
                new SelectListItem {Text = "Julio", Value = "Julio"},
                new SelectListItem {Text = "Agosto", Value = "Agosto"},
                new SelectListItem {Text = "Septiembre", Value = "Septiembre"},
                new SelectListItem {Text = "Octubre", Value = "Octubre"},
                new SelectListItem {Text = "Noviembre", Value = "Noviembre"},
                new SelectListItem {Text = "Diciembre", Value = "Diciembre"}                
            };


            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccionar un Mes...)",
                Value = ""
            });

            return list;
            //throw new NotImplementedException();
        }
        public IEnumerable<SelectListItem> GetComboAnio()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem {Text = (DateTime.Now.Year - 12).ToString(), Value =  (DateTime.Now.Year - 3).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 11).ToString(), Value =  (DateTime.Now.Year - 3).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 10).ToString(), Value =  (DateTime.Now.Year - 3).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 9).ToString(), Value =  (DateTime.Now.Year - 3).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 8).ToString(), Value =  (DateTime.Now.Year - 3).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 7).ToString(), Value =  (DateTime.Now.Year - 3).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 6).ToString(), Value =  (DateTime.Now.Year - 3).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 5).ToString(), Value =  (DateTime.Now.Year - 3).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 4).ToString(), Value =  (DateTime.Now.Year - 3).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 3).ToString(), Value =  (DateTime.Now.Year - 3).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 2).ToString(), Value =  (DateTime.Now.Year - 2).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 1).ToString(), Value =  (DateTime.Now.Year - 1).ToString()},                
                new SelectListItem {Text = DateTime.Now.Year.ToString(), Value = DateTime.Now.Year.ToString()},
                new SelectListItem {Text = (DateTime.Now.Year + 1).ToString(), Value =  (DateTime.Now.Year + 1).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year + 2).ToString(), Value =  (DateTime.Now.Year + 2).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year + 3).ToString(), Value =  (DateTime.Now.Year + 3).ToString()},
            };


            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccionar un Año...)",
                Value = ""
            });

            return list;
            //throw new NotImplementedException();
        }
        public IEnumerable<SelectListItem> GetComboTipoAnalisis()
        {
            var list = _dataContext.TiposAnalisis.Where(a => a.isActive == true).Select(ta => new SelectListItem
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
        public IEnumerable<SelectListItem> GetComboTipoTramites()
        {
            var list = _dataContext.tipoTramites.Where(a => a.isActive == true).Select(ta => new SelectListItem
            {
                Text = ta.Tipo,
                Value = $"{ta.Id}",
            })
            .OrderBy(ta => ta.Text)
            .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccionar un tipo de Trámite...)",
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

        public async Task<IEnumerable<SelectListItem>> GetComboPlacasSN(string nit)
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
            //myList.Insert(0, new SelectListItem
            //{
            //    Text = "Flota",
            //    Value = "Flota"
            //});

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

        public IEnumerable<SelectListItem> GetComboTipoCapacitaciones()
        {
            var list = _dataContext.tipoCapacitaciones.Where(a => a.isActive == true).Select(ta => new SelectListItem
            {
                Text = ta.Tipo,
                Value = $"{ta.Id}",
            })
                .OrderBy(ta => ta.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccionar un tipo de Capacitación...)",
                Value = "0"
            });

            return list;
            //throw new NotImplementedException();
        }
        public IEnumerable<SelectListItem> GetComboClientes()
        {
            List<SelectListItem> myList = new List<SelectListItem>();

            var Listaclientes = _userHelper.GetListUsersInRole("Cliente");

            int id = 1;
            foreach (var Data in Listaclientes.Result)
            {

                var data =
                 new SelectListItem
                 {
                     Value = Data.Cedula,
                     Text = Data.FullName,
                 };
                myList.Add(data);
                id = id + 1;
            };


            myList.Insert(0, new SelectListItem
            {
                Text = "(..Todos..)",
                Value = ""
            });

            return myList.OrderBy(n=>n.Text);
            //throw new NotImplementedException();
        }
    }
}
