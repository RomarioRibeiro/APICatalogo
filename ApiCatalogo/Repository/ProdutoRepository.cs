using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public class ProdutoRepository : Repository<ProdutoModel>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext appContext) : base(appContext)
        {
        }

        public async Task<PageList<ProdutoModel>> GetProdutoFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroPreco)
        {
            var produtos = await GetAllAsync();

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

            var produtosOrdenado = produtos.OrderBy(p => p.Preco).AsQueryable();

            var produtosOrdenados = PageList<ProdutoModel>.ToPagedList(produtosOrdenado, produtosFiltroPreco.PageNumber, produtosFiltroPreco.PageSize);

            return produtosOrdenados;
        }

        public async Task<PageList<ProdutoModel>> GetProdutosAsync(ProdutosParameters produtosParameters)
        {
            var produtos = await GetAllAsync();

            var produtosOrdenado = produtos.OrderBy(p => p.Nome).AsQueryable();

            var produtosOrdenados = PageList<ProdutoModel>.ToPagedList(produtosOrdenado, produtosParameters.PageNumber, produtosParameters.PageSize);

            return produtosOrdenados;
        }

        //public IEnumerable<ProdutoModel> GetProdutos(ProdutosParameters produtosParameters)
        //{
        //    return GetAll()
        //           .OrderBy(p => p.Nome)
        //             .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
        //                .Take(produtosParameters.PageSize);
        //}

        public async Task<IEnumerable<ProdutoModel>> GetProdutosPorCategoriaAsync(int id)
        {
            var produtos =  await GetAllAsync();
            var produtosPorCategoria = produtos.Where(p => p.CategoriaId == id);
            return produtosPorCategoria;
        }
    }
}
