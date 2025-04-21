using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Pedido.Commands
{
    public class PedidoPostCommand : IRequest<ModelResult>
    {
        public PedidoPostCommand(Domain.Entities.Pedido entity,
            string microServicoCadastroBaseAdress,
            string microServicoPagamentoBaseAdress,
            string[]? businessRules = null)
        {
            Entity = entity;
            MicroServicoCadastroBaseAdress = microServicoCadastroBaseAdress;
            MicroServicoPagamentoBaseAdress = microServicoPagamentoBaseAdress;
            BusinessRules = businessRules;
        }

        public Domain.Entities.Pedido Entity { get; private set; }
        public string MicroServicoCadastroBaseAdress { get; private set; }
        public string MicroServicoPagamentoBaseAdress { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}