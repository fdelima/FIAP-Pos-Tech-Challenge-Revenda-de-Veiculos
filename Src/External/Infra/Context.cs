using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra
{
    public partial class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options) { }

        #region [ DbSets ]

        public virtual DbSet<VeiculoEntity> Veiculos { get; set; }
        public virtual DbSet<VeiculoFotoEntity> VeiculoFotos { get; set; }
        public virtual DbSet<VeiculoPagamentoEntity> VeiculoPagamentos { get; set; }

        #endregion DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Map :: 2 - Adicione sua configuração aqui
            modelBuilder.ApplyConfiguration(new VeiculoFotoMap());
            modelBuilder.ApplyConfiguration(new VeiculoPagamentoMap());
            modelBuilder.ApplyConfiguration(new VeiculoMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}