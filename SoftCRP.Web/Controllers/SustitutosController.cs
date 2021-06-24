using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SoftCRP.Web.Controllers
{
    public class SustitutosController : BaseController
    {
        private readonly IUserHelper _userHelper;
        private readonly IDatosRepository _datosRepository;
        private readonly ICombosHelper _combosHelper;
        private readonly DataContext _context;
        public SustitutosController(
           IUserHelper userHelper,
           IDatosRepository datosRepository,
           DataContext context,
           ICombosHelper combosHelper)
        {
            _userHelper = userHelper;
            _context = context;
            _datosRepository = datosRepository;
            _combosHelper = combosHelper;
        }
        public async Task<IActionResult> Index()
        {
            DashBoardV2ViewModel model = new DashBoardV2ViewModel();
            SustitutosDataViewModel dataViewModel = new SustitutosDataViewModel();

            if (!string.IsNullOrEmpty(this.User.Identity.Name))
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                if (user != null)
                {
                    if (this.User.IsInRole("Cliente"))
                    {
                        model.Meses = _combosHelper.GetComboMes();
                        model.Anios = _combosHelper.GetComboAnio();
                        dataViewModel.DiasSustitutosViewModel = await _datosRepository.GetDiasSustitutosAsync(user.Cedula, "");
                        var cuantos = await _datosRepository.GetCuantosSustitutosAsync(user.Cedula, "", "", "");
                        var gamas = await _context.gamas.ToListAsync();
                        dataViewModel.SustitutosCuantosViewModel = await getCuantos(dataViewModel.DiasSustitutosViewModel);
                    }
                    else if (this.User.IsInRole("Admin") || this.User.IsInRole("Renting"))
                    {
                        model.Meses = _combosHelper.GetComboMes();
                        model.Anios = _combosHelper.GetComboAnio();
                        model.Clientes = _combosHelper.GetComboClientes();
                        dataViewModel.DiasSustitutosViewModel = await _datosRepository.GetDiasSustitutosAsync("", "");
                        var cuantos = await _datosRepository.GetCuantosSustitutosAsync("", "", "", "");
                        dataViewModel.SustitutosCuantosViewModel = await getCuantos(dataViewModel.DiasSustitutosViewModel);
                    }
                }
                dataViewModel.DashBoardV2ViewModel = model;
            }

            return View(dataViewModel);
        }
        public async Task<SustitutosCuantosViewModel> getCuantos(IEnumerable<DiasSustitutosViewModel> data)
        {
            var gamas = await _context.gamas.ToListAsync();
            SustitutosCuantosViewModel viewModel = new SustitutosCuantosViewModel();
            viewModel.sustitutos_utilizados = 0;
            viewModel.ahorro = 0;
            viewModel.ahorro_acumulado = 0;
            foreach (var info in data)
            {
                var confirmacion = gamas.Where<Gama>(u => u.GamaSustituto == info.Gama).ToList();
                var dias = info.Dias;
                if (info.Dias == 0)
                {
                    dias = 0;
                }
                var monto = 0;
                if (confirmacion.Count<Gama>() > 0)
                {
                    monto = confirmacion[0].Monto;
                }
                //var valor = monto * dias;
                //if (info.Placa != "" && info.Estado == "No disponible")
                //{
                //    viewModel.sustitutos_utilizados += 1;
                //    viewModel.ahorro += valor;
                //}
                //else
                //{
                //    viewModel.ahorro_acumulado += valor;
                //}
                var valor = 0;
                if(info.Disponible.ToUpper()=="SI")
                {
                    viewModel.sustitutos_utilizados += 1;
                    valor = monto * info.Dias;
                    viewModel.ahorro += valor;
                }
                else
                {
                    valor = monto * info.Dias_historia;
                    viewModel.ahorro_acumulado += valor;
                }
            }
            var total = viewModel;
            return viewModel;
        }
        public async Task<IActionResult> getDashboardDate(string UserId, string mes, string anio)
        {
            var cuantos = await _datosRepository.GetDiasSustitutosAsync(UserId, "");
            var dataView = new List<DiasSustitutosViewModel>();
            foreach (var data in cuantos)
            {
                if (data.Fecha_asignacion != "")
                {
                    var fecha = data.Fecha_asignacion.Split('-', 'T');
                    if (fecha.Length > 0)
                    {
                        if (mes != null && anio != null)
                        {
                            if (int.Parse(fecha[1]) == getMes(mes) && int.Parse(fecha[0]) == int.Parse(anio))
                            {
                                dataView.Add(data);
                            }
                        }
                        else if (mes != null)
                        {
                            if (int.Parse(fecha[1]) == getMes(mes))
                            {
                                dataView.Add(data);
                            }
                        }
                        else if (anio != null)
                        {
                            if (int.Parse(fecha[0]) == int.Parse(anio))
                            {
                                dataView.Add(data);
                            }
                        }
                        else if (mes == null && anio == null)
                        {
                            dataView.Add(data);
                        }
                    }
                }
            }


            var result = await getCuantos(dataView);
            return PartialView("_DashboardSustPartialView", result);
        }
        public async Task<IActionResult> GetEstadisticasV2(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                UserId = "";
            }

            var resultado = await _datosRepository.GetDiasSustitutosAsync(UserId, "");
            return PartialView("_ResumenPartialView", resultado);
        }

        public async Task<IActionResult> GetEstadisticasV2all()
        {
            var resultado = await _datosRepository.GetDiasSustitutosAsync("", "");
            return PartialView("_ResumenPartialView", resultado);
        }
        public int getMes(string mes)
        {
            int messalida = 0;

            if (mes.Equals("Enero"))
            {
                messalida = 1;
            }
            else if (mes.Equals("Febrero"))
            {
                messalida = 2;
            }
            else if (mes.Equals("Marzo"))
            {
                messalida = 3;
            }
            else if (mes.Equals("Abril"))
            {
                messalida = 4;
            }
            else if (mes.Equals("Mayo"))
            {
                messalida = 5;
            }
            else if (mes.Equals("Junio"))
            {
                messalida = 6;
            }
            else if (mes.Equals("Julio"))
            {
                messalida = 7;
            }
            else if (mes.Equals("Agosto"))
            {
                messalida = 8;
            }
            else if (mes.Equals("Septiembre"))
            {
                messalida = 9;
            }
            else if (mes.Equals("Octubre"))
            {
                messalida = 10;
            }
            else if (mes.Equals("Noviembre"))
            {
                messalida = 11;
            }
            else if (mes.Equals("Diciembre"))
            {
                messalida = 12;
            }
            return messalida;
        }
    }
}
