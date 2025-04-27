using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Handlers
{
    public class VeiculoGetVehiclesSoldHandler : IRequestHandler<VeiculoGetVehiclesSoldCommand, PagingQueryResult<Veiculo>>
    {
        private readonly IVeiculoService _service;

        public VeiculoGetVehiclesSoldHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Veiculo>> Handle(VeiculoGetVehiclesSoldCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.GetVehiclesSoldAsync(command.Filter);
        }
    }
}
