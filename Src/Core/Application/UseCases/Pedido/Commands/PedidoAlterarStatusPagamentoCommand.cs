using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Pedido.Commands
{
    public class PedidoAlterarStatusPagamentoCommand : IRequest<ModelResult>
    {
        public PedidoAlterarStatusPagamentoCommand(Guid id,
            enmPedidoStatusPagamento statusPagamento,
            string microServicoProducaoBaseAdress)
        {
            Id = id;
            StatusPagamento = statusPagamento;
            MicroServicoProducaoBaseAdress = microServicoProducaoBaseAdress;
        }

        public Guid Id { get; private set; }
        public enmPedidoStatusPagamento StatusPagamento { get; private set; }
        public string MicroServicoProducaoBaseAdress { get; private set; }
    }
}
