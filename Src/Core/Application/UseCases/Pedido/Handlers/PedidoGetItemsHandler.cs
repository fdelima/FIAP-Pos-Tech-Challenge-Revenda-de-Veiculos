using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Handlers
{
    public class VeiculoGetItemsHandler : IRequestHandler<VeiculoGetItemsCommand, PagingQueryResult<Domain.Entities.Veiculo>>
    {
        private readonly IVeiculoService _service;

        public VeiculoGetItemsHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Domain.Entities.Veiculo>> Handle(VeiculoGetItemsCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Expression == null)
                return await _service.GetItemsAsync(command.Filter, command.SortProp);
            else
                return await _service.GetItemsAsync(command.Filter, command.Expression, command.SortProp);
        }
    }
}
