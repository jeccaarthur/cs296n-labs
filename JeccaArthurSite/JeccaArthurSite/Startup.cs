using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Winterfell.Models;
using System.Runtime.InteropServices;
using Winterfell.Repositories;

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
            services.AddControllersWithViews();
            services.AddRazorPages()
            .AddRazorRuntimeCompilation();

            // add service for DbContext with SQLite - this is dependency injection
            // services.AddDbContext<MessageContext>(options => options.UseSqlite(Configuration["ConnectionStrings:SQLiteConnection"]));

            // add if statement to support azure db
            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //{
                services.AddDbContext<MessageContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:AzureSQLServerConnection"]));
            //}
            /*
            else
            {
                services.AddDbContext<MessageContext>(options => options.UseSqlite(Configuration["ConnectionStrings:SQLiteConnection"]));
            }
            */

            // injects repository into any controller that has it specified in its constructor
            services.AddTransient<IMessages, MessagesRepository>(); // specify repository interface, then repository
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
