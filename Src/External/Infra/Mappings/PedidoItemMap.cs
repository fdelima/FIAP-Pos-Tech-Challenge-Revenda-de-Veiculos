using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.Mappings;

internal class VeiculoItemMap : IEntityTypeConfiguration<VeiculoFoto>
{
    public void Configure(EntityTypeBuilder<VeiculoFoto> builder)
    {
        builder.HasKey(e => e.IdVeiculoItem);

        builder.ToTable("veiculo_item");

        builder.Property(e => e.IdVeiculoItem)
            .ValueGeneratedNever()
            .HasColumnName("id_veiculo_item");
        builder.Property(e => e.Data)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("data");
        builder.Property(e => e.IdVeiculo).HasColumnName("id_veiculo");
        builder.Property(e => e.IdProduto).HasColumnName("id_produto");
        builder.Property(e => e.Observacao)
            .HasMaxLength(50)
            .HasColumnName("observacao");
        builder.Property(e => e.Quantidade)
            .HasDefaultValue(1)
            .HasColumnName("quantidade");

        builder.HasOne(d => d.IdVeiculoNavigation).WithMany(p => p.VeiculoFotos)
            .HasForeignKey(d => d.IdVeiculo)
            .HasConstraintName("FK_veiculo_item_veiculo");
    }
}
