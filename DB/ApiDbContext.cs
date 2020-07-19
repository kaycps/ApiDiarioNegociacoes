using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


using ApiDiarioNegociacoes.Models;

namespace ApiDiarioNegociacoes.DB
{
    public partial class ApiDbContext : DbContext
    {

        public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .IsRequired();

                entity.Property(e => e.Username)
                    .IsRequired();
                    
            });
            mb.Entity<Estrategia>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired();


                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Estrategias)
                    .HasForeignKey(e => e.IdUser);
            });

            mb.Entity<Negociacao>(entity =>
            {
                entity.Property(n => n.Ativo)
                    .IsRequired();

                entity.Property(n => n.Data)
                    .IsRequired();

                entity.Property(n => n.Encerramento)
                    .IsRequired();

                entity.Property(n => n.Lote)
                    .IsRequired();

                entity.Property(n => n.Operacao)
                    .IsRequired();

                entity.Property(n => n.PrecoEntrada)
                    .IsRequired();

                entity.Property(n => n.PrecoSaida)
                    .IsRequired();

                entity.HasOne(n => n.User)
                    .WithMany(u => u.Negociacaos)
                    .HasForeignKey(n => n.IdUser);

                entity.HasOne(e => e.Estrategia)
                    .WithMany(n => n.Negociacoes)
                    .HasForeignKey(e => e.IdEstrategia);
            });           

            base.OnModelCreating(mb);


        }

        public virtual DbSet<User> Users { get; set; }
        public  virtual DbSet<Estrategia> Estrategias { get; set; }
        public virtual DbSet<Negociacao> Negociacoes { get; set; }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
