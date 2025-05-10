using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands
{
    public class VeiculoPagamentoPostCommand : IRequest<ModelResult<VeiculoEntity>>
    {
        public VeiculoPagamentoPostCommand(VeiculoEntity veiculo, VeiculoPagamentoEntity pagamento)
        {
            Veiculo = veiculo;
            Pagamento = pagamento;
        }

        public VeiculoEntity Veiculo { get; private set; }
        public VeiculoPagamentoEntity Pagamento { get; private set; }
    }
}
