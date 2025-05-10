using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Extensions
{
    /// <summary>
    /// Extensão da model para informar os campos de validação.
    /// </summary>
    public static class VeiculoExtension
    {
        /// <summary>
        /// Retorna a regra de validação a ser utilizada na atualização.
        /// </summary>
        public static Expression<Func<VeiculoEntity, bool>> ConsultRule(this PagingQueryParam<VeiculoEntity> param)
        {
            return x => (x.IdVeiculo.Equals(param.ObjFilter.IdVeiculo) || param.ObjFilter.IdVeiculo.Equals(default)) &&
                        (x.Marca.Equals(param.ObjFilter.Marca) || string.IsNullOrWhiteSpace(param.ObjFilter.Marca)) &&
                        (x.Modelo.Equals(param.ObjFilter.Modelo) || string.IsNullOrWhiteSpace(param.ObjFilter.Modelo)) &&
                        (x.AnoFabricacao.Equals(param.ObjFilter.AnoFabricacao) || param.ObjFilter.AnoFabricacao.Equals(default)) &&
                        (x.AnoModelo.Equals(param.ObjFilter.AnoModelo) || param.ObjFilter.AnoModelo.Equals(default)) &&
                        (x.Placa.Equals(param.ObjFilter.Placa) || string.IsNullOrWhiteSpace(param.ObjFilter.Placa)) &&
                        (x.Renavam.Equals(param.ObjFilter.Renavam) || string.IsNullOrWhiteSpace(param.ObjFilter.Renavam)) &&
                        (x.Preco.Equals(param.ObjFilter.Preco) || param.ObjFilter.Preco.Equals(default)) &&
                        (x.Status.Equals(param.ObjFilter.Status) || string.IsNullOrWhiteSpace(param.ObjFilter.Status));
        }

        /// <summary>
        /// Retorna a propriedade a ser ordenada
        /// </summary>
        public static Expression<Func<VeiculoEntity, object>> SortProp(this PagingQueryParam<VeiculoEntity> param)
        {
            switch (param?.SortProperty?.ToLower())
            {
                case "idveiculo":
                    return fa => fa.IdVeiculo;
                case "marca":
                    return fa => fa.Marca;
                case "modelo":
                    return fa => fa.Modelo;
                case "anofabricacao":
                    return fa => fa.AnoFabricacao;
                case "anomodelo":
                    return fa => fa.AnoModelo;
                case "placa":
                    return fa => fa.Placa;
                case "renavam":
                    return fa => fa.Renavam;
                case "preco":
                    return fa => fa.Preco;
                case "status":
                    return fa => fa.Status;
                default: return fa => fa.AnoModelo;
            }
        }
    }
}
