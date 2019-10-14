using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public UserHelper(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }
        public async Task RemoveUserToRoleAsync(User user, string roleName)
        {
            await _userManager.RemoveFromRoleAsync(user, roleName);
        }
        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<User> GetUserAsync(string user)
        {
            //throw new System.NotImplementedException();
            return await _userManager.FindByNameAsync(user);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {            
            return await _userManager.FindByEmailAsync(email); ;
        }
        public async Task<User> GetUserByIdAsync(string Id)
        {
            return await _userManager.FindByIdAsync(Id); ;
        }
        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }
        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }
        public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(
                user,
                password,
                false);
        }

        public IEnumerable<User> GetAllListUsers()
        {            
            return _userManager.Users;
            //throw new System.NotImplementedException();
        }
        public async Task<IEnumerable<RoleViewModel>> GetAllListRoles(User user)
        {
            var roles = _roleManager.Roles.OrderBy(r => r.Name);
            var model = new List<RoleViewModel>();
            foreach(var role in roles)
            {
                var roleViewModel = new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                };

                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    roleViewModel.IsSelected = true;
                }
                else
                {
                    roleViewModel.IsSelected = false;
                }
                model.Add(roleViewModel);
            }
            return model;
            //throw new System.NotImplementedException();
        }
        public async Task<IEnumerable<RoleViewModel>> GetAllListRoles()
        {
            var roles = _roleManager.Roles.OrderBy(r => r.Name);
            var model = new List<RoleViewModel>();
            foreach (var role in roles)
            {
                var roleViewModel = new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                };
                roleViewModel.IsSelected = false;
                model.Add(roleViewModel);
            }
            return model;
            //throw new System.NotImplementedException();
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }

    }
}
