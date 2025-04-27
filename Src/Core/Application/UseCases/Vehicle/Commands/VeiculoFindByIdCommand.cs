using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands
{
    public class VeiculoFindByIdCommand : IRequest<ModelResult<Veiculo>>
    {
        public VeiculoFindByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}