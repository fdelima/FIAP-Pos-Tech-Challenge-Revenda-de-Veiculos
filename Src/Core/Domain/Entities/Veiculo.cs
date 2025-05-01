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
        public string Thumb { get; set; } = "data:image/png;base64,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";
        public required string Status { get; set; }

        public virtual ICollection<VeiculoFoto> Fotos { get; set; } = new List<VeiculoFoto>();
        public virtual ICollection<VeiculoPagamento> Pagamentos { get; set; } = new List<VeiculoPagamento>();
    }
}