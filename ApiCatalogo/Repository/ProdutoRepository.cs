using ApiCatalogo.Context;
using ApiCatalogo.Models;

namespace ApiCatalogo.Repository
{
    public class ProdutoRepository : Repository<ProdutoModel>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext appContext) : base(appContext)
        {
        }
        public IEnumerable<ProdutoModel> GetProdutosPorCategoria(int id)
        {
            return GetAll().Where(p => p.CategoriaId == id);
        }
    }
}
