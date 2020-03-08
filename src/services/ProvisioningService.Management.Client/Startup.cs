using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProvisioningService.Management.Client.Business;
using ProvisioningService.Management.Client.Business.Implements;
using ProvisioningService.Management.Client.Infrastructure.Data;
using ProvisioningService.Management.Client.Repositories;
using ProvisioningService.Management.Client.Repositories.Implements;

namespace ProvisioningService.Management.Client
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
            services.AddDbContext<DeviceDbContext>(
                        options =>
                        {
                            options.UseSqlServer("Server=185.136.235.119;Database=DEVICES;User Id=sa;Password=xxQb6FVes;MultipleActiveResultSets=true;");
                        });

            services.AddControllers();

            services.AddScoped<IIotEdgeProvisioningBusiness, IotEdgeProvisioningBusiness>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IDeviceBusiness, DeviceBusiness>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod());
            });
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
            app.UseCors("AllowAll");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
