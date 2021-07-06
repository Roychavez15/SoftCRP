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
        public IEnumerable<SelectListItem> GetComboMesNew()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem {Text = "Enero", Value = "1"},
                new SelectListItem {Text = "Febrero", Value = "2"},
                new SelectListItem {Text = "Marzo", Value = "3"},
                new SelectListItem {Text = "Abril", Value = "4"},
                new SelectListItem {Text = "Mayo", Value = "5"},
                new SelectListItem {Text = "Junio", Value = "6"},
                new SelectListItem {Text = "Julio", Value = "7"},
                new SelectListItem {Text = "Agosto", Value = "8"},
                new SelectListItem {Text = "Septiembre", Value = "9"},
                new SelectListItem {Text = "Octubre", Value = "10"},
                new SelectListItem {Text = "Noviembre", Value = "11"},
                new SelectListItem {Text = "Diciembre", Value = "12"}
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
                new SelectListItem {Text = (DateTime.Now.Year - 12).ToString(), Value =  (DateTime.Now.Year - 12).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 11).ToString(), Value =  (DateTime.Now.Year - 11).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 10).ToString(), Value =  (DateTime.Now.Year - 10).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 9).ToString(), Value =  (DateTime.Now.Year - 9).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 8).ToString(), Value =  (DateTime.Now.Year - 8).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 7).ToString(), Value =  (DateTime.Now.Year - 7).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 6).ToString(), Value =  (DateTime.Now.Year - 6).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 5).ToString(), Value =  (DateTime.Now.Year - 5).ToString()},
                new SelectListItem {Text = (DateTime.Now.Year - 4).ToString(), Value =  (DateTime.Now.Year - 4).ToString()},
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
        public async Task<IEnumerable<SelectListItem>> GetComboPlacasGPS(string Nit)
        {

            //var list = await _dataContext.vehiculos.Include(u=>u.user)
            //    .Where(a => a.user.Cedula==Nit)
            //    .Select(ta => new SelectListItem
            //{
            //    Text = ta.Placa,
            //    Value = ta.Placa,
            //})
            //.OrderBy(ta => ta.Text)
            //.ToListAsync();

            ////list.Insert(0, new SelectListItem
            ////{
            ////    Text = "[Seleccionar Placa...]",
            ////    Value = "0"
            ////});

            //return list;

            if(Nit=="0")
            {
                var list = await _dataContext.vehiculos.Include(u => u.user)
                    //.Where(a => a.user.Cedula == Nit)
                    .Select(ta => new SelectListItem
                    {
                        Text = ta.Placa,
                        Value = ta.Placa,
                    })
                .OrderBy(ta => ta.Text)
                .ToListAsync();

                list.Insert(0, new SelectListItem
                {
                    Text = "[Seleccionar Placa...]",
                    Value = "0"
                });

                return list;
            }

            return await _dataContext.vehiculos.Include(u => u.user)
                .Where(a => a.user.Cedula == Nit)
                .Select(ta => new SelectListItem
                {
                    Text = ta.Placa,
                    Value = ta.Placa,
                })
            .OrderBy(ta => ta.Text)
            .ToListAsync();

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

        public IEnumerable<SelectListItem> GetComboCiudades()
        {
            var list = _dataContext.ciudades.Where(a => a.isActive == true).Select(ta => new SelectListItem
            {
                Text = ta.Nombre,
                Value = $"{ta.Nombre}",
            })
                .OrderBy(ta => ta.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccionar Ciudad...]",
                Value = ""
            });

            return list;
            //throw new NotImplementedException();
        }
        public IEnumerable<SelectListItem> GetComboDias(string Anio, string Mes)
        {
            List<SelectListItem> myList = new List<SelectListItem>();

            int mes = 0;
            if(Mes=="Enero")
            {
                mes = 1;
            }
            else if(Mes=="Febrero")
            {
                mes = 2;
            }
            else if (Mes == "Marzo")
            {
                mes = 3;
            }
            else if (Mes == "Abril")
            {
                mes = 4;
            }
            else if (Mes == "Mayo")
            {
                mes = 5;
            }
            else if (Mes == "Junio")
            {
                mes = 6;
            }
            else if (Mes == "Julio")
            {
                mes = 7;
            }
            else if (Mes == "Agosto")
            {
                mes = 8;
            }
            else if (Mes == "Septiembre")
            {
                mes = 9;
            }
            else if (Mes == "Octubre")
            {
                mes = 10;
            }
            else if (Mes == "Noviembre")
            {
                mes = 11;
            }
            else if (Mes == "Diciembre")
            {
                mes = 12;
            }
            int days = System.DateTime.DaysInMonth(Convert.ToInt32(Anio), mes);

            for(int i=1;i<=days;i++)
            {
                var data =
                     new SelectListItem
                     {
                         Value = i.ToString(),
                         Text = i.ToString("00"),
                     };
                myList.Add(data);
            }
            return myList.OrderBy(n => n.Text);
        }
        public IEnumerable<SelectListItem> UsuariosConduccion()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            IQueryable<String> list =
                (from usuario in _dataContext.vehiculosGps
                 select usuario.usuario).Distinct();


            int id = 1;
            foreach (var Data in list)
            {
                if (Data != "" || Data!="null")
                {
                    var data =
                     new SelectListItem
                     {
                         Value = Data,
                         Text = Data,
                     };
                    myList.Add(data);
                    id = id + 1;
                }
            };

            return myList.OrderBy(n => n.Text);

        }
        public IEnumerable<SelectListItem> GetComboSubProcesos(string proceso)
        {
            var myList = new List<SelectListItem>();

            if (proceso=="Conducción")
            {
                myList = new List<SelectListItem>
                    {
                        new SelectListItem {Text = "Cantidad de viajes", Value =  "Cantidad de viajes"},
                        new SelectListItem {Text = "Distancia recorrida", Value =  "Distancia recorrida"},
                        new SelectListItem {Text = "Cantidad de eventos", Value =  "Cantidad de eventos"},
                        new SelectListItem {Text = "Aceleraciones bruscas", Value =  "Aceleraciones bruscas"},
                        new SelectListItem {Text = "Frenasos bruscos", Value =  "Frenasos bruscos"},
                        new SelectListItem {Text = "Excesos de velocidad", Value =  "Excesos de velocidad"},
                        new SelectListItem {Text = "Giros bruscos", Value =  "Giros bruscos"},
                        new SelectListItem {Text = "Calificación promedio", Value =  "Calificación promedio"},
                    };
                
            }
            if (proceso == "Mantenimientos")
            {
                myList = new List<SelectListItem>
                    {
                        new SelectListItem {Text = "Tipos", Value =  "Tipos"},
                        new SelectListItem {Text = "Estados", Value =  "Estados"},
                    };

            }
            if (proceso == "Sustitutos")
            {
                myList = new List<SelectListItem>
                    {
                        new SelectListItem {Text = "Días de sustitución", Value =  "Días de sustitución"},
                        new SelectListItem {Text = "Regional", Value =  "Regional"},
                        new SelectListItem {Text = "Estados", Value =  "Estados"},
                    };

            }
            if (proceso == "Siniestros")
            {
                myList = new List<SelectListItem>
                    {
                        new SelectListItem {Text = "Estado", Value =  "Estado"},
                        new SelectListItem {Text = "Tiempo de siniestro", Value =  "Tiempo de siniestro"},
                        new SelectListItem {Text = "Tipo de siniestro", Value =  "Tipo de siniestro"},
                    };

            }
            return myList.OrderBy(n => n.Text);
        }
    }
}
