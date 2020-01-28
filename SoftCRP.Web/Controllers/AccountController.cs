using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Models;
using SoftCRP.Web.Repositories;
using SWDLCondelpi;

namespace SoftCRP.Web.Controllers
{
    public class AccountController : Controller
    {
        //private SWDLCondelpi.Service1SoapClient Service1SoapClient = new Service1SoapClient();
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly Service1Soap _service1Soap;
        private readonly IConfiguration _configuration;
        private readonly ILogRepository _logRepository;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        //private readonly ILog _logger;

        public AccountController(
            IUserHelper userHelper,
            IMailHelper mailHelper,
            Service1Soap service1Soap,
            IConfiguration configuration,
            ILogRepository logRepository,
            ILogger<AccountController> logger,
            ILoggerFactory loggerFactory
            //ILog logger
            )
        {
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _service1Soap = service1Soap;
            _configuration = configuration;
            _logRepository = logRepository;
            _logger = logger;
            _loggerFactory = loggerFactory;
            //_logger = logger;
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
                            Email = model.Username+"@condelpi.com",
                            //PhoneNumber = model.PhoneNumber,
                            UserName = model.Username,
                            isActive=true,
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
                        if(!user.isActive)
                        {
                            ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Login", "Error en Loguear Usuario, Usuario Desactivado", SweetAlertMessageType.error);
                            //ModelState.AddModelError(string.Empty, "Fallo en Loguear Usuario.");
                            return View(model);
                        }
                        var isInRole = await this._userHelper.IsUserInRoleAsync(user, "Renting");
                        if (isInRole)
                        {
                            var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
                            var resultPass = await _userHelper.ResetPasswordAsync(user, myToken, model.Password);
                            //await _userHelper.ChangePasswordAsync(user, user.PasswordHash, model.Password);                            
                        }
                        //await _userHelper.ChangePasswordAsync(user,user.PasswordHash)
                    }
                    
                }
                else
                {
                    var user = await _userHelper.GetUserAsync(model.Username);
                    if (user != null)
                    {
                        var isInRole = await this._userHelper.IsUserInRoleAsync(user, "Renting");
                        if (isInRole)
                        {
                            ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Login", "Error en Loguear Usuario, Usuario Incorrecto", SweetAlertMessageType.error);
                            //ModelState.AddModelError(string.Empty, "Fallo en Loguear Usuario.");
                            return View(model);
                        }
                    }
                    else
                    {
                        ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Login", "Error en Loguear Usuario, Usuario Incorrecto", SweetAlertMessageType.error);
                        //ModelState.AddModelError(string.Empty, "Fallo en Loguear Usuario.");
                        return View(model);
                    }
                }

                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    var user = await _userHelper.GetUserAsync(model.Username);

                    if (!user.isActive)
                    {
                        await _logRepository.SaveLogs("Error", "Usuario Desactivado", "Account", model.Username);
                        ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Login", "Error en Loguear Usuario, Usuario Desactivado", SweetAlertMessageType.error);
                        await _userHelper.LogoutAsync();
                        //ModelState.AddModelError(string.Empty, "Fallo en Loguear Usuario.");
                        return View(model);
                    }

                    await _logRepository.SaveLogs("Success", "Login", "Account", model.Username);

                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");

                }
                else
                {                    
                    ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Login", "Error en Loguear Usuario", SweetAlertMessageType.error);
                    //ModelState.AddModelError(string.Empty, "Fallo en Loguear Usuario.");
                    return View(model);
                }

            }
            //ModelState.AddModelError(string.Empty, "Fallo en Loguear Usuario.");
            ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Login", "Fallo en Loguear Usuario", SweetAlertMessageType.error);
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _logRepository.SaveLogs("Success", "Logout", "Account", User.Identity.Name);
            await _userHelper.LogoutAsync();
            //_logger.Information("Logout User :"+ User.Identity.Name);

            //IS OK
            //_logger.LogCritical("Logout User :" + User.Identity.Name);

            //var logger1 = _loggerFactory.CreateLogger("LoggerCategory");
            //logger1.LogInformation("Calling the ping action");

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
                var pathFoto = string.Empty;                

                if (model.FotoFile != null && model.FotoFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    //var file = $"{guid}.jpg";
                    var file = $"{model.Username}.jpg";
                    pathFoto = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Usuarios",
                        file);

                    using (var stream = new FileStream(pathFoto, FileMode.Create))
                    {
                        await model.FotoFile.CopyToAsync(stream);
                    }

                    pathFoto = $"~/images/Usuarios/{file}";
                }
                //else
                //{
                //    pathFoto = $"~/images/usuario.jpg";
                //}
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
                        UserName = model.Username,
                        isActive=true,
                        ImageUrl=pathFoto
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

                    await _logRepository.SaveLogs("Success", "Registrar Usuario:" + model.Username, "Account", User.Identity.Name);
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

                //this.ModelState.AddModelError(string.Empty, "The username is already registered.");
                await _logRepository.SaveLogs("Error", "Registrar Usuario:" + model.Username, "Account", User.Identity.Name);
                ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Registrar Usuario", "El usuario esta actualmente registrado", SweetAlertMessageType.error);
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
                model.ImageUrl = user.ImageUrl;
                model.UserName = user.UserName;
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var pathFoto = model.ImageUrl;
                var guid = Guid.NewGuid().ToString();
                //var file = $"{guid}.jpg";
                var file = $"{model.UserName}.jpg";

                if (model.FotoFile != null && model.FotoFile.Length > 0)
                {
                    pathFoto = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Usuarios",
                        file);

                    using (var stream = new FileStream(pathFoto, FileMode.Create))
                    {
                        await model.FotoFile.CopyToAsync(stream);
                    }

                    pathFoto = $"~/images/Usuarios/{file}";
                }

                var user = await _userHelper.GetUserAsync(this.User.Identity.Name);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Cedula = model.Cedula;
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                    user.ImageUrl = pathFoto;
                    user.UserName = model.UserName;
                    
                    var respose = await _userHelper.UpdateUserAsync(user);
                    if (respose.Succeeded)
                    {
                        //this.ViewBag.UserMessage = "User updated!";
                        await _logRepository.SaveLogs("Success", "Actualiza Usuario: " + model.UserName, "Account", User.Identity.Name);
                        ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Actualizar", "Usuario Actualizado", SweetAlertMessageType.success);
                    }
                    else
                    {
                        await _logRepository.SaveLogs("Error", "Actualiza Usuario: " + model.UserName, "Account", User.Identity.Name);
                        //this.ModelState.AddModelError(string.Empty, respose.Errors.FirstOrDefault().Description);
                        ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Actualizar", respose.Errors.FirstOrDefault().Description, SweetAlertMessageType.error);
                        
                    }
                }
                else
                {
                    await _logRepository.SaveLogs("Error", "Actualiza Usuario: " + model.UserName + " no encontrado", "Account", User.Identity.Name);
                    //this.ModelState.AddModelError(string.Empty, "User no found.");
                    ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Actualizar", "Usuario No Encontrado", SweetAlertMessageType.error);
                    

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
                        await _logRepository.SaveLogs("Success", "Cambiar Clave Usuario", "Account", User.Identity.Name);
                        ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Actualizar Password", "Password Actualizado", SweetAlertMessageType.success);
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        await _logRepository.SaveLogs("Error", "Cambiar Clave Usuario", "Account", User.Identity.Name);
                        ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Actualizar Password", result.Errors.FirstOrDefault().Description, SweetAlertMessageType.error);
                    }
                }
                else
                {
                    await _logRepository.SaveLogs("Error", "Cambiar Clave Usuario ", "Account", User.Identity.Name);
                    ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Actualizar Password", "Usuario no encontrado", SweetAlertMessageType.error);
                    //this.ModelState.AddModelError(string.Empty, "User no found.");
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

        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = await _userHelper.GetUserByEmailAsync(model.Email);
                var user = await _userHelper.GetUserAsync(model.user);
                if (user == null)
                {
                    //ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                    ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Recuperar Password", "El usuario no esta registrado", SweetAlertMessageType.error);
                    return View(model);
                }
                var isInRole = await this._userHelper.IsUserInRoleAsync(user, "Renting");
                if (isInRole)
                {
                    ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Recuperar Password", "Este usuario no tiene permisos para recuperar contraseña", SweetAlertMessageType.error);
                    return View(model);
                }

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
                var link = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme, "clientes.rentingpichincha.com");
                    //new { token = myToken }, protocol: HttpContext.Request.Scheme, "181.112.216.3:8084");


                _mailHelper.SendMail(user.Email, "Plataforma Clientes Renting", $"<h1>Recuperar Contraseña</h1>" +
                    $"Para cambiar la contraseña haga click en este Link:</br></br>" +
                    $"<a href = \"{link}\">Reset Password</a>");


                //ViewBag.Message = "The instructions to recover your password has been sent to email.";
                ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Recuperar Password", "Las Instrucciones para recuperar su clave fueron enviadas a su correo", SweetAlertMessageType.info);
                await _logRepository.SaveLogs("Success", "Recuperar Clave Email: "+ user.Email, "Account", user.UserName);
                return View();

            }

            return View(model);
        }

        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            //var user = await _userHelper.GetUserByEmailAsync(model.UserName);
            var user = await _userHelper.GetUserAsync(model.UserName);
            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    //   ViewBag.Message = "Password reset successful.";
                    await _logRepository.SaveLogs("Success", "Resetear Clave Usuario: " + model.UserName, "Account", model.UserName);
                    ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Resetear Password", "Password Actualizado", SweetAlertMessageType.success);
                    return View();
                    //return RedirectToAction("Index", "Home");
                }
                await _logRepository.SaveLogs("Error", "Resetear Clave Usuario: " + model.UserName+ " Result: "+ result.Errors.FirstOrDefault().Description, "Account", model.UserName);
                //ViewBag.Message = "Error while resetting the password.";
                ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Resetear Password", "Error en resetear Password", SweetAlertMessageType.error);
                return View(model);
            }

            //ViewBag.Message = "User not found.";
            await _logRepository.SaveLogs("Error", "Resetear Clave Usuario: " + model.UserName, "Account", model.UserName);
            ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Resetear Password", "Usuario No encontrado", SweetAlertMessageType.error);
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserRoleViewModel>> Index()
        {
            var user =  _userHelper.GetAllListUsers();

            var model = await ToUserRoleViewModel(user.ToList());

            await _logRepository.SaveLogs("Get", "Obtener Lista de Usuarios", "Account", User.Identity.Name);
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
                    Cedula = user.Cedula,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    ImageUrl = user.ImageUrl,
                    isActive = user.isActive,
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
                ImageUrl=user.ImageUrl,
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
                var pathFoto = userRoleViewModel.ImageUrl;
                

                if (userRoleViewModel.FotoFile != null && userRoleViewModel.FotoFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    //var file = $"{guid}.jpg";
                    var file = $"{userRoleViewModel.UserName}.jpg";
                    pathFoto = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Usuarios",
                        file);

                    using (var stream = new FileStream(pathFoto, FileMode.Create))
                    {
                        await userRoleViewModel.FotoFile.CopyToAsync(stream);
                    }

                    pathFoto = $"~/images/Usuarios/{file}";
                }

                user.Cedula = userRoleViewModel.Cedula;
                user.Email = userRoleViewModel.Email;
                user.FirstName = userRoleViewModel.FirstName;
                user.LastName = userRoleViewModel.LastName;
                user.PhoneNumber = userRoleViewModel.PhoneNumber;
                user.ImageUrl = pathFoto;
                user.UserName = userRoleViewModel.UserName;

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

                var result = await _userHelper.UpdateUserAsync(user);
                if (result.Succeeded)
                {
                    await _logRepository.SaveLogs("Success", "Editar Usuario: "+user.UserName, "Account", User.Identity.Name);

                    ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Actualizar Usuario", "Usuario Actualizado", SweetAlertMessageType.success);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await _logRepository.SaveLogs("Error", "Editar Usuario: " + user.UserName, "Account", User.Identity.Name);
                    ViewBag.SweetAlertShowMessage = SweetAlertHelper.ShowMessage("Actualizar Usuario", result.Errors.FirstOrDefault().Description, SweetAlertMessageType.error);
                }
            }
            
            return View(userRoleViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id=="")
            {

                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(id);

            if (user != null)
            {
                user.isActive = false;
                await _userHelper.EnableDisableUser(user, false);
                await _logRepository.SaveLogs("Desactiva Usuario", "Usuario Id: " + id.ToString(), "Account", User.Identity.Name);
            }

            return RedirectToAction("Index", "Account");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Enable(string id)
        {
            if (id == "")
            {

                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(id);

            if (user != null)
            {
                user.isActive = true;
                await _userHelper.EnableDisableUser(user, true);
                await _logRepository.SaveLogs("Activar Usuario", "Usuario Id: " + id.ToString(), "Account", User.Identity.Name);
            }

            return RedirectToAction("Index", "Account");
        }
        public async Task<IActionResult> NotAuthorized()
        {
            await _logRepository.SaveLogs("Error", "Página no autorizada", "Account", User.Identity.Name);
            return View();
        }
    }
}