using Microsoft.AspNetCore.Identity;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRoles();
            await CheckAnalisisTypesAsync();
            await CheckTramitesTypesAsync();
            await CheckCapacitacionesTypesAsync();

            var user = await this._userHelper.GetUserByEmailAsync("roy_chavez15@hotmail.com");
            if (user == null)
            {
                user = new User
                {
                    Cedula = "0702416637",
                    FirstName = "Roy",
                    LastName = "Chavez",
                    Email = "roy_chavez15@hotmail.com",
                    PhoneNumber="0989605062",
                    UserName = "rchavez"
                };

                var result = await this._userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
                //await this._userHelper.AddUserToRoleAsync(user, "Admin");
            }
            var isInRole = await this._userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this._userHelper.AddUserToRoleAsync(user, "Admin");
            }

        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Renting");
            await _userHelper.CheckRoleAsync("Cliente");
        }
        private async Task CheckAnalisisTypesAsync()
        {
            if (!_context.TiposAnalisis.Any())
            {
                _context.TiposAnalisis.Add(new Entities.TipoAnalisis { Tipo = "Auditoría" });
                _context.TiposAnalisis.Add(new Entities.TipoAnalisis { Tipo = "Informe Gerencial" });
                _context.TiposAnalisis.Add(new Entities.TipoAnalisis { Tipo = "Informe Administración Flota" });
                _context.TiposAnalisis.Add(new Entities.TipoAnalisis { Tipo = "Acta Entrega" });
                _context.TiposAnalisis.Add(new Entities.TipoAnalisis { Tipo = "Acta Recepción" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckTramitesTypesAsync()
        {
            if (!_context.tipoTramites.Any())
            {
                _context.tipoTramites.Add(new Entities.TipoTramite { Tipo = "Matrícula" });
                _context.tipoTramites.Add(new Entities.TipoTramite { Tipo = "Certificado Matrícula" });
                _context.tipoTramites.Add(new Entities.TipoTramite { Tipo = "Titulo Habilitante" });
                _context.tipoTramites.Add(new Entities.TipoTramite { Tipo = "Resolución 085" });
                _context.tipoTramites.Add(new Entities.TipoTramite { Tipo = "Resolución 004 O006" });
                _context.tipoTramites.Add(new Entities.TipoTramite { Tipo = "Pesos y Medidas" });
                _context.tipoTramites.Add(new Entities.TipoTramite { Tipo = "Permiso ARCSA" });
                _context.tipoTramites.Add(new Entities.TipoTramite { Tipo = "Infracciones" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCapacitacionesTypesAsync()
        {
            if (!_context.tipoCapacitaciones.Any())
            {
                _context.tipoCapacitaciones.Add(new Entities.TipoCapacitacion { Tipo = "Como cuidar mi vehículo?" });
                _context.tipoCapacitaciones.Add(new Entities.TipoCapacitacion { Tipo = "Debemos Reportar" });
                _context.tipoCapacitaciones.Add(new Entities.TipoCapacitacion { Tipo = "Cobertura póliza de seguros y exclusiones" });
                _context.tipoCapacitaciones.Add(new Entities.TipoCapacitacion { Tipo = "Siniestros y reclamación" });
                _context.tipoCapacitaciones.Add(new Entities.TipoCapacitacion { Tipo = "Documentos habilitantes" });
                _context.tipoCapacitaciones.Add(new Entities.TipoCapacitacion { Tipo = "Infracciones" });
                _context.tipoCapacitaciones.Add(new Entities.TipoCapacitacion { Tipo = "Como ocurre un accidente de tránsito?" });
                _context.tipoCapacitaciones.Add(new Entities.TipoCapacitacion { Tipo = "Factores que influyen en un accidente de tránsito"});
                _context.tipoCapacitaciones.Add(new Entities.TipoCapacitacion { Tipo = "Daños ocasionados en un accidente de tránsito y consecuencias" });
                _context.tipoCapacitaciones.Add(new Entities.TipoCapacitacion { Tipo = "Limitaciones frente a una conducción segura" });
                await _context.SaveChangesAsync();
            }
        }
    }
}