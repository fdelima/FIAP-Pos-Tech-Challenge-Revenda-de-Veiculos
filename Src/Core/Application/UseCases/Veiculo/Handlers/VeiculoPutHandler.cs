using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Handlers
{
    public class VeiculoPutHandler : IRequestHandler<VeiculoPutCommand, ModelResult<Domain.Entities.Veiculo>>
    {
        private readonly IVeiculoService _service;

        public VeiculoPutHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<ModelResult<Domain.Entities.Veiculo>> Handle(VeiculoPutCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.UpdateAsync(command.Entity, command.BusinessRules);
        }
    }
}
