using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;

public partial class VeiculoPagamento : IDomainEntity
{
    /// <summary>
    /// Retorna a regra de validação a ser utilizada na inserção.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
    {
        return x => ((VeiculoPagamento)x).IdVeiculo.Equals(IdVeiculo) &&
                    ((VeiculoPagamento)x).ValorRecebido.Equals(ValorRecebido) &&
                    ((VeiculoPagamento)x).CpfCnpj.Equals(CpfCnpj);
    }

    /// <summary>
    /// Retorna a regra de validação a ser utilizada na atualização.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
    {
        return x => !((VeiculoPagamento)x).IdVeiculoPagamento.Equals(IdVeiculoPagamento) &&
                    ((VeiculoPagamento)x).IdVeiculo.Equals(IdVeiculo) &&
                    ((VeiculoPagamento)x).ValorRecebido.Equals(ValorRecebido) &&
                    ((VeiculoPagamento)x).CpfCnpj.Equals(CpfCnpj);
    }

    public Guid IdVeiculoPagamento { get; set; }
    public Guid IdVeiculo { get; set; }
    public DateTime Data { get; set; }
    public decimal ValorRecebido { get; set; }
    public required string Banco { get; set; }
    public required string Conta { get; set; }
    public required string CpfCnpj { get; set; }

    [JsonIgnore]
    public virtual Veiculo Veiculo { get; set; } = null!;

}
