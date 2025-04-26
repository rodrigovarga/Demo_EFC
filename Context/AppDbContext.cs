using Demo_EFC.Model;
using Microsoft.EntityFrameworkCore;

namespace Demo_EFC.Context
{
    public class AppDbContext : DbContext 
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId);


            modelBuilder.Entity<Categoria>().HasData(
             new Categoria { Id = 1, Nome = "Eletrônicos" },
             new Categoria { Id = 2, Nome = "Livros" }
         );

            modelBuilder.Entity<Produto>().HasData(
                new Produto { Id = 1, Nome = "Notebook", Preco = 3500, CategoriaId = 1 },
                new Produto { Id = 2, Nome = "Smartphone", Preco = 2200, CategoriaId = 1 },
                new Produto { Id = 3, Nome = "Livro de C#", Preco = 120, CategoriaId = 2 }
            );
        }
    }
}
