using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Handlers;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
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
            services.AddScoped<IRequestHandler<VeiculoPostCommand, ModelResult<Veiculo>>, VeiculoPostHandler>();
            services.AddScoped<IRequestHandler<VeiculoPagamentoPostCommand, ModelResult<Veiculo>>, VeiculoPagamentoPostHandler>();
            services.AddScoped<IRequestHandler<VeiculoPutCommand, ModelResult<Veiculo>>, VeiculoPutHandler>();
            services.AddScoped<IRequestHandler<VeiculoDeleteCommand, ModelResult<Veiculo>>, VeiculoDeleteHandler>();
            services.AddScoped<IRequestHandler<VeiculoFindByIdCommand, ModelResult<Veiculo>>, VeiculoFindByIdHandler>();
            services.AddScoped<IRequestHandler<VeiculoGetItemsCommand, PagingQueryResult<Veiculo>>, VeiculoGetItemsHandler>();
            services.AddScoped<IRequestHandler<VeiculoGetVehiclesForSaleCommand, PagingQueryResult<Veiculo>>, VeiculoGetVehiclesForSaleHandler>();

        }
    }
}
