using DProject.Imoveis.Back.Infra.IoC.Register;
using System.Text.Json.Serialization;

namespace DProject.Imoveis.Back.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection service)
        {



            service.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            service.AddSwaggerConfig(_configuration);
            service.AddInfra(_configuration);

            var env = "Development"; // Environment.GetEnvironmentVariable("Development");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), false)
                .AddJsonFile(Path.Combine(AppContext.BaseDirectory, $"appsettings.{env}.json"), false);

            IConfiguration builder = configuration.Build();
            service.AddSingleton<IConfiguration>(x => builder);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("./v1/swagger.json", "DImoveis");
                });

                app.UseDeveloperExceptionPage();

                app.UseHttpsRedirection();

                //app.UseAuthorization();

                app.UseRouting();

                app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

            }
            catch (Exception ex)
            {

                throw;
            }


        }

    }
}
