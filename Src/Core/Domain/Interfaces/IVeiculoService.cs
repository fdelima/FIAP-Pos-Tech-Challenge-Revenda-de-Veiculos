using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces
{
    public interface IVeiculoService : IService<VeiculoEntity>
    {
        /// <summary>
        /// Listagem de veículos à venda, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        ValueTask<PagingQueryResult<VeiculoEntity>> GetVehiclesForSaleAsync(IPagingQueryParam filter);

        /// <summary>
        /// Listagem de veículos vendidos, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        ValueTask<PagingQueryResult<VeiculoEntity>> GetVehiclesSoldAsync(IPagingQueryParam filter);
    }
}
