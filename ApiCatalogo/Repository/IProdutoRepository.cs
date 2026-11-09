using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository
{
    public interface IProdutoRepository : IRepository<ProdutoModel>
    {
        //IEnumerable<ProdutoModel> GetProdutos(ProdutosParameters produtosParameters);

        Task<PageList<ProdutoModel>> GetProdutosAsync(ProdutosParameters produtosParameters);
        Task<IEnumerable<ProdutoModel>> GetProdutosPorCategoriaAsync(int id);
        Task<PageList<ProdutoModel>> GetProdutoFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroPreco);
    }
}
