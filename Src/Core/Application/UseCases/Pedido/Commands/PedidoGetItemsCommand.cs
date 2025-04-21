using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Pedido.Commands
{
    public class PedidoGetItemsCommand : IRequest<PagingQueryResult<Domain.Entities.Pedido>>
    {
        public PedidoGetItemsCommand(IPagingQueryParam filter, Expression<Func<Domain.Entities.Pedido, object>> sortProp)
        {
            Filter = filter;
            SortProp = sortProp;
        }

        public PedidoGetItemsCommand(IPagingQueryParam filter,
            Expression<Func<Domain.Entities.Pedido, bool>> expression, Expression<Func<Domain.Entities.Pedido, object>> sortProp)
            : this(filter, sortProp)
        {
            Expression = expression;
        }

        public IPagingQueryParam Filter { get; }
        public Expression<Func<Domain.Entities.Pedido, bool>> Expression { get; }

        public Expression<Func<Domain.Entities.Pedido, object>> SortProp { get; }
    }
}