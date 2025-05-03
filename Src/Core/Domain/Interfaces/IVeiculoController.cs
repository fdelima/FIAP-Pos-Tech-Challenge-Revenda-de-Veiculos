using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces
{
    public interface IVeiculoController : IController<Veiculo>
    {
        /// <summary>
        /// Listagem de veículos à venda, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        Task<PagingQueryResult<Veiculo>> GetVehiclesForSaleAsync(PagingQueryParam<Veiculo> filter);

        /// <summary>
        /// Listagem de veículos vendidos, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        Task<PagingQueryResult<Veiculo>> GetVehiclesSoldAsync(PagingQueryParam<Veiculo> filter);

        /// <summary>
        /// Cadastra um novo pagamento para um Veiculo.
        /// </summary>
        Task<ModelResult<VeiculoPagamento>> PostPagamentoAsync(VeiculoPagamento entity);
    }
}
