using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands
{
    public class VeiculoPostCommand : IRequest<ModelResult<Veiculo>>
    {
        public VeiculoPostCommand(Veiculo entity,
            string microServicoCadastroBaseAdress,
            string microServicoPagamentoBaseAdress,
            string[]? businessRules = null)
        {
            Entity = entity;
            MicroServicoCadastroBaseAdress = microServicoCadastroBaseAdress;
            MicroServicoPagamentoBaseAdress = microServicoPagamentoBaseAdress;
            BusinessRules = businessRules;
        }

        public Veiculo Entity { get; private set; }
        public string MicroServicoCadastroBaseAdress { get; private set; }
        public string MicroServicoPagamentoBaseAdress { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}