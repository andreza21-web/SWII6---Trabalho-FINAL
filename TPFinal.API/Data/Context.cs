using Microsoft.EntityFrameworkCore;
using TPFinal.API.Models;

namespace TPFinal.API.Data
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options):base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(p => p.Id);

            modelBuilder.Entity<Usuario>().Property(p => p.Id);
            modelBuilder.Entity<Usuario>().Property(p => p.Nome);
            modelBuilder.Entity<Usuario>().Property(p => p.Senha);
            modelBuilder.Entity<Usuario>().Property(p => p.Status);


            modelBuilder.Entity<Produto>().HasKey(p => p.Id);

            modelBuilder.Entity<Produto>().Property(p => p.Id);
            modelBuilder.Entity<Produto>().Property(p => p.Nome);
            modelBuilder.Entity<Produto>().Property(p => p.Status);
            modelBuilder.Entity<Produto>().Property(p => p.IdUsuarioCadastro);
            modelBuilder.Entity<Produto>().Property(p => p.IdUsuarioUpdate);
        }
    }
}
