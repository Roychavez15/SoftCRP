using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SWDLCondelpi;

namespace SoftCRP.Web.Controllers
{
    public class AccountController : Controller
    {
        //private SWDLCondelpi.Service1SoapClient Service1SoapClient = new Service1SoapClient();
        private readonly IUserHelper _userHelper;
        private readonly Service1Soap _service1Soap;
        private readonly IConfiguration _configuration;

        public AccountController(
            IUserHelper userHelper,
            Service1Soap service1Soap,
            IConfiguration configuration)
        {
            _userHelper = userHelper;
            _service1Soap = service1Soap;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var key = _configuration["KeyWs"];

            if(ModelState.IsValid)
            {
                
                var result1 = await _service1Soap.LOGIsAuthenticatedAsync(key, model.Username, model.Password);
                
                if (result1)
                {
                    var user = await _userHelper.GetUserAsync(model.Username);
                    if (user == null)
                    {
                        user = new User
                        {
                            Cedula = "0000000000",
                            FirstName = model.Username,
                            LastName = model.Username,
                            //Email = model.Username,
                            //PhoneNumber = model.PhoneNumber,
                            UserName = model.Username,
                        };
                        var result2 = await _userHelper.AddUserAsync(user, model.Password);
                        if (result2 != IdentityResult.Success)
                        {
                            //this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                            //return this.View(model);
                        }

                        await this._userHelper.AddUserToRoleAsync(user, "Renting");
                        
                    }
                    else
                    {
                        //await _userHelper.ChangePasswordAsync(user,user.PasswordHash)
                    }
                }
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }


            }
            ModelState.AddModelError(string.Empty, "Failed to login.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Register()
        {
            RegisterNewUserViewModel model = new RegisterNewUserViewModel();
            var roles = await _userHelper.GetAllListRoles();
            model.Roles = roles.ToList();
            
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                //var user = await _userHelper.GetUserByEmailAsync(model.Username);
                var user = await _userHelper.GetUserAsync(model.Username);
                
                if (user == null)
                {
                    user = new User
                    {
                        Cedula=model.Cedula,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        PhoneNumber=model.PhoneNumber,
                        UserName = model.Username
                    };

                    var result = await _userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    //await this._userHelper.AddUserToRoleAsync(user, "Cliente");
                    for (int i = 0; i < model.Roles.Count; i++)
                    {
                        //IdentityResult result = null;
                        if (model.Roles[i].IsSelected && !(await _userHelper.IsUserInRoleAsync(user, model.Roles[i].Name)))
                        {
                            await _userHelper.AddUserToRoleAsync(user, model.Roles[i].Name);
                        }
                        else if (!model.Roles[i].IsSelected && (await _userHelper.IsUserInRoleAsync(user, model.Roles[i].Name)))
                        {
                            await _userHelper.RemoveUserToRoleAsync(user, model.Roles[i].Name);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    return this.RedirectToAction(nameof(Index));
                    //var loginViewModel = new LoginViewModel
                    //{
                    //    Password = model.Password,
                    //    RememberMe = false,
                    //    Username = model.Username
                    //};

                    //var result2 = await _userHelper.LoginAsync(loginViewModel);

                    //if (result2.Succeeded)
                    //{
                    //    return this.RedirectToAction("Index", "Home");
                    //}

                    //this.ModelState.AddModelError(string.Empty, "The user couldn't be login.");
                    //return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "The username is already registered.");
            }

            return this.View(model);
        }

        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();
            if (user != null)
            {
                model.Cedula = user.Cedula;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.PhoneNumber = user.PhoneNumber;
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Cedula = model.Cedula;
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;

                    var respose = await _userHelper.UpdateUserAsync(user);
                    if (respose.Succeeded)
                    {
                        this.ViewBag.UserMessage = "User updated!";
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, respose.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return this.View(model);
        }

        public IActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserAsync(model.Username);
                if (user != null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(15),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, results);
                    }
                }
            }

            return this.BadRequest();
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserRoleViewModel>> Index()
        {
            var user =  _userHelper.GetAllListUsers();

            var model = await ToUserRoleViewModel(user.ToList());
            return View(model);
        }

        private async Task<IEnumerable<UserRoleViewModel>> ToUserRoleViewModel(List<User> users)
        {
            var model = new List<UserRoleViewModel>();
            //throw new NotImplementedException();
            foreach(var user in users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    Cedula=user.Cedula,
                    Email=user.Email,
                    FirstName=user.FirstName,
                    LastName=user.LastName,
                    Id=user.Id,
                    PhoneNumber=user.PhoneNumber,
                    UserName=user.UserName,
                };
                var usermodel = await _userHelper.GetUserAsync(user.UserName);
                var roles = await _userHelper.GetAllListRoles(usermodel);
                userRoleViewModel.Roles = roles.ToList();
                model.Add(userRoleViewModel);
            }

            return model;
        }

        public async Task<IActionResult> Edit(string id)
        {
            
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(id);
            if(user==null)
            {
                return NotFound();
            }
            var userRoleViewModel = new UserRoleViewModel
            {
                Cedula = user.Cedula,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
            };
            //var usermodel = await _userHelper.GetUserAsync(user.UserName);

            var roles = await _userHelper.GetAllListRoles(user);
            userRoleViewModel.Roles = roles.ToList();

            return View(userRoleViewModel);
            
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleViewModel userRoleViewModel, string id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                //var useredit = new User
                //{
                //    Id=userRoleViewModel.Id,
                //    Cedula = userRoleViewModel.Cedula,
                //    Email = userRoleViewModel.Email,
                //    FirstName = userRoleViewModel.FirstName,
                //    LastName = userRoleViewModel.LastName,
                //    PhoneNumber = userRoleViewModel.PhoneNumber,
                //    UserName=userRoleViewModel.UserName,
                //    //SecurityStamp=userRoleViewModel.SecurityStamp
                //};

                user.Cedula = userRoleViewModel.Cedula;
                user.Email = userRoleViewModel.Email;
                user.FirstName = userRoleViewModel.FirstName;
                user.LastName = userRoleViewModel.LastName;
                user.PhoneNumber = userRoleViewModel.PhoneNumber;
                //user.UserName = userRoleViewModel.UserName;

                for (int i = 0; i < userRoleViewModel.Roles.Count; i++)
                {
                    //IdentityResult result = null;
                    if (userRoleViewModel.Roles[i].IsSelected && !(await _userHelper.IsUserInRoleAsync(user, userRoleViewModel.Roles[i].Name)))
                    {
                        await _userHelper.AddUserToRoleAsync(user, userRoleViewModel.Roles[i].Name);
                    }
                    else if (!userRoleViewModel.Roles[i].IsSelected && (await _userHelper.IsUserInRoleAsync(user, userRoleViewModel.Roles[i].Name)))
                    {
                        await _userHelper.RemoveUserToRoleAsync(user, userRoleViewModel.Roles[i].Name);
                    }
                    else
                    {
                        continue;
                    }
                }

                await _userHelper.UpdateUserAsync(user);
                

                return RedirectToAction(nameof(Index));
            }
            return View(userRoleViewModel);
        }
    }
}