using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities
{
    public partial class Veiculo : IDomainEntity
    {
        /// <summary>
        /// Retorna a regra de validação a ser utilizada na inserção.
        /// </summary>
        public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
        {
            return x => ((Veiculo)x).Renavam.Equals(Renavam);
        }

        /// <summary>
        /// Retorna a regra de validação a ser utilizada na atualização.
        /// </summary>
        public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
        {
            return x => !((Veiculo)x).IdVeiculo.Equals(IdVeiculo) &&
                        ((Veiculo)x).Renavam.Equals(Renavam);
        }

        public Guid IdVeiculo { get; set; }
        public required string Marca { get; set; }
        public required string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public required string Placa { get; set; }
        public required string Renavam { get; set; }
        public decimal Preco { get; set; }
        public required string Status { get; set; }

        public virtual ICollection<VeiculoFoto> Fotos { get; set; } = new List<VeiculoFoto>();
        public virtual ICollection<VeiculoPagamento> Pagamentos { get; set; } = new List<VeiculoPagamento>();
    }
}