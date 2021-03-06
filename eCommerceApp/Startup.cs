using eCommerceApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApp.Repos;

namespace eCommerceApp
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
            services.AddDbContext<eCommerceAppEntities>(builder => {
                builder.UseSqlServer(Configuration.GetConnectionString("DB"));
            });
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<eCommerceAppEntities>();
            services.AddScoped<IProductRepository, ProductRepository>(); 
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IShoppingCart, ShoppingCart>();
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
               //Customed Catgegories Routes
                endpoints.MapControllerRoute(
                    name: "computer",
                    pattern: "computer",
                    defaults: new { controller = "Category", action = "GetOne", id = 1 });
                endpoints.MapControllerRoute(
                    name: "laptop",
                    pattern: "laptop",
                    defaults: new { controller = "Category", action = "GetOne", id = 2 });
                endpoints.MapControllerRoute(
                    name: "mobile",
                    pattern: "mobile",
                    defaults: new { controller = "Category", action = "GetOne", id = 3 });
                endpoints.MapControllerRoute(
                    name: "all-categories",
                    pattern: "all-categories",
                    defaults: new { controller = "product", action = "Index" });
                //register in Route
                endpoints.MapControllerRoute(
                   name: "signin",
                   pattern: "signin",
                   defaults: new { controller = "Account", action = "Login" });
                endpoints.MapControllerRoute(
                   name: "signup",
                   pattern: "signup",
                   defaults: new { controller = "Account", action = "Register" });
                //Default Handler
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    } 
}
