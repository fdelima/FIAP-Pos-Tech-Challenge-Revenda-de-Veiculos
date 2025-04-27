using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands
{
    public class VeiculoGetItemsCommand : IRequest<PagingQueryResult<Veiculo>>
    {
        public VeiculoGetItemsCommand(IPagingQueryParam filter,
            Expression<Func<Veiculo, object>> sortProp)
        {
            Filter = filter;
            SortProp = sortProp;
        }

        public VeiculoGetItemsCommand(IPagingQueryParam filter,
         Expression<Func<Veiculo, object>> sortProp,
         Expression<Func<Veiculo, bool>> expression)
            : this(filter, sortProp)
        {
            Expression = expression;
        }

        public IPagingQueryParam Filter { get; }
        public Expression<Func<Veiculo, bool>>? Expression { get; }
        public Expression<Func<Veiculo, object>> SortProp { get; }
    }
}