using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
            var key = _configuration.GetConnectionString("KeyWs");
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

                        await this._userHelper.AddUserToRoleAsync(user, "Cliente");

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


        public IActionResult Register()
        {
            return this.View();
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

                    await this._userHelper.AddUserToRoleAsync(user, "Cliente");

                    var loginViewModel = new LoginViewModel
                    {
                        Password = model.Password,
                        RememberMe = false,
                        Username = model.Username
                    };

                    var result2 = await _userHelper.LoginAsync(loginViewModel);

                    if (result2.Succeeded)
                    {
                        return this.RedirectToAction("Index", "Home");
                    }

                    this.ModelState.AddModelError(string.Empty, "The user couldn't be login.");
                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "The username is already registered.");
            }

            return this.View(model);
        }
    }
}