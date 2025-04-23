using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands
{
    public class VeiculoPostCommand : IRequest<ModelResult>
    {
        public VeiculoPostCommand(Domain.Entities.Veiculo entity,
            string microServicoCadastroBaseAdress,
            string microServicoPagamentoBaseAdress,
            string[]? businessRules = null)
        {
            Entity = entity;
            MicroServicoCadastroBaseAdress = microServicoCadastroBaseAdress;
            MicroServicoPagamentoBaseAdress = microServicoPagamentoBaseAdress;
            BusinessRules = businessRules;
        }

        public Domain.Entities.Veiculo Entity { get; private set; }
        public string MicroServicoCadastroBaseAdress { get; private set; }
        public string MicroServicoPagamentoBaseAdress { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}