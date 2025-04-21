using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;

public partial class Pedido : IDomainEntity
{
    /// <summary>
    /// Retorna a regra de validação a ser utilizada na inserção.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
    {
        return x => ((Pedido)x).IdCliente.Equals(IdCliente) &&
                    ((Pedido)x).IdDispositivo.Equals(IdDispositivo) &&
                    ((Pedido)x).Data.Equals(Data);
    }

    /// <summary>
    /// Retorna a regra de validação a ser utilizada na atualização.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
    {
        return x => !((Pedido)x).IdPedido.Equals(IdPedido) &&
                    ((Pedido)x).IdCliente.Equals(IdCliente) &&
                    ((Pedido)x).IdDispositivo.Equals(IdDispositivo) &&
                    ((Pedido)x).Data.Equals(Data);
    }

    public Guid IdPedido { get; set; }

    public DateTime Data { get; set; }

    public Guid IdDispositivo { get; set; }

    public Guid? IdCliente { get; set; }

    public string Status { get; set; }

    public DateTime DataStatusPedido { get; set; }

    public string StatusPagamento { get; set; }

    public DateTime DataStatusPagamento { get; set; }

    public virtual ICollection<PedidoItem> PedidoItems { get; set; } = new List<PedidoItem>();
}
