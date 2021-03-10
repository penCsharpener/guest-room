using GuestRoom.Api.Extensions;
using GuestRoom.Api.Models.Configuration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace GuestRoom.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private const string CorsPolicy = "CorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appsettings = Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

            services.AddControllers();
            services.AddEfCore(appsettings, Configuration.GetConnectionString("SqliteConnection"));
            services.AddGuestRoomServices(appsettings);
            services.AddMediatR(GetType().Assembly);
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "GuestRoom.Api", Version = "v1" }); });
            services.AddCors(opt =>
            {
                opt.AddPolicy(CorsPolicy, policy =>
                {
                    policy
#if DEBUG
                    .AllowAnyOrigin()
#else
                    .WithOrigins("https://localhost:4200")
#endif
                    .AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GuestRoom.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CorsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}