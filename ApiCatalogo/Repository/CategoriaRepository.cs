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

        public async Task<PageList<CategoriaModel>> GetCategoriasAsync(CategoriaParameters categoriaParameters)
        {
            var categorias = await GetAllAsync();

            var categoriaOrdenadas = categorias.OrderBy(c => c.Nome).AsQueryable();

            var categoriaOrdenada = PageList<CategoriaModel>.ToPagedList(categoriaOrdenadas, categoriaParameters.PageNumber, categoriaParameters.PageSize);
            return categoriaOrdenada;
        }

        public async Task<PageList<CategoriaModel>> GetCategoriasFiltroNomeAsync(CategoriaFiltroNome categoriaFiltroNome)
        {
            var categorias = await GetAllAsync();

            if (!string.IsNullOrEmpty(categoriaFiltroNome.Nome))
            {
                categorias = categorias.Where(c => c.Nome.Contains(categoriaFiltroNome.Nome));
            }

            var categoriaOrdenada = PageList<CategoriaModel>.ToPagedList(categorias.AsQueryable(), categoriaFiltroNome.PageNumber, categoriaFiltroNome.PageSize);
            return categoriaOrdenada;
        }
    }
}
