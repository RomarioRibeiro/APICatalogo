using ApiCatalogo.Context;
using ApiCatalogo.Models;

namespace ApiCatalogo.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly AppDbContext _context;


        public ProdutoRepository(AppDbContext appContext)
        {
            _context = appContext;
        }


        public IQueryable<ProdutoModel> GetProdutos()
        {
            return _context.Produtos;
        }

        public ProdutoModel GetProduto(int id)
        {
            return _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        }

        public ProdutoModel Create(ProdutoModel produto)
        {
            if (produto is null)
            {
                throw new ArgumentNullException("Dados do produto invalidos para o cadastro.");
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return produto;
        }

        public bool Update(ProdutoModel produto)
        {
            if(produto is null)
            {
                throw new ArgumentNullException("Dados do produto invalidos para atualização.");
            }

            if(_context.Produtos.Any(p => p.ProdutoId == produto.ProdutoId))
            {
                _context.Produtos.Update(produto);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
           var produto = _context.Produtos.Find(id);

            if(produto is not null)
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
