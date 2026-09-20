using Microsoft.EntityFrameworkCore;
using ApiCatalogo.Models;

namespace ApiCatalogo.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    { 
    }
    

    public DbSet<CategoriaModel>? Categorias { get; set; }
    public DbSet<ProdutoModel>? Produtos { get; set; }

}
