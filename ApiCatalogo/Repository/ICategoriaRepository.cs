using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using System.Collections;

namespace ApiCatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<CategoriaModel>
    {

        PageList<CategoriaModel> GetCategorias(CategoriaParameters categoriaParameters);

    }
}
