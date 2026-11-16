using Microsoft.EntityFrameworkCore;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ApiCatalogo.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    { 
    }
    

    public DbSet<CategoriaModel>? Categorias { get; set; }
    public DbSet<ProdutoModel>? Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
