using Microsoft.AspNetCore.Identity;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using System;
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

            var user = await this._userHelper.GetUserByEmailAsync("roy_chavez15@hotmail.com");
            if (user == null)
            {
                user = new User
                {
                    Cedula = "0702416637",
                    FirstName = "Roy",
                    LastName = "Chavez",
                    Email = "roy_chavez15@hotmail.com",
                    UserName = "roy_chavez15@hotmail.com"
                };

                var result = await this._userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
                await this._userHelper.AddUserToRoleAsync(user, "Admin");
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
            await _userHelper.CheckRoleAsync("Usuario");
        }

    }
}