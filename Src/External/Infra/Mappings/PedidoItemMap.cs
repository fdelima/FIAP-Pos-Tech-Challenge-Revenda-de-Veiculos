using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.Mappings;

internal class PedidoItemMap : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.HasKey(e => e.IdPedidoItem);

        builder.ToTable("pedido_item");

        builder.Property(e => e.IdPedidoItem)
            .ValueGeneratedNever()
            .HasColumnName("id_pedido_item");
        builder.Property(e => e.Data)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("data");
        builder.Property(e => e.IdPedido).HasColumnName("id_pedido");
        builder.Property(e => e.IdProduto).HasColumnName("id_produto");
        builder.Property(e => e.Observacao)
            .HasMaxLength(50)
            .HasColumnName("observacao");
        builder.Property(e => e.Quantidade)
            .HasDefaultValue(1)
            .HasColumnName("quantidade");

        builder.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.PedidoItems)
            .HasForeignKey(d => d.IdPedido)
            .HasConstraintName("FK_pedido_item_pedido");
    }
}
