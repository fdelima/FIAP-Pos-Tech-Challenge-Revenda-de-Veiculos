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
            services.AddScoped(typeof(IValidator<VeiculoPagamentoEntity>), typeof(VeiculoPagamentoValidator));
            services.AddScoped(typeof(IValidator<VeiculoFotoEntity>), typeof(VeiculoFotoValidator));
            services.AddScoped(typeof(IValidator<VeiculoEntity>), typeof(VeiculoValidator));
        }
    }
}
