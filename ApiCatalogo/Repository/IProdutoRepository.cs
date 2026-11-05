using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository
{
    public interface IProdutoRepository : IRepository<ProdutoModel>
    {
        //IEnumerable<ProdutoModel> GetProdutos(ProdutosParameters produtosParameters);

        PageList<ProdutoModel> GetProdutos(ProdutosParameters produtosParameters);
        IEnumerable<ProdutoModel> GetProdutosPorCategoria(int id); 


    }
}
