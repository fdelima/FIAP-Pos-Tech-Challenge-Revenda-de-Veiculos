using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Pedido.Commands
{
    public class PedidoGetListaCommand : IRequest<PagingQueryResult<Domain.Entities.Pedido>>
    {
        public PedidoGetListaCommand(IPagingQueryParam filter)
        {
            Filter = filter;
        }

        public IPagingQueryParam Filter { get; }
    }
}