using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
    public class CategoriaRepository :  Repository<CategoriaModel> ,ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }

        public PageList<CategoriaModel> GetCategorias(CategoriaParameters categoriaParameters)
        {
            var categorias = GetAll().OrderBy(c => c.CategoriaId).AsQueryable();
            var categoriaOrdenada = PageList<CategoriaModel>.ToPagedList(categorias, categoriaParameters.PageNumber, categoriaParameters.PageSize);
            return categoriaOrdenada;
        }
    
    }
}
