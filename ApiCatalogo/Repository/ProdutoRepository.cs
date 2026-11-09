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

        public PageList<ProdutoModel> GetProdutoFiltroPreco(ProdutosFiltroPreco produtosFiltroPreco)
        {
            var produtos = GetAll().AsQueryable();

            if (produtosFiltroPreco.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroPreco.PrecoCriterio))
            {
                if (produtosFiltroPreco.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco > produtosFiltroPreco.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosFiltroPreco.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco < produtosFiltroPreco.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosFiltroPreco.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco == produtosFiltroPreco.Preco.Value).OrderBy(p => p.Preco);
                }
            }

            var produtosOrdenados = PageList<ProdutoModel>.ToPagedList(produtos, produtosFiltroPreco.PageNumber, produtosFiltroPreco.PageSize);

            return produtosOrdenados;
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
