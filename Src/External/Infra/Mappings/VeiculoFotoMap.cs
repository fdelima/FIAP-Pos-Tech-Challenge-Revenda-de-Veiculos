using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.Mappings;

internal class VeiculoFotoMap : IEntityTypeConfiguration<VeiculoFoto>
{
    public void Configure(EntityTypeBuilder<VeiculoFoto> builder)
    {
        builder.HasKey(e => e.IdVeiculoFoto);

        builder.ToTable("veiculo_foto");

        builder.Property(e => e.IdVeiculoFoto)
            .ValueGeneratedNever()
            .HasColumnName("id_veiculo_foto");
        builder.Property(e => e.IdVeiculo)
            .HasColumnName("id_veiculo");
        builder.Property(e => e.Imagem)
            .HasMaxLength(4000)
            .HasColumnName("imagem_base64");

        builder.HasOne(d => d.Veiculo).WithMany(p => p.Fotos)
            .HasForeignKey(d => d.IdVeiculo)
            .HasConstraintName("FK_veiculo_foto_veiculo");
    }
}
