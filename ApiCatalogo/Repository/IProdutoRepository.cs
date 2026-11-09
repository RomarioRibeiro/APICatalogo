using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using X.PagedList;

namespace ApiCatalogo.Repository
{
    public interface IProdutoRepository : IRepository<ProdutoModel>
    {
        //IEnumerable<ProdutoModel> GetProdutos(ProdutosParameters produtosParameters);

        Task<IPagedList<ProdutoModel>> GetProdutosAsync(ProdutosParameters produtosParameters);
        Task<IEnumerable<ProdutoModel>> GetProdutosPorCategoriaAsync(int id);
        Task<IPagedList<ProdutoModel>> GetProdutoFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroPreco);
    }
}
