using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using System.Collections;

namespace ApiCatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<CategoriaModel>
    {

        Task<PageList<CategoriaModel>> GetCategoriasAsync(CategoriaParameters categoriaParameters);

        Task<PageList<CategoriaModel>> GetCategoriasFiltroNomeAsync(CategoriaFiltroNome categoriaFiltroNome);

    }
}
