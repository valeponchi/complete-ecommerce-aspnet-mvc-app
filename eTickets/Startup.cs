using eTickets.Data;
using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

namespace eTickets
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; } //value
        
        
        //_______________________________________________________________________
        //CONFIGURE SERVICES

        //Method called by the runtime. Use it to add services to the default-building-container in the .NET framework
        public void ConfigureServices(IServiceCollection services)
        {
            //The TRANSLATOR *DbContext* configuration
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"))); //as param you pass the DataStorage/the language/the connection string

            //SERVICES configuration
                //ACTOR SERVICE
                //scoped lifetime / param(interface to inject in ctor, implementation of this interface)
            services.AddScoped<IActorsService, ActorsService>();
            services.AddScoped<IProducersService, ProducersService>();
            services.AddScoped<ICinemasService, CinemasService>();
            services.AddScoped<IMoviesService, MoviesService>();
            services.AddScoped<IOrdersService, OrdersService>();

            //CONFIG SHOPPING CART
                //config Http context first
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                //sc = shopping cart
            services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

            //configure identity
            //AUTHENTICATION AND AUTHORIZATION
            //param1 = identityuser, param2 = identityrole, then when store all the authentication related data and which file you are using
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            //we add the memory-cache
            services.AddMemoryCache();
            
            services.AddSession();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //you can define custom prop for the cookie
                //you can say the pw needs to be min 10chars, 1uppercase, 1specialChar etc
            });

            //controller config - already in here
            services.AddControllersWithViews();
        }

        //_______________________________________________________________________
        //CONFIGURE

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
            app.UseSession();

            //authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //Seed database
            //if you look at this Configure Arg, you find that "app" is IApplicationBuilder
            AppDbInitializer.Seed(app);
            AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
        }
    }
}

