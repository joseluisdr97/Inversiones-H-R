using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MADERERA_HRS.DB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MADERERA_HRS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //ESTA ES MI CONEXION CON EL SQL SERVER - TENER CUIDADO AL NOMBRAR EL CONTEXTO APPCONTEXT PORQUE SE CRUZAN ENTRE CLASES
            services.AddDbContext<AppContextDB>(
            o => o.UseSqlServer("Server=JOSE-LUIS;Database=DB_MADERERA_HRS;User Id=MADERERA_HRS;Password=12345;Trusted_Connection=False;MultipleActiveResultSets=True")
            );
            //ESTO SE INGRESA PARA PODER UTILIZAR LA SESION - COOKIE
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
              AddCookie(o => o.LoginPath = "/Auth/Login");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //PONER ESTO PARA UTILIZAR LA AUTENTICACION
            app.UseAuthentication();
            //
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
