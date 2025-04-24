using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.Mappings;

internal class VeiculoMap : IEntityTypeConfiguration<Domain.Entities.Veiculo>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Veiculo> builder)
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
            .HasMaxLength(10)
            .HasColumnName("status");
    }
}
