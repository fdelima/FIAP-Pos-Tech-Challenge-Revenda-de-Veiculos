using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Handlers
{
    public class VeiculoGetVehiclesForSaleHandler : IRequestHandler<VeiculoGetVehiclesForSaleCommand, PagingQueryResult<Veiculo>>
    {
        private readonly IVeiculoService _service;

        public VeiculoGetVehiclesForSaleHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Veiculo>> Handle(VeiculoGetVehiclesForSaleCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.GetVehiclesForSaleAsync(command.Filter);
        }
    }
}
