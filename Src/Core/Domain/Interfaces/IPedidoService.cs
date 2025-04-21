namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces
{
    public interface IPedidoService : IService<Entities.Pedido>
    {
        /// <summary>
        /// Retorna os Pedidos cadastrados
        /// A lista de pedidos deverá retorná-los com suas descrições, ordenados com a seguinte regra:
        /// 1. Pronto > Em Preparação > Recebido;
        /// 2. Pedidos mais antigos primeiro e mais novos depois;
        /// 3. Pedidos com status Finalizado não devem aparecer na lista.
        /// </summary>
        ValueTask<PagingQueryResult<Entities.Pedido>> GetListaAsync(IPagingQueryParam filter);
    }
}
