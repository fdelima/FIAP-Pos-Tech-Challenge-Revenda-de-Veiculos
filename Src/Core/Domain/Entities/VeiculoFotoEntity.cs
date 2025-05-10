using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;

public partial class VeiculoFotoEntity : IDomainEntity
{
    /// <summary>
    /// Retorna a regra de validação a ser utilizada na inserção.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
    {
        return x => ((VeiculoFotoEntity)x).IdVeiculo.Equals(IdVeiculo) &&
                    ((VeiculoFotoEntity)x).Imagem.Equals(Imagem);
    }

    /// <summary>
    /// Retorna a regra de validação a ser utilizada na atualização.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
    {
        return x => !((VeiculoFotoEntity)x).IdVeiculoFoto.Equals(IdVeiculoFoto) &&
                    ((VeiculoFotoEntity)x).IdVeiculo.Equals(IdVeiculo) &&
                    ((VeiculoFotoEntity)x).Imagem.Equals(Imagem);
    }

    public Guid IdVeiculoFoto { get; set; }
    public Guid IdVeiculo { get; set; }
    public required string Imagem { get; set; }

    [JsonIgnore]
    public virtual VeiculoEntity Veiculo { get; set; } = null!;

}
