using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Handlers
{
    public class VeiculoDeleteHandler : IRequestHandler<VeiculoDeleteCommand, ModelResult<Domain.Entities.Veiculo>>
    {
        private readonly IVeiculoService _service;

        public VeiculoDeleteHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<ModelResult<Domain.Entities.Veiculo>> Handle(VeiculoDeleteCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.DeleteAsync(command.Id, command.BusinessRules);
        }
    }
}
