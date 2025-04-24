using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.Mappings;

internal class VeiculoPagamentoMap : IEntityTypeConfiguration<VeiculoPagamento>
{
    public void Configure(EntityTypeBuilder<VeiculoPagamento> builder)
    {
        builder.HasKey(e => e.IdVeiculoPagamento);

        builder.ToTable("id_veiculo_pagamento");

        builder.Property(e => e.IdVeiculoPagamento)
            .ValueGeneratedNever()
            .HasColumnName("id_veiculo_pagamento");
        builder.Property(e => e.IdVeiculo)
            .HasColumnName("id_veiculo");
        builder.Property(e => e.Data)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("data");
        builder.Property(e => e.ValorRecebido)
            .HasColumnName("valor_recebido");
        builder.Property(e => e.Banco)
            .HasMaxLength(100)
            .HasColumnName("banco");
        builder.Property(e => e.Conta)
            .HasMaxLength(100)
            .HasColumnName("conta");
        builder.Property(e => e.CpfCnpj)
            .HasMaxLength(14)
            .HasColumnName("cpf_cnpj");

        builder.HasOne(d => d.Veiculo).WithMany(p => p.Pagamentos)
            .HasForeignKey(d => d.IdVeiculo)
            .HasConstraintName("FK_veiculo_pagamento_veiculo");
    }
}
