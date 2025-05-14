using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.Mappings;

internal class VeiculoMap : IEntityTypeConfiguration<VeiculoEntity>
{
    public void Configure(EntityTypeBuilder<VeiculoEntity> builder)
    {
        builder.HasKey(e => e.IdVeiculo);

        builder.ToTable("veiculo");

        builder.Property(e => e.IdVeiculo)
            .ValueGeneratedNever()
            .HasColumnName("id_veiculo");
        builder.Property(e => e.Marca)
            .HasMaxLength(100)
            .HasColumnName("marca");
        builder.Property(e => e.Modelo)
            .HasMaxLength(100)
            .HasColumnName("modelo");
        builder.Property(e => e.AnoFabricacao)
            .HasColumnName("ano_fabricacao");
        builder.Property(e => e.AnoModelo)
            .HasColumnName("ano_modelo");
        builder.Property(e => e.Placa)
            .HasMaxLength(7)
            .HasColumnName("placa");
        builder.Property(e => e.Renavam)
            .HasMaxLength(11)
            .HasColumnName("renavam");
        builder.Property(e => e.Preco)
            .HasColumnName("preco");
        builder.Property(e => e.Status)
            .HasMaxLength(50)
            .HasColumnName("status");
        builder.Property(e => e.Thumb)
            .IsUnicode(false)
            .HasColumnName("thumb_base64");
    }
}
