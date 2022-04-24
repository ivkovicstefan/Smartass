using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smartass.Group.BLL;
using Smartass.Group.BLL.Contract;
using Smartass.Group.DAL;
using Smartass.Group.DAL.Contract;

namespace Smartass.Group.API
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
            services.AddControllers();

            #region BLL Dependency Injection Registration
            services.AddScoped<IGroupLogic, GroupLogic>();
            services.AddScoped<IScriptLogic, ScriptLogic>();
            #endregion

            #region DAL Dependency Injection Registration
            services.AddScoped<IGroupDataAccess, GroupDataAccess>();
            services.AddScoped<IScriptDataAccess, ScriptDataAccess>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
