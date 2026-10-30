using ApiCatalogo.Models;

namespace ApiCatalogo.Repository
{
    public interface IProdutoRepository : IRepository<ProdutoModel>
    {

        IEnumerable<ProdutoModel> GetProdutosPorCategoria(int id); 
    }
}
