using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.Mappings;

internal class PedidoMap : IEntityTypeConfiguration<Domain.Entities.Pedido>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Pedido> builder)
    {
        builder.HasKey(e => e.IdPedido);

        builder.ToTable("revendaDeVeiculos");

        builder.Property(e => e.IdPedido)
            .ValueGeneratedNever()
            .HasColumnName("id_pedido");
        builder.Property(e => e.Data)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("data");
        builder.Property(e => e.DataStatusPedido)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("data_status_pedido");
        builder.Property(e => e.IdCliente).HasColumnName("id_cliente");
        builder.Property(e => e.IdDispositivo).HasColumnName("id_dispositivo");
        builder.Property(e => e.Status)
            .HasMaxLength(50)
            .HasColumnName("status");
        builder.Property(e => e.StatusPagamento)
            .HasMaxLength(50)
            .HasColumnName("status_pagamento");
        builder.Property(e => e.DataStatusPagamento)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("data_status_pagamento");
    }
}
