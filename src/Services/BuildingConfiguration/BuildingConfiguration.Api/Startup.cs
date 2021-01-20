using System.Threading.Tasks;
using BuildingConfiguration.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NodaTime;
using RabbitMQ.Client;

namespace BuildingConfiguration.Api
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
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.FullName);
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BuildingConfiguration.Api", Version = "v1" });
            });

            services.SetupInfrastructure(Configuration.GetConnectionString("MongoConnectionString"));

            services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddSingleton<IClock>(_ => SystemClock.Instance);
            services.AddMediatR(new[] { typeof(Startup).Assembly, typeof(Infrastructure.ServiceCollectionExtensions).Assembly });

            services.AddSingleton(_ => new ConnectionFactory() { HostName = Configuration.GetSection("QueueHostname").Get<string>() }.CreateConnection());
            services.AddSingleton(provider => provider.GetRequiredService<IConnection>().CreateModel());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BuildingConfiguration.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger");
                    return Task.CompletedTask;
                });

                endpoints.MapControllers();
            });
        }
    }
}
