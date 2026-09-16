using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGE.Models;

namespace SGE.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; } = null!;
        public DbSet<Local> Locais { get; set; } = null!;
        public DbSet<TipoServico> TiposServico { get; set; } = null!;
        public DbSet<Cardapio> Cardapios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Evento>()
                .HasOne(e => e.Local)
                .WithMany(l => l.Eventos)
                .HasForeignKey(e => e.LocalId);

            builder.Entity<Evento>()
                .HasOne(e => e.TipoServico)
                .WithMany(ts => ts.Eventos)
                .HasForeignKey(e => e.TipoServicoId);

            builder.Entity<Evento>()
                .HasOne(e => e.Cardapio)
                .WithMany(c => c.Eventos)
                .HasForeignKey(e => e.CardapioId);
        }
    }
}
