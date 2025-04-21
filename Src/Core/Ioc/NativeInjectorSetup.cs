using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.IoC;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.IoC
{
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]
    /// <summary>
    /// Configura a injeção de dependência
    /// </summary>
    public static class NativeInjectorSetup
    {
        /// <summary>
        /// Registra as dependências aos serviços da aplicação
        /// </summary>
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterInfraDependencies(configuration);
            services.RegisterAppDependencies();
        }
    }
}
