using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces
{
    public interface IVeiculoController : IController<VeiculoEntity>
    {
        /// <summary>
        /// Consulta os veículos cadastrados no sistema.
        /// </summary>
        Task<PagingQueryResult<VeiculoModel>> ConsultListItemsAsync(PagingQueryParam<VeiculoModel> param);

        /// <summary>
        /// Retorna os veículos cadastrados no sistema paginado.
        /// </summary>
        Task<PagingQueryResult<VeiculoModel>> GetListItemsAsync(int currentPage, int take);

        /// <summary>
        /// Listagem de veículos à venda, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        Task<PagingQueryResult<VeiculoModel>> GetVehiclesForSaleAsync(PagingQueryParam<VeiculoEntity> filter);

        /// <summary>
        /// Listagem de veículos vendidos, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        Task<PagingQueryResult<VeiculoModel>> GetVehiclesSoldAsync(PagingQueryParam<VeiculoEntity> filter);

        /// <summary>
        /// Cadastra um novo pagamento para um Veiculo.
        /// </summary>
        Task<ModelResult<VeiculoPagamentoEntity>> PostPagamentoAsync(VeiculoPagamentoEntity entity);
    }
}
