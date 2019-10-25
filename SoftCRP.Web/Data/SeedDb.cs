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

    }
}