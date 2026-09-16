using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; } = null!;
        public DbSet<Local> Locais { get; set; } = null!;
        public DbSet<Cardapio> Cardapios { get; set; } = null!;
        public DbSet<TipoServico> TiposServico { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Aqui serão adicionadas as configurações específicas das entidades
            builder.Entity<Evento>(entity =>
            {
                entity.HasOne(e => e.Local)
                    .WithMany()
                    .HasForeignKey(e => e.LocalId);

                entity.HasOne(e => e.Cardapio)
                    .WithMany()
                    .HasForeignKey(e => e.CardapioId);

                entity.HasOne(e => e.TipoServico)
                    .WithMany()
                    .HasForeignKey(e => e.TipoServicoId);

                entity.HasOne(e => e.Produtora)
                    .WithMany()
                    .HasForeignKey(e => e.ProdutoraId);

                entity.HasOne(e => e.PosVendedora)
                    .WithMany()
                    .HasForeignKey(e => e.PosVendedoraId);
            });
        }
    }
}
