using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces
{
    public interface IVeiculoController : IController<Entities.Veiculo>
    {
        /// <summary>
        /// Alterar o status de pagamento do veículo.
        /// </summary>
        Task<ModelResult> ChangePaymentStatus(Guid id, enmVeiculoStatusPagamento statusPagamento);

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
