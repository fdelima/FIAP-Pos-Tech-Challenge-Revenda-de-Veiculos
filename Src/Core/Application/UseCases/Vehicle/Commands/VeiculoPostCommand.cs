using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands
{
    public class VeiculoPostCommand : IRequest<ModelResult<Veiculo>>
    {
        public VeiculoPostCommand(Veiculo entity,
            string[]? businessRules = null)
        {
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Veiculo Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}