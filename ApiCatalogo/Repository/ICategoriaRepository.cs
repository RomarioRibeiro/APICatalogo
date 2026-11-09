using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using System.Collections;
using X.PagedList;

namespace ApiCatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<CategoriaModel>
    {

        Task<IPagedList<CategoriaModel>> GetCategoriasAsync(CategoriaParameters categoriaParameters);

        Task<IPagedList<CategoriaModel>> GetCategoriasFiltroNomeAsync(CategoriaFiltroNome categoriaFiltroNome);

    }
}
