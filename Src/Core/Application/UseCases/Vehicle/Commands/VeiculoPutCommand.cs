using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands
{
    public class VeiculoPutCommand : IRequest<ModelResult<VeiculoEntity>>
    {
        public VeiculoPutCommand(Guid id, VeiculoEntity entity,
            string[]? businessRules = null)
        {
            Id = id;
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Guid Id { get; private set; }
        public VeiculoEntity Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}