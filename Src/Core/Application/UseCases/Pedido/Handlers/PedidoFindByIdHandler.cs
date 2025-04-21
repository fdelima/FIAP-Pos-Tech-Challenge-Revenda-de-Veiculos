using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Pedido.Handlers
{
    public class PedidoFindByIdHandler : IRequestHandler<PedidoFindByIdCommand, ModelResult>
    {
        private readonly IPedidoService _service;

        public PedidoFindByIdHandler(IPedidoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoFindByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FindByIdAsync(command.Id);
        }
    }
}
