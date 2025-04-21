using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;

public partial class PedidoItem : IDomainEntity
{
    /// <summary>
    /// Retorna a regra de validação a ser utilizada na inserção.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
    {
        return x => ((PedidoItem)x).IdPedido.Equals(IdPedido) &&
                    ((PedidoItem)x).IdProduto.Equals(IdProduto);
    }

    /// <summary>
    /// Retorna a regra de validação a ser utilizada na atualização.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
    {
        return x => !((PedidoItem)x).IdPedidoItem.Equals(IdPedidoItem) &&
                    ((PedidoItem)x).IdPedido.Equals(IdPedido) &&
                    ((PedidoItem)x).IdProduto.Equals(IdProduto);
    }
    public Guid IdPedidoItem { get; set; }

    public DateTime Data { get; set; }

    public Guid IdPedido { get; set; }

    public Guid IdProduto { get; set; }

    public string? Observacao { get; set; }

    public int Quantidade { get; set; }

    [JsonIgnore]
    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

}
