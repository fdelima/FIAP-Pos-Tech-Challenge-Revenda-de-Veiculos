using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands
{
    public class VeiculoPostCommand : IRequest<ModelResult<VeiculoEntity>>
    {
        public VeiculoPostCommand(VeiculoEntity entity,
            string[]? businessRules = null)
        {
            Entity = entity;
            BusinessRules = businessRules;
        }

        public VeiculoEntity Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}