using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities
{
    public partial class VeiculoEntity : VeiculoModel, IDomainEntity
    {
        /// <summary>
        /// Retorna a regra de validação a ser utilizada na inserção.
        /// </summary>
        public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
        {
            return x => ((VeiculoEntity)x).Renavam.Equals(Renavam);
        }

        /// <summary>
        /// Retorna a regra de validação a ser utilizada na atualização.
        /// </summary>
        public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
        {
            return x => !((VeiculoEntity)x).IdVeiculo.Equals(IdVeiculo) &&
                        ((VeiculoEntity)x).Renavam.Equals(Renavam);
        }

        public virtual ICollection<VeiculoFotoEntity> Fotos { get; set; } = new List<VeiculoFotoEntity>();
        public virtual ICollection<VeiculoPagamentoEntity> Pagamentos { get; set; } = new List<VeiculoPagamentoEntity>();

    }
}