using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.IoC
{
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]
    internal static class ValidatorsRegistry
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            //TODO: Validators :: 3 - Adicione sua configuração aqui

            //Validators
            services.AddScoped(typeof(IValidator<Notificacao>), typeof(NotificacaoValidator));
            services.AddScoped(typeof(IValidator<VeiculoFoto>), typeof(VeiculoItemValidator));
            services.AddScoped(typeof(IValidator<Domain.Entities.Veiculo>), typeof(VeiculoValidator));
        }
    }
}
