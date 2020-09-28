using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SWDLCondelpi;

namespace SoftCRP.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Service1Soap _service1Soap;
        private readonly IUserHelper _userHelper;

        public AccountApiController(
            IConfiguration configuration,
            Service1Soap service1Soap,
            IUserHelper userHelper
            )
            
        {
            _configuration = configuration;
            _service1Soap = service1Soap;
            _userHelper = userHelper;
        }

        [HttpPost]
        [Route("LoginApi")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var key = _configuration["KeyWs"];

            if (ModelState.IsValid)
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
                            Email = model.Username + "@condelpi.com",
                            //PhoneNumber = model.PhoneNumber,
                            UserName = model.Username,
                            isActive = true,
                        };
                        var result2 = await _userHelper.AddUserAsync(user, model.Password);
                        if (result2 != IdentityResult.Success)
                        {
                            //this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                            //return this.View(model);
                            return BadRequest();
                        }

                        await this._userHelper.AddUserToRoleAsync(user, "Renting");

                        return Ok();
                    }
                    else
                    {
                        if (!user.isActive)
                        {
                            return BadRequest();
                        }
                        var isInRole = await this._userHelper.IsUserInRoleAsync(user, "Renting");
                        if (isInRole)
                        {
                            var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
                            var resultPass = await _userHelper.ResetPasswordAsync(user, myToken, model.Password);
                            //await _userHelper.ChangePasswordAsync(user, user.PasswordHash, model.Password);                            
                        }
                        return Ok();
                        //await _userHelper.ChangePasswordAsync(user,user.PasswordHash)
                    }

                }
                else
                {
                    //usuario mal clave o no existe
                    

                    var user = await _userHelper.GetUserAsync(model.Username);
                    if (user != null)
                    {
                        //var isInRole = await this._userHelper.IsUserInRoleAsync(user, "Renting");
                        //if (isInRole)
                        //{
                        //    return BadRequest();
                        //}
                        //else
                        //{
                        //    return Ok(); //si es usuario admin creado manualmente en la plataforma y no es usuario renting
                        //}
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            return BadRequest();
        }
    }
}
