using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;

public partial class VeiculoFoto : IDomainEntity
{
    /// <summary>
    /// Retorna a regra de validação a ser utilizada na inserção.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
    {
        return x => ((VeiculoFoto)x).IdVeiculo.Equals(IdVeiculo) &&
                    ((VeiculoFoto)x).Imagem.Equals(Imagem);
    }

    /// <summary>
    /// Retorna a regra de validação a ser utilizada na atualização.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
    {
        return x => !((VeiculoFoto)x).IdVeiculoFoto.Equals(IdVeiculoFoto) &&
                    ((VeiculoFoto)x).IdVeiculo.Equals(IdVeiculo) &&
                    ((VeiculoFoto)x).Imagem.Equals(Imagem);
    }

    public Guid IdVeiculoFoto { get; set; }
    public Guid IdVeiculo { get; set; }
    public required string Imagem { get; set; }

    [JsonIgnore]
    public virtual Veiculo Veiculo { get; set; } = null!;

}
