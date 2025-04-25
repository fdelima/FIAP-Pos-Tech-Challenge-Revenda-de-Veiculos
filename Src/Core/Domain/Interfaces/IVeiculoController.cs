namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces
{
    public interface IVeiculoController : IController<Entities.Veiculo>
    {
        /// <summary>
        /// Listagem de veículos à venda, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        Task<PagingQueryResult<Entities.Veiculo>> GetVehiclesForSaleAsync(PagingQueryParam<Entities.Veiculo> param);

        /// <summary>
        /// Listagem de veículos vendidos, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        Task<PagingQueryResult<Entities.Veiculo>> GetVehiclesSoldAsync(PagingQueryParam<Entities.Veiculo> param);
    }
}
