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
            services.AddScoped<IRequestHandler<VeiculoPostCommand, ModelResult<VeiculoEntity>>, VeiculoPostHandler>();
            services.AddScoped<IRequestHandler<VeiculoPagamentoPostCommand, ModelResult<VeiculoEntity>>, VeiculoPagamentoPostHandler>();
            services.AddScoped<IRequestHandler<VeiculoPutCommand, ModelResult<VeiculoEntity>>, VeiculoPutHandler>();
            services.AddScoped<IRequestHandler<VeiculoDeleteCommand, ModelResult<VeiculoEntity>>, VeiculoDeleteHandler>();
            services.AddScoped<IRequestHandler<VeiculoFindByIdCommand, ModelResult<VeiculoEntity>>, VeiculoFindByIdHandler>();
            services.AddScoped<IRequestHandler<VeiculoGetItemsCommand, PagingQueryResult<VeiculoEntity>>, VeiculoGetItemsHandler>();
            services.AddScoped<IRequestHandler<VeiculoGetVehiclesForSaleCommand, PagingQueryResult<VeiculoEntity>>, VeiculoGetVehiclesForSaleHandler>();

        }
    }
}
