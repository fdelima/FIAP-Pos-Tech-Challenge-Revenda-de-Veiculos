using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands
{
    public class VeiculoGetVehiclesForSaleCommand : IRequest<PagingQueryResult<Veiculo>>
    {
        public VeiculoGetVehiclesForSaleCommand(IPagingQueryParam filter)
        {
            Filter = filter;
        }

        public IPagingQueryParam Filter { get; }
    }
}