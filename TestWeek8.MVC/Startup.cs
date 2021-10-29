using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWeek8.Core;
using TestWeek8.Core.Interfaces;
using TestWeek8.EF;
using TestWeek8.EF.Repositories;

namespace TestWeek8.MVC
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

            services.AddTransient<IMainBusinessLayer, MainBusinessLayer>();
            services.AddTransient<IDishRepository, DishRepositoryEF>();
            services.AddTransient<IMenuRepository, MenuRepositoryEF>();
            services.AddTransient<IUserRepository, UserRepositoryEF>();

            services.AddDbContext<RestaurantContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("RestaurantDB"));
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(
                   options =>
                   {
                       options.LoginPath = new PathString("/User/Login");
                       options.AccessDeniedPath = new PathString("/User/Forbidden");
                   }
               );

            services.AddAuthorization(
                options =>
                {
                    options.AddPolicy("UtenteRistoratore", policy => policy.RequireRole("Ristoratore"));
                    options.AddPolicy("UtenteCliente", policy => policy.RequireRole("Cliente"));
                }
            );
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

            app.UseAuthentication();

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
