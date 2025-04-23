using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands
{
    public class VeiculoAlterarStatusPagamentoCommand : IRequest<ModelResult>
    {
        public VeiculoAlterarStatusPagamentoCommand(Guid id,
            enmVeiculoStatusPagamento statusPagamento,
            string microServicoProducaoBaseAdress)
        {
            Id = id;
            StatusPagamento = statusPagamento;
            MicroServicoProducaoBaseAdress = microServicoProducaoBaseAdress;
        }

        public Guid Id { get; private set; }
        public enmVeiculoStatusPagamento StatusPagamento { get; private set; }
        public string MicroServicoProducaoBaseAdress { get; private set; }
    }
}
