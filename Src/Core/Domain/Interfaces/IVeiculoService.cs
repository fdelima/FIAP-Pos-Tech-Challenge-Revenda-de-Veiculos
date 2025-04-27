using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces
{
    public interface IVeiculoService : IService<Veiculo>
    {
        /// <summary>
        /// Listagem de veículos à venda, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        ValueTask<PagingQueryResult<Veiculo>> GetVehiclesForSaleAsync(IPagingQueryParam filter);

        /// <summary>
        /// Listagem de veículos vendidos, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        ValueTask<PagingQueryResult<Veiculo>> GetVehiclesSoldAsync(IPagingQueryParam filter);
    }
}
