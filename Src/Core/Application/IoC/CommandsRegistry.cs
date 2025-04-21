using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Pedido.Handlers;
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

            //Pedido
            services.AddScoped<IRequestHandler<PedidoPostCommand, ModelResult>, PedidoPostHandler>();
            services.AddScoped<IRequestHandler<PedidoAlterarStatusPagamentoCommand, ModelResult>, PedidoAlterarStatusPagamentoHandler>();
            services.AddScoped<IRequestHandler<PedidoPutCommand, ModelResult>, PedidoPutHandler>();
            services.AddScoped<IRequestHandler<PedidoDeleteCommand, ModelResult>, PedidoDeleteHandler>();
            services.AddScoped<IRequestHandler<PedidoFindByIdCommand, ModelResult>, PedidoFindByIdHandler>();
            services.AddScoped<IRequestHandler<PedidoGetItemsCommand, PagingQueryResult<Domain.Entities.Pedido>>, PedidoGetItemsHandler>();
            services.AddScoped<IRequestHandler<PedidoGetListaCommand, PagingQueryResult<Domain.Entities.Pedido>>, PedidoGetIListaHandler>();

        }
    }
}
