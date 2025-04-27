using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Handlers
{
    public class VeiculoGetItemsHandler : IRequestHandler<VeiculoGetItemsCommand, PagingQueryResult<Veiculo>>
    {
        private readonly IVeiculoService _service;

        public VeiculoGetItemsHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Veiculo>> Handle(VeiculoGetItemsCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Expression == null)
                return await _service.GetItemsAsync(command.Filter, command.SortProp);
            else
                return await _service.GetItemsAsync(command.Filter, command.Expression, command.SortProp);
        }
    }
}
