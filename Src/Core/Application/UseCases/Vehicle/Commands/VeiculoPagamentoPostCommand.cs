using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands
{
    public class VeiculoPagamentoPostCommand : IRequest<ModelResult<Veiculo>>
    {
        public VeiculoPagamentoPostCommand(Veiculo veiculo, VeiculoPagamento pagamento)
        {
            Veiculo = veiculo;
            Pagamento = pagamento;
        }

        public Veiculo Veiculo { get; private set; }
        public VeiculoPagamento Pagamento { get; private set; }
    }
}
