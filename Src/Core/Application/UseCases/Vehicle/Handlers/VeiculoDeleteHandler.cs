using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Handlers
{
    public class VeiculoDeleteHandler : IRequestHandler<VeiculoDeleteCommand, ModelResult<Veiculo>>
    {
        private readonly IVeiculoService _service;

        public VeiculoDeleteHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<ModelResult<Veiculo>> Handle(VeiculoDeleteCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.DeleteAsync(command.Id, command.BusinessRules);
        }
    }
}
