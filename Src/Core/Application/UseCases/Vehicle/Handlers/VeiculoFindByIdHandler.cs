using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Handlers
{
    public class VeiculoFindByIdHandler : IRequestHandler<VeiculoFindByIdCommand, ModelResult<Veiculo>>
    {
        private readonly IVeiculoService _service;

        public VeiculoFindByIdHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<ModelResult<Veiculo>> Handle(VeiculoFindByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FindByIdAsync(command.Id);
        }
    }
}
