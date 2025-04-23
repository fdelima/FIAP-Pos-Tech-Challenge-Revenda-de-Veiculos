using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Handlers
{
    public class VeiculoFindByIdHandler : IRequestHandler<VeiculoFindByIdCommand, ModelResult>
    {
        private readonly IVeiculoService _service;

        public VeiculoFindByIdHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(VeiculoFindByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FindByIdAsync(command.Id);
        }
    }
}
