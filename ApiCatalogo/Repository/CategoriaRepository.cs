using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
    public class CategoriaRepository :  Repository<CategoriaModel> ,ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }       
    }
}
