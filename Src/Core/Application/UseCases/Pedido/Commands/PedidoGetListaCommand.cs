using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands
{
    public class VeiculoGetListaCommand : IRequest<PagingQueryResult<Domain.Entities.Veiculo>>
    {
        public VeiculoGetListaCommand(IPagingQueryParam filter)
        {
            Filter = filter;
        }

        public IPagingQueryParam Filter { get; }
    }
}