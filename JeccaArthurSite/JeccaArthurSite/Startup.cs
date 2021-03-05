using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Winterfell.Models;
using System.Runtime.InteropServices;
using Winterfell.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;

namespace Winterfell
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
            // added for lab 6 - security checks
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://maxcdn.bootstrapcdn.com", "https://localhost:5001")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });


            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            // add service for DbContext with SQLite - this is dependency injection
            // add if statement to support azure db
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                services.AddDbContext<MessageContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:AzureSQLServerConnection"]));
            }
            else
            {
                services.AddDbContext<MessageContext>(options => options.UseSqlite(Configuration["ConnectionStrings:SQLiteConnection"]));
            }

            // add identity service
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<MessageContext>().AddDefaultTokenProviders();

            // injects repository into any controller that has it specified in its constructor
            services.AddTransient<IMessageRepository, MessageRepository>(); // specify repository interface, then repository
            services.AddTransient<IReplyRepository, ReplyRepository>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MessageContext context)
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

            // enable CORS
            app.UseCors();

            // set x-frame options - commented out for lab 9 azure db update
            //app.Use(async (context, next) =>
            //{
            //    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            //    await next();
            //});

            // vulnerability: loosely scoped cookies
            CookieOptions cookie = new CookieOptions
            {
                Domain = "https://winterfellja.azurewebsites.net/",
                Path = "/"   // this allows the cookie access to the root
            };

            // vulnerability: cookie without secure flag
            app.UseCookiePolicy(new CookiePolicyOptions { HttpOnly = HttpOnlyPolicy.Always, Secure = CookieSecurePolicy.Always });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            var serviceProvider = app.ApplicationServices;
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            SeedData.Seed(context, roleManager, userManager);
        }
    }
}
