using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DProject.Imoveis.Back.Infra.IoC.Register
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfig(this IServiceCollection services, IConfiguration configuration)
        {

            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }


            services.AddCors();

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileInfo fileInfo = new FileInfo(assembly.Location);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "Data Versão - " + fileInfo.LastWriteTime.ToString(),
                    Title = "Gestão Pix Recebimento",
                    Description = "Api de Gestão Pix Recebimento",

                });

                c.ResolveConflictingActions(api => api.First());
            });







        }
    }
}
