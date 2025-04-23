using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands
{
    public class VeiculoGetItemsCommand : IRequest<PagingQueryResult<Domain.Entities.Veiculo>>
    {
        public VeiculoGetItemsCommand(IPagingQueryParam filter, Expression<Func<Domain.Entities.Veiculo, object>> sortProp)
        {
            Filter = filter;
            SortProp = sortProp;
        }

        public VeiculoGetItemsCommand(IPagingQueryParam filter,
            Expression<Func<Domain.Entities.Veiculo, bool>> expression, Expression<Func<Domain.Entities.Veiculo, object>> sortProp)
            : this(filter, sortProp)
        {
            Expression = expression;
        }

        public IPagingQueryParam Filter { get; }
        public Expression<Func<Domain.Entities.Veiculo, bool>> Expression { get; }

        public Expression<Func<Domain.Entities.Veiculo, object>> SortProp { get; }
    }
}