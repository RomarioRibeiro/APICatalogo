using ApiCatalogo.Models;

namespace ApiCatalogo.Repository
{
    public interface ICategoriaRepository
    {
        IEnumerable<CategoriaModel> GetCategorias();

        CategoriaModel GetCategoriaById(int id);

        CategoriaModel Create(CategoriaModel categoria);

        CategoriaModel Uptade(CategoriaModel categoria);

        CategoriaModel Delete(int id);

    }
}
