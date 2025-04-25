using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Handlers
{
    public class VeiculoGetVehiclesForSaleHandler : IRequestHandler<VeiculoGetVehiclesForSaleCommand, PagingQueryResult<Domain.Entities.Veiculo>>
    {
        private readonly IVeiculoService _service;

        public VeiculoGetVehiclesForSaleHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Domain.Entities.Veiculo>> Handle(VeiculoGetVehiclesForSaleCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.GetVehiclesForSaleAsync(command.Filter);
        }
    }
}
