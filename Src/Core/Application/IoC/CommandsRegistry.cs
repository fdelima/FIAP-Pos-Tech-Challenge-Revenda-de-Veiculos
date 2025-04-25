using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Handlers;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.IoC
{
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]
    internal static class CommandsRegistry
    {
        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            //Veiculo
            services.AddScoped<IRequestHandler<VeiculoPostCommand, ModelResult>, VeiculoPostHandler>();
            services.AddScoped<IRequestHandler<VeiculoPagamentoPostCommand, ModelResult>, VeiculoPagamentoPostHandler>();
            services.AddScoped<IRequestHandler<VeiculoPutCommand, ModelResult>, VeiculoPutHandler>();
            services.AddScoped<IRequestHandler<VeiculoDeleteCommand, ModelResult>, VeiculoDeleteHandler>();
            services.AddScoped<IRequestHandler<VeiculoFindByIdCommand, ModelResult>, VeiculoFindByIdHandler>();
            services.AddScoped<IRequestHandler<VeiculoGetItemsCommand, PagingQueryResult<Domain.Entities.Veiculo>>, VeiculoGetItemsHandler>();
            services.AddScoped<IRequestHandler<VeiculoGetVehiclesForSaleCommand, PagingQueryResult<Domain.Entities.Veiculo>>, VeiculoGetVehiclesForSaleHandler>();

        }
    }
}
