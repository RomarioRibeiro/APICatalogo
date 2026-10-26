using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext _Dbcontext)
        {
            _context = _Dbcontext;
        }

        [HttpGet]
        public async  Task<ActionResult<IEnumerable<ProdutoModel>>> Get()
        {
            var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados...");
            }
            return produtos;
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProdutos")]
        public async Task<ActionResult<ProdutoModel>> Get(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(obj => obj.ProdutoId == id);
            if (produto is null)
            {
               return NotFound(string.Format($"Produto do codigo {id} não encontrado....."));
            }
            return produto;
        }
        [HttpPost]
        public ActionResult Post(ProdutoModel produto)
        {
            if (produto is null)
                return BadRequest();

            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProdutos",new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, ProdutoModel produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if(produto is null)
            {
                return NotFound(string.Format($"Produto do codigo {id} não encontrado..."));
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return Ok(produto);
        }
    }
}
