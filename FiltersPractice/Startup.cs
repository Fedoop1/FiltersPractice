using FiltersPractice.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FiltersPractice
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDiagnosticFilter, SimpleDiagnosticFilter>();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(SimpleResourceFilterWithDI));
                options.Filters.Add(new SimpleResourceFilterWithParams(123, "test"));
                //options.Filters.Add(typeof(DiagnosticFilter));
                //options.Filters.Add(typeof(TimerFilter));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseStatusCodePages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
