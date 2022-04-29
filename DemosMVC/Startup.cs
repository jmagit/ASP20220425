using DemosMVC.Data;
using Domains.Contracts.DomainsServices;
using Domains.Contracts.Repositories;
using Domains.Services;
using Infraestructure.Repositories;
using Infraestructure.UoW;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemosMVC {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<TiendaDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("TiendaConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            services.AddSingleton<IValidationAttributeAdapterProvider, CustomValidationAttributeAdapterProvider>();
            //#if DEBUG
            //            services.AddTransient<IProductoRepository, ProductoRepositoryMock>();
            //#else
            services.AddTransient<IProductoRepository, ProductoRepository>();
            //#endif
            services.AddTransient<IProductoService, ProductoService>();
            services.AddRazorPages();
            services.AddControllers().AddXmlSerializerFormatters();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "productos",
                    pattern: "productos/{num:int:min(0)}/{size:int:min(5)?}",
                    defaults: new { controller = "Products", action = "Index" }
                    );
                endpoints.MapControllerRoute(
                    name: "informes",
                    pattern: "informes/trimestrales/{año:int:min(0)}/{trimestre:int:min(5)}",
                    defaults: new { controller = "Products", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });

            var positionOptions = new PositionOptions();
            Configuration.GetSection(PositionOptions.Position).Bind(positionOptions);
            string userName = Configuration.GetSection("Position")["Name"];


        }

    }
    public class PositionOptions {
        public const string Position = "Position";
        public string Title { get; set; }
        public string Name { get; set; }
    }
}
