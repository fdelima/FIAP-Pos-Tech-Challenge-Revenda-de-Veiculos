using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Pedido.Handlers
{
    public class PedidoGetIListaHandler : IRequestHandler<PedidoGetListaCommand, PagingQueryResult<Domain.Entities.Pedido>>
    {
        private readonly IPedidoService _service;

        public PedidoGetIListaHandler(IPedidoService service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Domain.Entities.Pedido>> Handle(PedidoGetListaCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.GetListaAsync(command.Filter);
        }
    }
}
