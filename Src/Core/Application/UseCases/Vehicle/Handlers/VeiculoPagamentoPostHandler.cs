using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Handlers
{
    public class VeiculoPagamentoPostHandler : IRequestHandler<VeiculoPagamentoPostCommand, ModelResult<VeiculoEntity>>
    {
        private readonly IVeiculoService _service;

        public VeiculoPagamentoPostHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<ModelResult<VeiculoEntity>> Handle(VeiculoPagamentoPostCommand command, CancellationToken cancellationToken = default)
        {
            command.Veiculo.Status = enmVeiculoStatus.VENDIDO.ToString();
            command.Veiculo.Pagamentos.Add(command.Pagamento);
            return await _service.UpdateAsync(command.Veiculo);
        }
    }
}
