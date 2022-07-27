using DProject.Imoveis.Back.Services.Interfaces;
using DProject.Imoveis.Back.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DProject.Imoveis.Back.Infra.IoC.Register
{
    public static class DependencyInjection
    {
        public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            AddIOC_ImoveisWeb(services);
            AddIOC_ZapImoveis(services);
        }

        private static IServiceCollection AddIOC_ImoveisWeb(IServiceCollection services)
        {
            return services.AddSingleton<IImovelWebService, ImovelWebService>();
        }

        private static IServiceCollection AddIOC_ZapImoveis(IServiceCollection services)
        {
            return services.AddSingleton<IZapImoveisService, ZapImoveisService>();
        }
    }
}
