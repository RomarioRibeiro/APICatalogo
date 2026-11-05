using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository
{
    public class ProdutoRepository : Repository<ProdutoModel>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext appContext) : base(appContext)
        {
        }

        public PageList<ProdutoModel> GetProdutos(ProdutosParameters produtosParameters)
        {
            var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();
            var produtosOrdenados = PageList<ProdutoModel>.ToPagedList(produtos, produtosParameters.PageNumber, produtosParameters.PageSize);

            return produtosOrdenados;
        }

        //public IEnumerable<ProdutoModel> GetProdutos(ProdutosParameters produtosParameters)
        //{
        //    return GetAll()
        //           .OrderBy(p => p.Nome)
        //             .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
        //                .Take(produtosParameters.PageSize);
        //}

        public IEnumerable<ProdutoModel> GetProdutosPorCategoria(int id)
        {
            return GetAll().Where(p => p.CategoriaId == id);
        }
    }
}
