using AutoMapper;
using Entity.Service.Business;
using Entity.Service.Business.Implements;
using Entity.Service.Infrastructure.Data;
using Entity.Service.Repositories;
using Entity.Service.Repositories.Implements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Entity.Service
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
            services.AddDbContext<EntityDbContext>(
                options =>
                {
                    options.UseSqlServer("Server=185.136.235.119;Database=ENTITIES;User Id=sa;Password=xxQb6FVes;MultipleActiveResultSets=true;");
                });

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IEntityRepository, EntityRepository>();
            services.AddScoped<IEntityBusiness, EntityBusiness>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod());
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://wetry.eu.auth0.com";
                options.Audience = "http://easypark.tech";
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
