using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands
{
    public class VeiculoPutCommand : IRequest<ModelResult<Domain.Entities.Veiculo>>
    {
        public VeiculoPutCommand(Guid id, Domain.Entities.Veiculo entity,
            string[]? businessRules = null)
        {
            Id = id;
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Guid Id { get; private set; }
        public Domain.Entities.Veiculo Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}