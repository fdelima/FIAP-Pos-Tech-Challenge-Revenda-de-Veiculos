using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands
{
    public class VeiculoPagamentoPostCommand : IRequest<ModelResult<Domain.Entities.Veiculo>>
    {
        public VeiculoPagamentoPostCommand(Guid idVeiculo,
            DateTime data, decimal valorRecebido, string banco, string conta,
            string cpfCnpj)
        {
            IdVeiculo = idVeiculo;
            Data = data;
            ValorRecebido = valorRecebido;
            Banco = banco;
            Conta = conta;
            CpfCnpj = cpfCnpj;
        }

        public Guid IdVeiculo { get; private set; }
        public DateTime Data { get; private set; }
        public decimal ValorRecebido { get; private set; }
        public string Banco { get; private set; }
        public string Conta { get; private set; }
        public string CpfCnpj { get; private set; }
    }
}
