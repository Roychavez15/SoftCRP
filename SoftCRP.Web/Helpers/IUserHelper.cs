﻿using Microsoft.AspNetCore.Identity;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(string Id);
        Task<User> GetUserAsync(string user);
        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task RemoveUserToRoleAsync(User user, string roleName);
        
        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        Task<SignInResult> ValidatePasswordAsync(User user, string password);

        IEnumerable<User> GetAllListUsers();

        Task<IEnumerable<RoleViewModel>> GetAllListRoles(User user);
        Task<IEnumerable<RoleViewModel>> GetAllListRoles();


        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);

        Task<IEnumerable<User>> GetListUsersInRole(string Role);

        Task<User> GetUserByCedulaAsync(string cedula);

        Task<IdentityResult> EnableDisableUser(User user, bool option);

    }
}
