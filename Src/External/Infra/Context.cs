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

        public virtual DbSet<Notificacao> Notificacaos { get; set; }

        public virtual DbSet<Domain.Entities.Pedido> Pedidos { get; set; }

        public virtual DbSet<PedidoItem> PedidoItems { get; set; }

        #endregion DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Map :: 2 - Adicione sua configuração aqui
            modelBuilder.ApplyConfiguration(new NotificacaoMap());
            modelBuilder.ApplyConfiguration(new PedidoItemMap());
            modelBuilder.ApplyConfiguration(new PedidoMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}