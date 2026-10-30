using ApiCatalogo.Models;

namespace ApiCatalogo.Repository
{
    public interface IProdutoRepository
    {

        IQueryable<ProdutoModel> GetProdutos();

        ProdutoModel GetProduto(int id);

        ProdutoModel Create(ProdutoModel produto);

        bool Update(ProdutoModel produto);
        bool Delete(int id);    
    }
}
