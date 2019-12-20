using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Helpers;
using SoftCRP.Web.Repositories;
using SWDLCondelpi;

namespace SoftCRP.Web
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            

            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                cfg.User.RequireUniqueEmail = false;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 0;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<DataContext>();
            //


            //////
            services.ConfigureApplicationCookie(options =>
            {
                //options.LoginPath = "/Account/NotAuthorized";
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/NotAuthorized";
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
            ////
            ///
            services.AddAuthentication()
            //.AddCookie(options => {
            //    options.LoginPath = "/Account/Login/";
            //    options.SlidingExpiration = false;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
            //})
            .AddCookie()
            .AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = this.Configuration["Tokens:Issuer"],
                    ValidAudience = this.Configuration["Tokens:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Tokens:Key"]))
                };
            });
            

            services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddSingleton(_env.ContentRootFileProvider); //Inject IFileProvider

            //services.AddSingleton<IFileProvider>(
            //        new PhysicalFileProvider($"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\Analisis"));

            services.AddTransient<SeedDb>();//
            services.AddScoped<IUserHelper, UserHelper>();//
            services.AddScoped<IDatosRepository, DatosRepository>();
            services.AddScoped<IMailHelper, MailHelper>();
            services.AddScoped<ITiposAnalisis, TiposAnalisis>();            
            services.AddScoped<ICombosHelper, CombosHelper>();
            services.AddScoped<IFileHelper, FileHelper>();

            services.AddScoped<IAnalisisRepository, AnalisisRepository>();
            services.AddScoped<INovedadesRepository, NovedadesRepository>();
            services.AddScoped<ITramitesRepository, TramitesRepository>();
            services.AddScoped<ICapacitacionesRepository, CapacitacionesRepository>();

            services.AddScoped<ILogRepository, LogRepository>();


            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            var url = Configuration["WsdlUser"];
             
            services.AddScoped<Service1Soap>(provider =>
            {
                BasicHttpBinding result = new BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.MaxBufferPoolSize = int.MaxValue;
                //result.Security = BasicHttpSecurityMode.None;

                var client = new Service1SoapClient(
                      
                      //new BasicHttpBinding(BasicHttpSecurityMode.None),
                      result,
                      new EndpointAddress(url));
                return client;
            });

            var url1 = Configuration["WsdlData"];
            services.AddScoped<WSDLCondelpiData.Service1Soap>(provider =>
            {
                BasicHttpBinding result = new BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.MaxBufferPoolSize = int.MaxValue;

                var client1 = new WSDLCondelpiData.Service1SoapClient(
                    //new BasicHttpBinding(BasicHttpSecurityMode.None),
                    result,
                    new EndpointAddress(url1));
                return client1;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");//

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();//
            app.UseCookiePolicy();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
